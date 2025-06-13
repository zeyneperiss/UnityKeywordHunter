using UnityEngine;
public static class GameData
{
    public static bool siteAIsPlaying = true;

    public static bool siteAResult = false;
    public static bool siteBResult = false;

    public static bool siteAPlayed = false;
    public static bool siteBPlayed = false;

    public static bool resolveRoundOnNextLoad = false;

    public static SEOScenario siteAScenario;
    public static SEOScenario siteBScenario;

    public static int currentSlotLevel = 0; 

    public static float siteACompletionTime;
    public static float siteBCompletionTime;

    public static bool infoShownOnce = false;

    
    public static string[] roundResults = new string[4]; // 4 turun sonucu tutulacak
    public static bool[] siteAWins = new bool[4];
    public static string finalWinnerText = "";

}
