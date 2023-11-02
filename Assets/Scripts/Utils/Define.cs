using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public class Items
    {
        public ItemType itemType;
        public float level_1;
        public float level_2;
        public float level_3;
    }
    public class Score
    {
        public int MaxScore;
    }
    public enum ItemType
    {
        Player_speed,
        Player_HitBox,
        CCTV_Time,
        Thorn_Steps,
        Rock_SpawnTime,
        Rock_Speed,
        Rock_HitBox
    }
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
    }
    public enum State
    {
        Idle,
        Walk,
        Run
    }
    public enum UIEvent
    {
        Click,
        BeginDrag,
        Drag,
        DragEnd,
        PointerDown,
        PointerUP
    }
    public enum MouseEvent
    {
        Press,
        Click
    }

    public enum Scene
    {
        Unknown,
        MainScene,
        DataTest,
        GameScene
    }
    public enum Sound
    {
        Master,
        BGM,
        SFX,
        MaxCount
    }
}