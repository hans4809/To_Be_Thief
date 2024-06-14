using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public int score = 0;
    int beforeScore = 0;
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
            JsonManager.Save(Managers.Data.gameData);
        }
        Managers.Data.gameData.firstPlay = true;
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
        //player.Init();
        player.MakeStartPattern();
        Managers.UI.CloseAllPopUPUI();
        player.GetComponent<Collider2D>().isTrigger = true;
        score = 0;
        beforeScore = 0;
        if (player != null) { player.transform.localPosition = new Vector3(0, 0, 0); }
        for (int i = 0; i < 7; i++)
        {
            Managers.Data.currentLevel[i] = 1;
            Managers.Data.currentStat[i] = Managers.GoogleSheet.itemDict[new Define.ItemKey(i, 1, true)].effect;
        }
        currentState = GameState.Playing;
        player.Init(); // currentLevel, Stat 업데이트 하고 init해야 재시작 할 때 초기화가 돼서 위치 옮겼습니다.
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
                //score가 int형이라 한 칸 이동하기 전까지 아이템 선택창이 여러번 뜨는 오류가 있어서 beforeScore 변수를 추가했습니다.
                if ((score == 15 || (score > 15 && ((score-5) % 10 == 0))) && beforeScore != score)
                {
                    if ((score + 1 > itemSelected * 10 && itemSelected != 0) || itemSelected == 0)
                    //if(itemSelected == 0)
                    {
                        Managers.Sound.Play("Sounds/SFX/OpenBox");
                        Managers.UI.ShowPopUpUI<UI_SelectItem>();
                        Time.timeScale = 0;
                        currentState = GameState.Pause;
                        beforeScore = score;
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
