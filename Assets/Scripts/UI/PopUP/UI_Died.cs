using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Died : UI_Popup
{
    public enum Buttons
    {
        MainMenuButton,
        ReplayButton
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
        GetButton((int)Buttons.MainMenuButton).gameObject.AddUIEvent(MainMenuClicked);
        GetButton((int)Buttons.ReplayButton).gameObject.AddUIEvent(ReplayClicked);
    }
    public void MainMenuClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainScene);
        Time.timeScale = 1;
    }
    public void ReplayClicked(PointerEventData eventData)
    {
        Managers.Game.GameStart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
