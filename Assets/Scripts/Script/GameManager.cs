using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Score = 0;
    

    void Update()
    {
    }

    bool CanPopUpUI_ItemButton()
    {
        int notLevel3 = 0;
        foreach(var var in Managers.Data.CurrentLevel)
        {
            if(var.Value != 3)
            {
                notLevel3++;
            }
        }
        if (notLevel3 >= 3) return true;
        return false;
    }
}
