using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    public Text ScoreText;
    public enum GameObjects
    {
        ScoreText,
        MaxScoreText,
        PauseButton
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        ScoreText = Get<GameObject>((int)GameObjects.ScoreText).GetComponent<Text>();
        Get<GameObject>((int)GameObjects.MaxScoreText).GetComponent<Text>().text = "최고 칸 수 : " + Managers.Data.wholeGameData[0].maxScore;
        Get<GameObject>((int)GameObjects.PauseButton).AddUIEvent(PauseClicked);
        ScoreText.text = "현재 칸 수 : " + Managers.Game.score;
    }
    public void PauseClicked(PointerEventData eventData)
    {
        Managers.UI.ShowPopUpUI<UI_GameSetting>();
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        ScoreText.text = "현재 칸 수 : " + Managers.Game.score;
    }
}
