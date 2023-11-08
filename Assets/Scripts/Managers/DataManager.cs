using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : GoogleSheetManager
{
    public Dictionary<Define.ItemType, int> CurrentLevel = new Dictionary<Define.ItemType, int>();
    public Dictionary<Define.ItemType, float> CurrentStat = new Dictionary<Define.ItemType, float>();

    public override void Init()
    {
        base.Init();
    }
}
