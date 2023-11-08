using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public int score = 0;
    public PlayerMove player;
    //게임 상태를 나눠서 상태에 따라 스크립트들이 돌아가게 함
    public enum GameState
    {
        Playing,
        Pause,
        End
    }
    public GameState currentState;
    void Update()
    {
    }
    //플레이어 죽을 때 실행시킬 함수
    public void PlayerDied()
    {
        if (score > Managers.Data.gameDatas[0].maxScore)
            Managers.Data.SaveData(score, 1);
        else if (Managers.Data.gameDatas[0].firstPlay == 0)
            Managers.Data.SaveData(Managers.Data.gameDatas[0].maxScore, 1);
        currentState = GameState.End;
        Time.timeScale = 0;
    }
    //인게임 데이터 초기화 
    public void Init()
    {
        score = 0;
        for (int i = 0; i < Managers.Data.items.Count; i++)
        {
            Managers.Data.CurrentLevel[Managers.Data.items[i].itemType] = 1;
            Managers.Data.CurrentStat[Managers.Data.items[i].itemType] = Managers.Data.items[i].level_1;
        }
        currentState = GameState.Playing;
        Time.timeScale = 1;
    }
    //인게임 동작 중 실행시킬 함수
    public void OnUpdate()
    {
        if (player == null) return;
        if (currentState == GameState.Playing)
        {
            score = (int)player.transform.position.y / 2;
        }
    }
    // 중간에 아이템 선택창을 띄울수 있는지 없는지 확인하는 함수
    bool CanPopUpUI_ItemButton()
    {
        int notLevel3 = 0;
        foreach(var var in Managers.Data.CurrentLevel)
        {
            if(var.Value != 3)
            {
                notLevel3++;
            }
        }
        if (notLevel3 >= 3) return true;
        return false;
    }
}
