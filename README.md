# 대도가 되는 가장 완벽한 방법
![appicon_ver1 (3)](https://github.com/user-attachments/assets/918614c5-a6f1-40e1-85a2-f94f6cbb073e)


# 홍익대학교 ExP Make 2023 2학기 학기 중 프로젝트 대도가 되는 가장 완벽한 방법
Unity / 2D / 아케이드

## 역할 분담 🧑‍💻
### 개발 인원 : 9명 [ExP Make 2023 2학기 학기 중 팀프로젝트]
| 이름 | 개인 역할 | 담당 역할 및 기능 |
| ------ | ---------- | ------ |
| 김정현 | 기획 | 메인 기획자 |
| 유재영 | 기획 | 서브 기획자 |
| 김한세 | 아트 | 디자이너 |
| 김정현 | 아트 | 디자이너 |
| 최예진 | 아트 | 디자이너 |
| 한효빈 | Developer | 개발 |
| 김대윤 | Developer | 개발 |
| 박유정 | Developer | 개발 |
| 황운서 | 사운드 | 사운드 |

## 개발 기간 📅
2023.10 ~ 2024. 12

## 시연영상 
#### ⬇ Link Here ⬇
https://youtu.be/4PYj3PyUdYQ
 
## 기술 스택 💻
<img src="https://img.shields.io/badge/Unity-FFFFFF?style=for-the-badge&logo=Unity&logoColor=black">
<img src="https://img.shields.io/badge/csharp-512BD4?style=for-the-badge&logo=csharp&logoColor=white">

## 구현 내용

### 구글 스프레드 시트를 활용한 데이터 관리
```{cpp}
 public class ItemKeyComparer : IEqualityComparer<ItemKey>
{
    bool IEqualityComparer<ItemKey>.Equals(ItemKey x, ItemKey y)
    {
        return x.itemIndex == y.itemIndex && x.level == y.level && x.isDebuff == y.isDebuff;
    }

    public int GetHashCode(ItemKey obj)
    {
        return obj.itemIndex.GetHashCode() ^ obj.level.GetHashCode() ^ obj.isDebuff.GetHashCode();
    }
}
public class GoogleSheetManager
{
    // 읽어올 구글스프레드시트 주소
    protected const string ADDRESS = "https://docs.google.com/spreadsheets/d/1WZj4mF3424Ta28-ENZQJycE76xYHYTHQKQ14rxHVgjo";
    // 해당 구글스프레드시트의 AppScript 주소
    protected const string SaveADDRESS = "https://script.google.com/macros/s/AKfycbyzwhIItQkkamKuxFhcMIaEr-uIjx1abNgQF1-qHQLzW4jGQpObv38ziHHw8ZdW4YnYwQ/exec";
    // 구글 스프레드 시트 주소에서 아이템 데이터를 읽어올 행의 범위
    protected const string ItmeRANGE = "A2:F";
    // 구글 스프레드 시트 주소에서 게임 데이터를 읽어올 행의 범위
    protected const string GameDataRANGE = "A2:B";
    // 첫 번째 시트의 SHEET_ID는 0인데 그 다음 부터는 SHEET마다 ID가 붙어서 필요함
    protected const long SHEET_ID = 55073727;
    /*
        Key -> 해당 스프레드 시트에서 읽어올 데이터를 정의해둔 타입
        Value -> 해당 스프레드 시트에서 읽어온 데이터들을 쭉 나열
    */
    protected Dictionary<Type, string> sheetDatas = new Dictionary<Type, string>();
    public List<ItemTable> itemList = new List<ItemTable>();
    public List<ScoreData> scoreList = new List<ScoreData>();
    // 아이템 데이터의 기본키가 ItemIndex, level, isDebuff 3개가 필요해서 Struct로 묶어서 KEY값 설정
    public Dictionary<ItemKey, ItemData> itemDict = new Dictionary<ItemKey, ItemData>(new ItemKeyComparer());
    public virtual void Init() 
    {
        // 처음에 각 데이터를 읽어올 주소를 일단 SheetDatas에 넣음
        if(!sheetDatas.ContainsKey(typeof(ItemTable)))
        {
            sheetDatas.Add(typeof(ItemTable), GetTSVAddress(ADDRESS, ItmeRANGE));
        }
        if (!sheetDatas.ContainsKey(typeof(ScoreData)))
        {
            sheetDatas.Add(typeof(ScoreData), GetTSVAddress(ADDRESS, GameDataRANGE, SHEET_ID));
        }
    }
    /// <summary>
    /// 함수 이름 : GetTSVAddress
    /// 기능 : 구글스프레드시트 주소에서 TSV파일을 긁어오기 위한 주소를 만듬
    /// 반환 값 : TSV파일을 긁어오기 위한 주소
    /// </summary>
    /// <param name="address"></param> 기본 스프레드시트 주소
    /// <param name="range"></param> 읽어올 범위
    /// <param name="sheetID"></param> 시트 아이디, 첫번 째 시트일때는 ID가 필요 없으므로 default값을 0으로 설정
    /// <returns></returns>
    public static string GetTSVAddress(string address, string range, long sheetID = 0)
    {   
        if(sheetID == 0)
        {
            return $"{address}/export?format=tsv&range={range}";
        }
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }
    /// <summary>
    /// 함수 이름 : LoadData
    /// 기능 : 구글스프레드 시트에서 읽어온 문자열을 파싱해서 각 리스트에 저장
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadData()
    {
        List<Type> sheetTypes = new List<Type>(sheetDatas.Keys);
        // 각 데이터 타입 마다 실행
        foreach (Type type in sheetTypes)
        {
            // 해당 주소로 request보냄
            UnityWebRequest www = UnityWebRequest.Get(sheetDatas[type]);
            // request보내고 응답 받을 동안은 유니티 상의 코드 실행
            yield return www.SendWebRequest();

            if (www.isDone)
                Debug.Log(www.downloadHandler.text);
            // 오류 났을 때 예외 처리
            else
            {
                Debug.Log(www.error);
                yield return null;
            }
            // sheeDatas에 주소를 받은 응답의 text로 변경 
            sheetDatas[type] = www.downloadHandler.text;

            // 데이터 타입에 맞게 파싱해서 리스트에 저장
            if(type == typeof(ItemTable))
            {
                itemList = GetDatas<ItemTable>(sheetDatas[type]);
            }
            if(type == typeof(ScoreData))
            {
                scoreList = GetDatas<ScoreData>(sheetDatas[type]);
            }
        }
    }
    /// <summary>
    /// 함수 이름 : GetData
    /// 기능 : 파싱한 데이터를 데이터 타입에 맞게 반환
    /// </summary>
    /// <typeparam name="T"></typeparam> T 타입의 데이터
    /// <param name="datas"></param> 파싱한 데이터
    /// <param name="childType"></param> 
    /// <returns></returns>
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
    /// <summary>
    /// 함수 이름 : GetDatas
    /// 기능 : 스프레드시트에서 읽어온 스트링을 파싱해서 List로 반환
    /// </summary>
    /// <typeparam name="T"></typeparam> 반환할 리스트의 데이터 타입
    /// <param name="data"></param> 파싱할 스트링
    /// <returns></returns>
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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
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
