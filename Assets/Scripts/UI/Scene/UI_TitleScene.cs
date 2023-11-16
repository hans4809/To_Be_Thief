using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    public enum Buttons
    {
        GameStart,
        Setting
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
        GetButton((int)Buttons.GameStart).gameObject.AddUIEvent(StartClicked);
        GetButton((int)Buttons.Setting).gameObject.AddUIEvent(SettingClicked);
    }
    public void StartClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainScene);
    }
    public void SettingClicked(PointerEventData eventData)
    {
        Managers.UI.ShowPopUpUI<UI_TitleSetting>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
