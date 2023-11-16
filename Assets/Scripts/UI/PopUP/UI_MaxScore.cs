using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MaxScore : UI_Popup
{
    public enum GameObjects
    {
        CloseButton,
        MaxScoreText
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
        Get<GameObject>((int)GameObjects.CloseButton).AddUIEvent(CloseClicked);
        Get<GameObject>((int)GameObjects.MaxScoreText).GetComponent<Text>().text = Managers.Data.wholeGameData[0].maxScore.ToString();
    }
    public void CloseClicked(PointerEventData eventData)
    {
        ClosePopUPUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
