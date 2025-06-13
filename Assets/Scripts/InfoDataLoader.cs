using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class InfoDataLoader : MonoBehaviour
{
    [System.Serializable]
    public class PageData
    {
        public string title;
        public string body;
    }

    [System.Serializable]
    public class InfoData
    {
        public List<PageData> pages;
    }

    [Header("Page Text Targets")]
    public TextMeshProUGUI[] pageTitles;
    public TextMeshProUGUI[] pageBodies;

    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "seo_info.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InfoData data = JsonUtility.FromJson<InfoData>(json);

            for (int i = 0; i < data.pages.Count && i < pageTitles.Length; i++)
            {
                pageTitles[i].text = data.pages[i].title;
                pageBodies[i].text = data.pages[i].body;
            }

            Debug.Log(" InfoData başarıyla yüklendi.");
        }
        else
        {
            Debug.LogError(" seo_info.json bulunamadı! Yol: " + path);
        }
    }
}