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
        
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        //GetButton((int)Buttons.GameStart).gameObject.AddUIEvent();
        //GetButton((int)Buttons.Setting).gameObject.AddUIEvent();
    }
    public void GameStartClicked(PointerEventData eventData)
    {
        //Managers.UI.ShowSceneUI
    }
    public void SettingClicked(PointerEventData eventData)
    {
        //Managers.UI.ShowSceneUI
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
