using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public class ItemTable
    {
        public ItemType itemType;
        public int level;
        public bool isDebuff;
        public float effect;
        public string itemName;
        public string itemExplain;
    }
    public class ItemKey
    {
        public ItemType itemType { get; set; }
        public int level { get; set; }
        public bool isDebuff { get; set; }
        public ItemKey(ItemType _itemType, int _level, bool _isDebuff)
        {
            this.itemType = _itemType;
            this.level = _level;
            this.isDebuff = _isDebuff;
        }
        public override int GetHashCode()
        {
            return itemType.GetHashCode() + level.GetHashCode() + isDebuff.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ItemKey o = obj as ItemKey;
            return o != null && (o.itemType == this.itemType || o.level == this.level || o.isDebuff == this.isDebuff);
        }
    }
    public class ItemData
    {
        public float effect;
        public string itemName;
        public string itemExplain;
        public ItemData(float _effect, string _itemName, string _itemExaplain)
        {
            this.effect = _effect;
            this.itemName = _itemName;
            this.itemExplain = _itemExaplain;
        }
    }

    public class GameData
    {
        public int maxScore;
        public int firstPlay;
    }
    public enum ItemType
    {
        Player_speed,
        Player_HitBox,
        CCTV_Time,
        Thorn_Steps,
        Rock_SpawnTime,
        Rock_Speed,
        Rock_HitBox,
        MaxCount
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
        Click,
        End
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