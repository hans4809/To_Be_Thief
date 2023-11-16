using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region 아이템 데이터
    public class ItemTable
    {
        public int itemIndex;
        public int level;
        public bool isDebuff;
        public float effect;
        public string itemName;
        public string itemExplain;
    }
    public struct ItemKey
    {
        public int itemIndex { get; set; }
        public int level { get; set; }
        public bool isDebuff { get; set; }
        public ItemKey(int _itemIndex, int _level, bool _isDebuff)
        {
            this.itemIndex = _itemIndex;
            this.level = _level;
            this.isDebuff = _isDebuff;
        }
        public override int GetHashCode()
        {
            return itemIndex.GetHashCode() + level.GetHashCode() + isDebuff.GetHashCode();
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
    #endregion
    public class GameData
    {
        public int maxScore;
        public bool firstPlay;
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
        Crouch
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
        TitleScene,
        MainScene,
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