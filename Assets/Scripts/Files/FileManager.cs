using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public static FileManager instance;
    [SerializeField] string basePath = "D:/My Folder/GitHubProjects/LegendsOfEndalor2Dv.0.1a/Assets/Resources/";

    private void Start()
    {
        instance = GetComponent<FileManager>();
    }

    /// <summary>
    /// writes text to a file located in the base directory (Resources)
    /// </summary>
    /// <param name="filename"> only file name without extension</param>
    /// <param name="text"></param>
    public void WriteToFile(string filename, string text)
    {
        File.WriteAllText(basePath + filename + ".txt", text);
    }

    /// <summary>
    /// reads text from file located in the base directory (Resources)
    /// </summary>
    /// <param name="filename"> only file name without extension</param>
    /// <returns></returns>
    public string ReadFromFile(string filename)
    {
        string data = File.ReadAllText(basePath + filename + ".txt");
        return data;
    }
}
