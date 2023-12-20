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
        SceneType = Define.Scene.GameScene;
        Managers.Game.player = FindObjectOfType<PlayerMove>();
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Sound.Play("Sounds/BGM/GameBGM", Define.Sound.BGM);
        Managers.Game.GameStart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
