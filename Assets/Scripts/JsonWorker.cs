using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using UnityEngine;

public static class JsonWorker
{
    private static string path = Application.persistentDataPath + "/Config";

    public static void SaveFile<T>(T serializeObject, string fileName)
    {
        var json = JsonUtility.ToJson(serializeObject, true);

        var fullPath = Path.Combine(path, fileName);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.WriteAllText(fullPath, json);
    }

    public static T Deserialize<T>(string fileName)
    {
        var fullPath = Path.Combine(path, fileName);

        if (!File.Exists(fullPath))
            throw new Exception("File is not exist");

        var text = File.ReadAllText(fullPath);

        return JsonUtility.FromJson<T>(text);
    }


}
