using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Option : UI_Popup
{
    public enum Buttons
    {
        Button_Close
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.Button_Close).gameObject.AddUIEvent(CloseButtonClicked);
    }
    public void CloseButtonClicked(PointerEventData evenData)
    {
        Managers.UI.ClosePopUpUI();
        Time.timeScale = 1;
    }
}
