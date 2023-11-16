using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public int score = 0;
    public PlayerMove player;
    public int itemSelected = 0;
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
        for (int i = 0; i < Managers.Data.itemDict.Count; i++)
        {
            Managers.Data.currentLevel[(Define.ItemType)i] = 1;
            Managers.Data.currentStat[(Define.ItemType)i] = Managers.Data.itemDict[new Define.ItemKey((Define.ItemType)i, 1, true)].effect;
        }
        currentState = GameState.Playing;
        Time.timeScale = 1;
        itemSelected = 0;
    }
    //인게임 동작 중 실행시킬 함수
    public void OnUpdate()
    {
        if (player == null) return;
        if (currentState == GameState.Playing)
        {
            score = (int)player.transform.position.y / 2;
            if (CanPopUpUI_ItemButton())
            {
                if (score == 5 || (score % 30 == 0 && score > 5))
                {
                    if ((score + 1 > itemSelected * 30 && itemSelected != 0) || itemSelected == 0)
                    {
                        Managers.UI.ShowPopUpUI<UI_SelectItem>();
                        Time.timeScale = 0;
                        currentState = GameState.Pause;
                    }
                }
            }
        }
    }
    // 중간에 아이템 선택창을 띄울수 있는지 없는지 확인하는 함수
    bool CanPopUpUI_ItemButton()
    {
        int notLevel3 = 0;
        foreach(var var in Managers.Data.currentLevel)
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
