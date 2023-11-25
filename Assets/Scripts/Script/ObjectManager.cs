using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject[] Prefabs;

    GameObject[] mapCCTV;
    GameObject[] mapRock;
    GameObject[] mapSpike;
    GameObject[] Break;
    void Awake()
    {
        mapCCTV = new GameObject[5];
        mapRock = new GameObject[5];
        mapSpike = new GameObject[5];
        Break = new GameObject[5];
        Generate();
    }

    void Generate()
    {
        for(int i = 0; i<5; i++)
        {
            mapCCTV[i] = Instantiate(Prefabs[0],transform);
            mapCCTV[i].SetActive(false);
            mapRock[i] = Instantiate(Prefabs[1],transform);
            mapRock[i].SetActive(false);
            mapSpike[i] = Instantiate(Prefabs[2],transform);
            mapSpike[i].SetActive(false);
            Break[i]=Instantiate(Prefabs[3], transform);
            Break[i].SetActive(false);
        }
    }

    public GameObject MakeObj(int type)
    {
        if (type == 0)
        {
            for (int i = 0; i<5; i++)
            {
                if (!mapCCTV[i].activeSelf)
                {
                    mapCCTV[i].SetActive(true);
                    return mapCCTV[i];
                }
            }
        }

        if (type == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!mapRock[i].activeSelf)
                {
                    mapRock[i].SetActive(true);
                    return mapRock[i];
                }
            }
        }

        if (type == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!mapSpike[i].activeSelf)
                {
                    mapSpike[i].SetActive(true);
                    return mapSpike[i];
                }
            }
        }

        if(type==3)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!Break[i].activeSelf)
                {
                    Break[i].SetActive(true);
                    return Break[i];
                }
            }
        }

        return null;
    }
}
