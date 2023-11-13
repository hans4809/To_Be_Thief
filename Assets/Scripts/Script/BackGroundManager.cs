using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public GameObject[] Prefabs;

    GameObject[] mapCCTV;
    GameObject[] mapRock;
    GameObject[] mapSpike;

    void Awake()
    {
        mapCCTV = new GameObject[5];
        mapRock = new GameObject[5];
        mapSpike = new GameObject[5];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < 5; i++)
        {
            mapCCTV[i] = Instantiate(Prefabs[0], transform);
            mapCCTV[i].SetActive(false);
            mapRock[i] = Instantiate(Prefabs[1], transform);
            mapRock[i].SetActive(false);
            mapSpike[i] = Instantiate(Prefabs[2], transform);
            mapSpike[i].SetActive(false);
        }
    }

    public GameObject MakeMap(int type)
    {
        Debug.Log("진입확인2");
        if (type == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!mapCCTV[i].activeSelf)
                {
                    mapCCTV[i].SetActive(true);
                    Debug.Log("맵1 실행확인");
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
                    Debug.Log("맵2 실행확인");
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
                    Debug.Log("맵3 실행확인");
                    return mapSpike[i];
                }
            }
        }

        Debug.Log("맵 생성실패 확인");
        return null;
    }
}

