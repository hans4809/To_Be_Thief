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


    public static GameManager Game { get { return Instance._game; } }
    public static UI_Manager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static DataManager Data { get { return Instance._data; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PoolManager Pool { get { return Instance._pool; } }

    void Start()
    {
        Init();
        StartCoroutine(WaitForDataLoaing());
    }
    public IEnumerator WaitForDataLoaing()
    {
        Coroutine cor1 = StartCoroutine(_data.LoadData());
        yield return cor1;
        foreach (var temp in _data.itemList)
        {
            Define.ItemKey itemKey = new Define.ItemKey(temp.itemType, temp.level, temp.isDebuff);
            Define.ItemData itemData = new Define.ItemData(temp.effect, temp.itemName, temp.itemExplain);
            _data.itemDict.Add(itemKey, itemData);
        }
        for(int i = 0; i < (int)Define.ItemType.MaxCount; i++)
        {
            Define.ItemKey itemKey = new Define.ItemKey((Define.ItemType)i, 1, true);
            _data.currentLevel.Add((Define.ItemType)i, 1);
            _data.currentStat.Add((Define.ItemType)i, _data.itemDict[itemKey].effect);
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
        s_instance._data.Init();
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
