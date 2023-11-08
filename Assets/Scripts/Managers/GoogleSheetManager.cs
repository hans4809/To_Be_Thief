using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager
{
    /*
        ���� �������� ��Ʈ URL�� tsv����(������ ���ϰ� ���ͷ� ���е�)�� �������� ��,
        URL = "���� �������� ��Ʈ URL �� /edit �ձ���" + "export?format= � �������� ���� ������" + "&gid=��Ʈ ��ȣ(�������� ��Ʈ ���ʿ� ����)" + "range=�����ؿ� ����"
    */
    protected const string ADDRESS = "https://docs.google.com/spreadsheets/d/1WZj4mF3424Ta28-ENZQJycE76xYHYTHQKQ14rxHVgjo";
    protected const string SaveADDRESS = "https://script.google.com/macros/s/AKfycbyzwhIItQkkamKuxFhcMIaEr-uIjx1abNgQF1-qHQLzW4jGQpObv38ziHHw8ZdW4YnYwQ/exec";
    protected const string ItmeRANGE = "A2:E";
    protected const string GameDataRANGE = "A2:B";
    protected const long SHEET_ID = 55073727;
    /*
        key -> ���������Ʈ ����
        value -> ���������Ʈ ������ (ó���� ��ũ)
    */
    protected Dictionary<Type, string> sheetDatas = new Dictionary<Type, string>();
    public List<Define.Items> items = new List<Define.Items>();
    public List<Define.GameData> gameDatas = new List<Define.GameData>();

    public virtual void Init() 
    {
        
        if(!sheetDatas.ContainsKey(typeof(Define.Items)))
        {
            sheetDatas.Add(typeof(Define.Items), GetTSVAddress(ADDRESS, ItmeRANGE));
        }
        if (!sheetDatas.ContainsKey(typeof(Define.GameData)))
        {
            sheetDatas.Add(typeof(Define.GameData), GetTSVAddress(ADDRESS, GameDataRANGE, SHEET_ID));
        }
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

            if (www.isDone)
                Debug.Log(www.downloadHandler.text);
            else
            {
                Debug.Log(www.error);
                yield return null;
            }
                
            sheetDatas[type] = www.downloadHandler.text;
            if(type == typeof(Define.Items))
            {
                items = GetDatas<Define.Items>(sheetDatas[type]);
            }
            if(type == typeof(Define.GameData))
            {
                gameDatas = GetDatas<Define.GameData>(sheetDatas[type]);
            }
        }
    }
    public IEnumerator LoadData(Type sheetTypes)
    {
        UnityWebRequest www = UnityWebRequest.Get(sheetDatas[sheetTypes]);
        yield return www.SendWebRequest();

        if (www.isDone)
            Debug.Log(www.downloadHandler.text);
        else
        {
            Debug.Log(www.error);
            yield return null;
        }

        sheetDatas[sheetTypes] = www.downloadHandler.text;
        if (sheetTypes == typeof(Define.Items))
        {
            items = GetDatas<Define.Items>(sheetDatas[sheetTypes]);
        }
        if (sheetTypes == typeof(Define.GameData))
        {
            gameDatas = GetDatas<Define.GameData>(sheetDatas[sheetTypes]);
        }
    }
    public IEnumerator SaveData(int MaxScore, int FirstPlay)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", MaxScore);
        form.AddField("firstPlay", FirstPlay);
        using(UnityWebRequest www = UnityWebRequest.Post(SaveADDRESS, form))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
                Debug.Log(www.downloadHandler.text);
            else
                Debug.Log("Error");
        }
    }
    protected T GetData<T>(string[] datas, string childType = "")
    {

        object data;
    
        if (string.IsNullOrEmpty(childType) || Type.GetType(childType) == null)
        {
            data = Activator.CreateInstance(typeof(T));
        }
        else
        {
            data = Activator.CreateInstance(Type.GetType(childType));
        }

        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

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
    protected List<T> DataToList<T>(string element)
    {
        List<T> returnList = new List<T>();
        string[] datas = element.Split('\t');
        returnList.Add(GetData<T>(datas));
        return returnList;
    }

    protected List<T> GetDatas<T>(string data)
    {
        List<T> returnList = new List<T>();
        string[] splitedData = data.Split('\n');

        foreach (string element in splitedData)
        {
            string[] datas = element.Split('\t');
            if(datas[0] == " "){ break; }
            returnList.Add(GetData<T>(datas));
        }
        return returnList;
    }
    
    protected List<T> GetDatasAsChildren<T>(string data)
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
