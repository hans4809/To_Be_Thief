using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : GoogleSheetManager
{
    public Dictionary<int, int> currentLevel = new Dictionary<int, int>();
    public Dictionary<int, float> currentStat = new Dictionary<int, float>();
    public List<Define.GameData> wholeGameData = new List<Define.GameData>();
    public override void Init()
    {
        base.Init();
    }
}
