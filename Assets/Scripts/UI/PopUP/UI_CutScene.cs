using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CutScene : UI_Popup
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        StartCoroutine(CutScene());
    }
    IEnumerator CutScene()
    {
        yield return new WaitForSeconds(10f);
        Managers.UI.ClosePopUpUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
