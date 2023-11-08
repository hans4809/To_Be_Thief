using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    UI_Main Original;
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        Managers.UI.ShowSceneUI<UI_Main>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
