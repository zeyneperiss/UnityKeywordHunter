using UnityEngine;
using TMPro;
using System.IO;

public class InfoDataLoader : MonoBehaviour
{
    [System.Serializable]
    public class InfoData
    {
        public string title;
        public string body;
    }

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;

    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "seo_info.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InfoData data = JsonUtility.FromJson<InfoData>(json);

            titleText.text = data.title;
            bodyText.text = data.body;
        }
        else
        {
            Debug.LogError("seo_info.json bulunamadı! Yol: " + path);
        }
    }
}