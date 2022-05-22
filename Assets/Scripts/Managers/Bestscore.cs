using UnityEngine;
using System.IO;

public class Data
{
    public ulong bestScore;
    public Data(ulong score = 0)
    {
        this.bestScore = score;
    }
}

public class Bestscore
{

    public static void Save()
    {
        Data data = new Data(Managers.Data.BEST_SCORE);
        string path = $"{Application.dataPath}/SaveData.json";

#if UNITY_ANDROID
        path = $"{Application.persistentDataPath}/SaveData.json";
#endif

        string jsonData = JsonUtility.ToJson(data, true); //json으로 변환
        System.IO.File.WriteAllText(path, jsonData.ToString()); // json을 string으로 변환해 파일에 입력
    }

    public static Data Load()
    {
        Data data;
        string path = $"{Application.dataPath}/SaveData.json";

#if UNITY_ANDROID
        path = $"{Application.persistentDataPath}/SaveData.json";
#endif
        if (File.Exists(path)) {
            string jsonString = System.IO.File.ReadAllText(path);
            data = JsonUtility.FromJson<Data>(jsonString);
        }
        else
        {
            //세이브파일이 없는경우
            data = new Data();
        }

        return data;
    }
}
