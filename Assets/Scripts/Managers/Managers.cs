using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    UI_Manager _ui = new UI_Manager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    InputManager _input = new InputManager();
    DataManager _data = new DataManager();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();
    GameManager _game = new GameManager();
    JsonManager _json = new JsonManager();
    GoogleSheetManager _googleSheet = new GoogleSheetManager();


    public static GameManager Game { get { return Instance._game; } }
    public static UI_Manager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static DataManager Data { get { return Instance._data; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static GoogleSheetManager GoogleSheet {  get { return Instance._googleSheet; } }
    public static JsonManager Json { get { return Instance._json; } }

    void Start()
    {
        Init();
        StartCoroutine(WaitForDataLoaing());
    }
    public IEnumerator WaitForDataLoaing()
    {
        Coroutine cor1 = StartCoroutine(_googleSheet.LoadData());
        _data.patternDatas = _json.Load<Define.PatternDatas>();
        _data.gameData = _json.Load<Define.WholeGameData>();
        yield return cor1;
        foreach (var temp in _googleSheet.itemList)
        {
            Define.ItemKey itemKey = new Define.ItemKey(temp.itemIndex, temp.level, temp.isDebuff);
            Define.ItemData itemData = new Define.ItemData(temp.effect, temp.itemName, temp.itemExplain);
            _googleSheet.itemDict.Add(itemKey, itemData);
        }
        for(int i = 0; i < 7; i++)
        {
            Define.ItemKey itemKey = new Define.ItemKey(i, 1, true);
            _data.currentLevel.Add(i, 1);
            _data.currentStat.Add(i, _googleSheet.itemDict[itemKey].effect);
        }
        for(int i = 0; i< _googleSheet.scoreList.Count; i++)
        {
            _data.scoreDatas.Add(_googleSheet.scoreList[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
        _game.OnUpdate();
    }
    static void Init()
    {
        GameObject go = GameObject.Find("@Manager");
        if (go == null)
        {
            go = new GameObject { name = "@Manager" };
            go.AddComponent<Managers>();
        }
        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();

        s_instance._pool.Init();
        s_instance._sound.Init();
        s_instance._googleSheet.Init();
    }
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }

}
