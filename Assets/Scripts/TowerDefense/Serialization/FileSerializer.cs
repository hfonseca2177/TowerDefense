using System;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;

namespace TowerDefense.Serialization
{
    /// <summary>
    /// File serializer definition
    /// </summary>
    [CreateAssetMenu(fileName = "_FileSerializationDefinition", menuName = "TOWER_DEFENSE/Serialization/File Serialization Definition")]
    public class FileSerializer : ScriptableObject, IFileHandler
    {
        public string DirectoryName = "data";
        public string FileName = "file";
        public string FileExtension = ".data";
        public string VersionDirectoryName = "dataVersion";
        public bool UseVersioning;
        public bool DebugEnabled;

        public void Write(SerializableData data)
        {
            string path = GetFullPath();
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path,json);
            if(DebugEnabled) Debug.Log($"FILE CREATED: {path}");
            if(UseVersioning) SaveVersion(json);
        }
        
        public void Read<T>(out T data)
        {
            string path = GetFullPath();
            try
            {
                string json = File.ReadAllText(path);
                if(DebugEnabled) Debug.Log($"READING FILE: {path}");
                data = JsonUtility.FromJson<T>(json);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"FILE NOT FOUND: {path}");
                data = default;
            }
        }
        
        private string GetOrCreateDirectoryPath(string folderName)
        {
            string directoryPath = Path.Combine( Application.persistentDataPath , folderName);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }
        private string GetFileName(string fileName, string extension)
        {
            return string.Concat(fileName, extension);
        }
        
        private string GetFullPath()
        {
            string fileName = GetFileName(FileName, FileExtension);
            string rootPath = GetOrCreateDirectoryPath(DirectoryName);
            return Path.Combine(rootPath, fileName);
        }

        private string ConcatTimeStamp(string fileName)
        {
            return string.Concat(fileName,
                Regex.Replace(DateTime.Now.ToString("s"), "[:]", string.Empty));
        }

        private void SaveVersion(string json)
        {
            string fileName = GetFileName(ConcatTimeStamp(FileName), FileExtension);
            string rootPath = GetOrCreateDirectoryPath(VersionDirectoryName);
            string path = Path.Combine(rootPath, fileName);
            if(DebugEnabled) Debug.Log($"VERSION CREATED: {path}");
            File.WriteAllText(path,json);
        }
    }
}