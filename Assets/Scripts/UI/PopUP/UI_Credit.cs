using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Credit : UI_Popup
{
    public enum Buttons
    {
        ExitButton
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
        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(ExitButtonClicked);

    }
    void ExitButtonClicked(PointerEventData eventData)
    {
        ClosePopUPUI();
    }
}
