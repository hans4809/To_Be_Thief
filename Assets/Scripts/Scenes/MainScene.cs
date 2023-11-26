using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    private void Start()
    {
        if (!Managers.Data.gameData.firstPlay)
        {
            Managers.UI.ShowPopUpUI<UI_CutScene>();
        }
    }
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.MainScene;
        Managers.UI.ShowSceneUI<UI_Main>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Clear()
    {
        
    }
}
