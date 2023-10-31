using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager
{
    /*'
        구글 스프레드 시트 URL로 tsv파일(공란이 탭하고 엔터로 구분됨)을 가져오는 거,
        URL = "구글 스프레드 시트 URL 중 /edit 앞까지" + "export?format= 어떤 형식으로 받을 것인지" + "&gid=시트 번호(스프레드 시트 뒤쪽에 나옴)" + "range=복사해올 범위"
    */
    const string ADDRESS = "https://docs.google.com/spreadsheets/d/1WZj4mF3424Ta28-ENZQJycE76xYHYTHQKQ14rxHVgjo";
    const string RANGE = "A2:D";
    const long SHEET_ID = 0;
    /*
        key -> 스프레드시트 주제
        value -> 스프레드시트 데이터 (처음엔 링크)
    */
    private Dictionary<Type, string> sheetDatas = new Dictionary<Type, string>();
    public void Init()
    {
        sheetDatas.Add(typeof(Define.Items), GetTSVAddress(ADDRESS, RANGE, SHEET_ID));
    }

    public static string GetTSVAddress(string address, string range, long sheetID = 0)
    {
        if(sheetID == 0)
        {
            return $"{address}/export?format=tsv&range={range}";
        }
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }
    public IEnumerator LoadData()
    {
        List<Type> sheetTypes = new List<Type>(sheetDatas.Keys);

        foreach (Type type in sheetTypes)
        {
            UnityWebRequest www = UnityWebRequest.Get(sheetDatas[type]);
            yield return www.SendWebRequest();

            sheetDatas[type] = www.downloadHandler.text;
        }
    }
    T GetData<T>(string[] datas, string childType = "")
    {
        // T타입으로 인스턴스 생성
        object data;
    
        if (string.IsNullOrEmpty(childType) || Type.GetType(childType) == null)
        {
            data = Activator.CreateInstance(typeof(T));
        }
        else
        {
            data = Activator.CreateInstance(Type.GetType(childType));
        }
            // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

                    // 변수에 맞는 자료형으로 파싱해서 넣는다
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));

                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));

                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(datas[i]));

                else if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);

                    // enum
                else
                    fields[i].SetValue(data, Enum.Parse(type, datas[i]));
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message}");
            }
        }

            return (T)data;
    }
    List<T> GetDatas<T>(string data)
    {
        List<T> returnList = new List<T>();
        string[] splitedData = data.Split('\n');

        foreach (string element in splitedData)
        {
            string[] datas = element.Split('\t');
            returnList.Add(GetData<T>(datas));
        }

        return returnList;
    }

    List<T> GetDatasAsChildren<T>(string data)
    {
        List<T> returnList = new List<T>();
        string[] splitedData = data.Split('\n');

        foreach (string element in splitedData)
        {
            string[] datas = element.Split('\t');
            returnList.Add(GetData<T>(datas, datas[0]));
        }
        return returnList;
    }
}
