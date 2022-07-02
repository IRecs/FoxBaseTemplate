﻿using System.IO;
using UnityEngine;

namespace Data
{
    public static class ObjectSaver
    {
        public static string GetSavingPathFile<TObject>(string savingId = "")
        {
            if (!Directory.Exists(Application.persistentDataPath + "/data/"))
                Directory.CreateDirectory(Application.persistentDataPath + "/data/");

            return Application.persistentDataPath + "/data/" + typeof(TObject).ToString() + savingId + ".json";
        }

        public static string GetSavingPathDirectory()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/data/"))
                Directory.CreateDirectory(Application.persistentDataPath + "/data/");

            return Application.persistentDataPath + "/data/";
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Edit/Clear All Json Files", false, 266)]
        public static void ClearAllFiles()
        {
            string directoryPath = GetSavingPathDirectory();
            if (Directory.Exists(directoryPath))
            {
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    File.Delete(file);
                }
            }

            Debug.Log("All Files Deleted path is: " + directoryPath);
        }
#endif
    }
}
