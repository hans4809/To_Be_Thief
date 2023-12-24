using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Clear()
    {
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.TitleScene;
        Managers.UI.ShowSceneUI<UI_TitleScene>();
        Managers.Sound.Play("Sounds/BGM/MainTitle", Define.Sound.BGM);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
