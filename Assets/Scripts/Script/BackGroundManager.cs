using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public GameObject[] Prefabs;

    GameObject[] Map_Round_1;
    GameObject[] Map_Round_2;
    GameObject[] Map_Round_3;

    void Awake()
    {
        Map_Round_1 = new GameObject[5];
        Map_Round_2 = new GameObject[5];
        Map_Round_3 = new GameObject[5];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < 5; i++)
        {
            Map_Round_1[i] = Instantiate(Prefabs[0], transform);
            Map_Round_1[i].SetActive(false);
            Map_Round_2[i] = Instantiate(Prefabs[1], transform);
            Map_Round_2[i].SetActive(false);
            Map_Round_3[i] = Instantiate(Prefabs[2], transform);
            Map_Round_3[i].SetActive(false);
        }
    }

    public GameObject MakeMap(int type)
    {
    
        if (type == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!Map_Round_1[i].activeSelf)
                {
                    Map_Round_1[i].SetActive(true);
                    return Map_Round_1[i];
                }
            }
        }

        if (type == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!Map_Round_2[i].activeSelf)
                {
                    Map_Round_2[i].SetActive(true);
                    return Map_Round_2[i];
                }
            }
        }

        if (type == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!Map_Round_3[i].activeSelf)
                {
                    Map_Round_3[i].SetActive(true);
                    return Map_Round_3[i];
                }
            }
        }

        return null;
    }
}

