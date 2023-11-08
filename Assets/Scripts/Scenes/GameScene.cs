using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Clear()
    {
        
    }
    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();
        Managers.UI.ShowSceneUI<UI_GameScene>();
        //TO-DO : Play BGM
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
}
