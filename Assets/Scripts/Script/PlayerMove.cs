using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public ObjectManager ObjectManager;
    public BackGroundManager BackGroundManager;
    public GameManager GameManager;
    public int score;
    public int MapCode = 0; // 무슨 맵을 생성하느냐에 대한 변수 
    Rigidbody2D rigid;
    Animator anim;
    private int CountBreak,value = 0;
    TextAsset jsonAsset;
    PatternList patternList;
    int PatternRandomCode;

    private int[] PatternCode = new int[] { 0, 3, 2 }; // 임시 패턴 코드 현재 0-> CCTV, 3 -> 휴식 , 2 -> 톱니바퀴입니다. 

    public class PatternList { public List<Define.PatternData> patterns; } // jsonManager 통하지않고 값 받기
   


    public float Playersize_level;  // 크기 변수
    int patternY = 15;
    public float player_speed;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {



        Playersize_level = 1.3f;
        player_speed = 3f;
        transform.localScale = new Vector3(Playersize_level, Playersize_level, 1f); // 크기 설정
        /* 
         * 원래 유정님이 하신 것도 잘하셨습니다. 그런데 그냥 플레이어에 마우스 입력을 받아버리면
         * UI 클릭 할 때도 캐릭터 오브젝트가 있으면 마우스 입력이 되어버리는 문제가 발생할 수 있습니다.
         * 그래서 InputManger에서 인 게임 내에서 키 입력하고 마우스 입력을 모두 관리하는 편이 좋습니다.
         * InputManager를 보시면 KeyAction하고 MouseAction을 Action 값의 매개변수로 만들고
         * 여기 등록된 Event들을 호출하는 것입니다.
         * 밑에 보시면 MouseAction들에 누르고 있을 때 호출되는 함수와 땠을 때 호출되는 함수들을 등록한 겁니다.
         * Press하고 있을 때는 MousePress 함수가 호출 되고, PressEnd일 때는 MosuePressEnd함수가 호출 됩니다.
         * 이해 안 가시면 질문하세요
        */
        Managers.Input.MouseAction -= MousePress; 
        Managers.Input.MouseAction += MousePress;
        Managers.Input.MouseAction -= MousePressEnd;
        Managers.Input.MouseAction += MousePressEnd;
    }

    private void Update()
    {
        //Player Animation
        //if (Input.GetMouseButton(0))
        //    anim.SetBool("isWalking", false);
        //else
        //    anim.SetBool("isWalking", true);

        //Player Score
        //GameManager.Score = (int)transform.position.y / 2;
        //score = (int)transform.position.y / 2;
        //DeletePattern
        StartCoroutine(DeletePattern());

    }

    private void FixedUpdate()
    {
        //Player Move
        //if (!Input.GetMouseButton(0))
        if(Managers.Game.currentState == GameManager.GameState.Playing)
        {
            transform.position = transform.position + Vector3.up * player_speed * Time.deltaTime;
        }
    }
    void MousePress(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Press)
            return;
        anim.SetBool("isWalking", false);
        player_speed = 0.0f;
    }
    void MousePressEnd(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.End)
            return;
        anim.SetBool("isWalking", true);
        player_speed = 3f;//DataManager._instance.items[(int)Define.ItemType.Player_speed].level_1;
    }
    //Make pattern
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pattern")
        {
            
            if (CountBreak == 0)
            {
                PatternRandomCode = Random.Range(0,24);
                jsonAsset = Resources.Load<TextAsset>("Data/PatternDatas"); // jsonManager 안거치고 받기
                patternList = JsonUtility.FromJson<PatternList>(jsonAsset.text);  // jsonManager 안거치고 받기
                //데이터 받고 
                value = patternList.patterns[PatternRandomCode].firstObstacle;
                Debug.Log(value);
                GameObject newPattern_test = ObjectManager.MakeObj(value);
                newPattern_test.transform.position = new Vector3(0, patternY, 0);
                CountBreak++;
            }
            else if (CountBreak==1)
            {
                jsonAsset = Resources.Load<TextAsset>("Data/PatternDatas"); // jsonManager 안거치고 받기
                patternList = JsonUtility.FromJson<PatternList>(jsonAsset.text);  // jsonManager 안거치고 받기
                value = patternList.patterns[PatternRandomCode].secondObstacle;
                GameObject newPattern_test = ObjectManager.MakeObj(value);
                newPattern_test.transform.position = new Vector3(0, patternY, 0);
                CountBreak++;
            }

            else if (CountBreak==2)
            {
                jsonAsset = Resources.Load<TextAsset>("Data/PatternDatas"); // jsonManager 안거치고 받기
                patternList = JsonUtility.FromJson<PatternList>(jsonAsset.text);  // jsonManager 안거치고 받기
                value = patternList.patterns[PatternRandomCode].thirdObstacle;
                GameObject newPattern_test = ObjectManager.MakeObj(value);
                newPattern_test.transform.position = new Vector3(0, patternY, 0);
                CountBreak=0;
            }

            GameObject newBackGround = BackGroundManager.MakeMap(MapCode);  //맵 코드를 받아서 맵 생성 
            newBackGround.transform.position = new Vector3(0, patternY, 0);

            patternY += 6;
        }
    }

    //Coroutine Delete pattern
    IEnumerator DeletePattern()
    {
        GameObject[] patterns = GameObject.FindGameObjectsWithTag("pattern");
        for (int i = 0; i < patterns.Length; i++)
        {
            if (gameObject.transform.position.y - patterns[i].transform.position.y > 15)
            {
                patterns[i].SetActive(false);
            }
        }
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Objects.Length; i++)
        {
            if (gameObject.transform.position.y - Objects[i].transform.position.y > 15)
            {
                Objects[i].SetActive(false);
            }
        }

        yield return new WaitForSeconds(0.1f);
    }
    
    //GameOver
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ObstacleHitBox")
        {
            Debug.Log("�׾����ϴ�.");
            Managers.Game.PlayerDied();
        }

    }
}
