using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public int score = 0;
    public PlayerMove player;
    public int itemSelected = 0;
    bool Stage_in2 = false;
    bool Stage_in3 = false;
    //게임 상태를 나눠서 상태에 따라 스크립트들이 돌아가게 함
    public enum GameState
    {
        Playing,
        Pause,
        End
    }
    public GameState currentState;
    //플레이어 죽을 때 실행시킬 함수
    public void PlayerDied()
    {
        if (score > Managers.Data.gameData.maxScore)
        {
            Managers.Data.gameData.maxScore = score;
            Managers.Data.gameData.firstPlay = true;
            Managers.Json.Save(Managers.Data.gameData);
        }
        currentState = GameState.End;
        Time.timeScale = 0;
        player.GetComponent<Collider2D>().isTrigger = false;
        Managers.UI.ShowPopUpUI<UI_Died>();
        Managers.Sound._audioSources[(int)Define.Sound.BGM].Stop();
        Managers.Sound.Play("Sounds/SFX/GameOver");
    }
    //인게임 데이터 초기화 
    public void GameStart()
    {
        BackGroundManager backGroundManager = GameObject.FindObjectOfType<BackGroundManager>();
        ObjectManager objectManager = GameObject.FindObjectOfType<ObjectManager>();
        backGroundManager.Clear();
        objectManager.Clear();
        player.Init();
        player.MakeStartPattern();
        Managers.UI.CloseAllPopUPUI();
        player.GetComponent<Collider2D>().isTrigger = true;
        score = 0;
        if (player != null) { player.transform.localPosition = new Vector3(0, 0, 0); }
        for (int i = 0; i < 7; i++)
        {
            Managers.Data.currentLevel[i] = 1;
            Managers.Data.currentStat[i] = Managers.GoogleSheet.itemDict[new Define.ItemKey(i, 1, true)].effect;
        }
        currentState = GameState.Playing;
        Time.timeScale = 1;
        itemSelected = 0;
        Managers.Sound.Play("Sounds/BGM/InGameBGM", Define.Sound.BGM);
    }
    //인게임 중 Update문으로 실행시킬 함수
    public void OnUpdate()
    {
        if (player == null) return;
        if (currentState == GameState.Playing)
        {
            score = (int)player.transform.position.y / 2;
            
            // 60점 넘으면 2페이즈로 넘어가기.
            if(score == 60 &&Stage_in2 ==false)
            {
                player.MapCode = 1;
                Managers.Sound.Play("Sounds/BGM/Phase_2", Define.Sound.BGM);
                Stage_in2 = true;
            }

            // 100점 넘으면 3페이즈로 넘어가기.
            if(score == 100 &&Stage_in3==false)
            {
                player.MapCode = 2;
                Managers.Sound.Play("Sounds/BGM/Phase_3",Define.Sound.BGM);
                Stage_in3 = true;
            }


            if (CanPopUpUI_ItemButton())
            {
                if (score == 5 || (score % 10 == 0 && score > 5))
                //if (score == 50 || ((score-50) % 30 == 0))
                {
                    if ((score + 1 > itemSelected * 10 && itemSelected != 0) || itemSelected == 0)
                    //if(itemSelected == 0)
                    {
                        Managers.Sound.Play("Sounds/SFX/OpenBox");
                        Managers.UI.ShowPopUpUI<UI_SelectItem>();
                        Time.timeScale = 0;
                        currentState = GameState.Pause;
                    }
                    //else
                    //{
                    //    if((score - 50) + 1 > itemSelected * 30)
                    //    {
                    //        Managers.Sound.Play("Sounds/SFX/OpenBox");
                    //        Managers.UI.ShowPopUpUI<UI_SelectItem>();
                    //        Time.timeScale = 0;
                    //        currentState = GameState.Pause;
                    //    }
                    //}
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
