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
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game { get { return Instance._game; } }
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
        for (int i = 0; i < _data.items.Count; i++)
        {
            _data.CurrentLevel.Add(_data.items[i].itemType, 1);
            _data.CurrentStat.Add(_data.items[i].itemType, _data.items[i].level_1);
        }
        _data.CurrentLevel[Define.ItemType.Player_speed] = 3;
        //_data.CurrentLevel[Define.ItemType.Player_HitBox] = 3;
        //_data.CurrentLevel[Define.ItemType.CCTV_Time] = 3;
        _data.CurrentLevel[Define.ItemType.Thorn_Steps] = 3;
        _data.CurrentLevel[Define.ItemType.Rock_SpawnTime] = 3;
        //_data.CurrentLevel[Define.ItemType.Rock_Speed] = 3;
        //_data.CurrentLevel[Define.ItemType.Rock_HitBox] = 3;
    }
    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
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
