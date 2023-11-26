using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : GoogleSheetManager
{
    public Dictionary<int, int> currentLevel = new Dictionary<int, int>();
    public Dictionary<int, float> currentStat = new Dictionary<int, float>();
    public List<Define.ScoreData> scoreDatas = new List<Define.ScoreData>();
    public Define.WholeGameData gameData;
    public Define.PatternData patternData;
    public JsonManager jsonManager = new JsonManager();
    public override void Init()
    {
        base.Init();
        patternData = jsonManager.Load<Define.PatternData>();
        gameData = jsonManager.Load<Define.WholeGameData>();
    }
}
