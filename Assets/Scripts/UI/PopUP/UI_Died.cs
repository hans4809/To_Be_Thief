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
        //오브젝트 초기화 
        // BackGround 초기화
        GameObject[] patterns = GameObject.FindGameObjectsWithTag("pattern");
        for (int i = 0; i < patterns.Length; i++)
        {
            if (patterns[i].activeSelf)
                patterns[i].SetActive(false);
        }
        //Obastacle 초기화
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Objects.Length; i++)
        { if (Objects[i].activeSelf)
                Objects[i].SetActive(false);
        }

        Managers.Game.GameStart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
