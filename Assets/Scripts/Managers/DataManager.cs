using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, int> currentLevel = new Dictionary<int, int>();
    public Dictionary<int, float> currentStat = new Dictionary<int, float>();
    public List<Define.ScoreData> scoreDatas = new List<Define.ScoreData>();
    public Define.WholeGameData gameData;
    public Define.PatternDatas patternDatas;
    public Define.VolumeData volumeData = new Define.VolumeData();
}
