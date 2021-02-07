using System;
using System.IO;
using UnityEngine;

public class JsonWorker
{
    private string path = Application.streamingAssetsPath + "/Config";

    public void SaveFile<T>(T serializeObject, string fileName)
    {
        var json = JsonUtility.ToJson(serializeObject, true);

        var fullPath = Path.Combine(path, fileName);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.WriteAllText(fullPath, json);
    }

    public bool IsFileExist(string fileName)
    {
        var fullPath = Path.Combine(path, fileName);

        return File.Exists(fullPath);
    }

    public T Deserialize<T>(string fileName)
    {
        if (!IsFileExist(fileName))
            throw new Exception("File is not exist");

        var fullPath = Path.Combine(path, fileName);

        var text = File.ReadAllText(fullPath);

        return JsonUtility.FromJson<T>(text);
    }
}
