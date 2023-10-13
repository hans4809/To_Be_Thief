using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Score = 0;

    public Text UIScore;

    void Update()
    {
        UIScore.text = "ÇöÀç Ä­ ¼ö : " + Score.ToString();
    }
}
