using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : GoogleSheetManager
{
    public static DataManager _instance;
    public GameObject _gameObject;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_gameObject);
        }
        else if (_instance != this)
            Destroy(_instance.gameObject);
        Init();
    }   
    public override void Init()
    {
        base.Init();
    }
}
