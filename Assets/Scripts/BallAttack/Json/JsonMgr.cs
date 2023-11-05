using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public enum JsonType
{
    JsonUtility,
    LitJson
}

public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;
    private JsonMgr() { }
    public void SaveData(object data,string path,JsonType type = JsonType.LitJson)
    {
        string Path = Application.persistentDataPath + "/" + path + ".json";
        string Jsonstr = "";
        switch (type)
        {
            case JsonType.JsonUtility:
                Jsonstr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                Jsonstr = JsonMapper.ToJson(data);
                break;
            default:
                break;
        }
        File.WriteAllText(Path, Jsonstr);
    }
    public T LoadData<T>(string path, JsonType type = JsonType.LitJson) where T : new()
    {
        //����ȷ�� ����·�� ������
        string Path = Application.streamingAssetsPath + "/" + path + ".json";
        if (!File.Exists(Path))
        {
            Path = Application.persistentDataPath + "/" + path + ".json";
        }
        if (!File.Exists(Path))
        {
            return new T();
        }
        //�õ�����
        string Jsonstr = File.ReadAllText(Path);
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(Jsonstr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(Jsonstr);
                break;
            default:
                break;
        }
        return data;
    }
}
