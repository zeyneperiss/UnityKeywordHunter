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

    public static int currentSlotLevel = 0; // 0: zemin, 1: 1. kat, 2: 2. kat, ... max 3

    public static float siteACompletionTime;
    public static float siteBCompletionTime;

    public static bool infoShownOnce = false;
}
