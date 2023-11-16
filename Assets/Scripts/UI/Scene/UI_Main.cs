using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    public enum Buttons
    {
        ScoreButton,
        StartButton
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.ScoreButton).gameObject.AddUIEvent(ScoreClicked);
        GetButton((int)Buttons.StartButton).gameObject.AddUIEvent(StartClicked);
    }
    public void StartClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }
    public void ScoreClicked(PointerEventData eventData)
    {
        Managers.UI.ShowPopUpUI<UI_MaxScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
