using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CutScene : UI_Popup
{
    public string getText0, getText1; // 텍스트를 입력할 칸
    public TMP_Text targetText; //천천히 출력할 텍스트 변수선언
    private float delay = 0.15f; // 천천히 출력되는 시간
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        text = targetText.text.ToString(); // 적힌 텍스트를 받아온다
        targetText.text = " "; // 그 후 초기화한다.
        StartCoroutine(textPrint(delay)); //텍스트 프린트 시작
    }
    public override void Init() // CutScene 출력과정에서 우선 안썼습니다 2024-03-18
    {
        base.Init();
        StartCoroutine(CutScene());
    }
    IEnumerator CutScene()
    {
        yield return new WaitForSeconds(5f);

        Managers.UI.ClosePopUpUI();
    }
    // Update is called once per frame

    // 텍스트를 천천히 출력하는 함수
    IEnumerator textPrint(float d)
    {
        
        for (int i = 0; i < 3; i++)
        {
            int count = 0; // 초기화
            while (count != text.Length) // 모두출력할때까지 다른행위 못함.
            {
                if (count < text.Length)
                {
                    targetText.text += text[count].ToString(); // 텍스트 천천히 출력중
                    count++;
                }

                yield return new WaitForSeconds(delay); // 출력속도 조절
            }
            for(int j=0;j<4;j++) // 모두 출력뒤 잠시 딜레이
            {
                yield return new WaitForSeconds(delay * 2);
            }
          
            targetText.text = " "; // 초기화
            if (i == 0) text = getText0; // 입력값 받기
            else if (i == 1) text = getText1; // 입력값 받기
        }
        Managers.UI.ClosePopUpUI(); //모두 출력하면 팝업종료
    }
    void Update()
    {
        
    }
}
