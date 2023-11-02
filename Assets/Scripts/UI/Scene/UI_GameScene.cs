using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    public PlayerMove player;
    public Text ScoreText;
    public enum Texts
    {
        ScoreText
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        player = gameObject.GetComponent<PlayerMove>();
        ScoreText = Get<Text>((int)Texts.ScoreText).GetComponent<Text>();
        ScoreText.text = "현재 칸 수 : " + player.score;
    }
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "현재 칸 수 : " + player.score;
    }
}
