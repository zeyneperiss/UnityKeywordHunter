using UnityEngine;

[System.Serializable]
public class KeywordInfo
{
    public string text;
    public string correctDropArea;
}

[CreateAssetMenu(fileName = "New SEO Scenario", menuName = "SEO/Scenario")]
public class SEOScenario : ScriptableObject
{
    public string title;
    public KeywordInfo[] keywords;
}

