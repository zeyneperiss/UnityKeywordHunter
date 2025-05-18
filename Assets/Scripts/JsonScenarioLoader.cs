using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class KeywordEntryJson
{
    public string text;
    public string correctDropArea;
}

[Serializable]
public class ScenarioJson
{
    public string title;
    public List<KeywordEntryJson> keywords;
}

[Serializable]
public class ScenarioListJson
{
    public List<ScenarioJson> scenarios;
}

public static class JsonScenarioLoader
{
    public static List<SEOScenario> LoadScenariosFromJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "sample_scenarios.json");

        if (!File.Exists(path))
        {
            Debug.LogError("JSON dosyası bulunamadı! Path: " + path);
            return new List<SEOScenario>();
        }

        string json = File.ReadAllText(path);

        ScenarioListJson parsed = JsonUtility.FromJson<ScenarioListJson>(json);

        if (parsed == null || parsed.scenarios == null)
        {
            Debug.LogError("JSON içeriği çözümlenemedi.");
            return new List<SEOScenario>();
        }

        List<SEOScenario> scenarioList = new List<SEOScenario>();

        foreach (var scenarioJson in parsed.scenarios)
        {
            SEOScenario scenario = ScriptableObject.CreateInstance<SEOScenario>();
            scenario.title = scenarioJson.title;

            List<KeywordInfo> keywordInfos = new List<KeywordInfo>();
            foreach (var entry in scenarioJson.keywords)
            {
                keywordInfos.Add(new KeywordInfo
                {
                    text = entry.text,
                    correctDropArea = entry.correctDropArea
                });
            }

            scenario.keywords = keywordInfos.ToArray();
            scenarioList.Add(scenario);
        }

        Debug.Log($"✔ JSON'dan yüklenen senaryo sayısı: {scenarioList.Count}");
        return scenarioList;
    }
}