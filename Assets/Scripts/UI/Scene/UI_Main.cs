using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    public enum Buttons
    {
        StartButton,
        SettingButton,
        DataTest
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
        GetButton((int)Buttons.StartButton).gameObject.AddUIEvent(StartClicked);
        GetButton((int)Buttons.SettingButton).gameObject.AddUIEvent(SettingClicked);
        GetButton((int)Buttons.DataTest).gameObject.AddUIEvent(DataTest);
    }
    public void StartClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }
    public void SettingClicked(PointerEventData eventData)
    {
        Debug.Log("SettingClicked");
    }
    public void DataTest(PointerEventData eventData)
    {
        //if(DataManager._instance.MaxScore[0].MaxScore < 100)
        //{
        //    StartCoroutine(DataManager._instance.SaveData(100));
        //}
        //if (CanPopUpUI_SelectItem())
            Managers.UI.ShowPopUpUI<UI_SelectItem>();
        //else
            //Debug.Log("Can PopUp UI_SelectItem");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
