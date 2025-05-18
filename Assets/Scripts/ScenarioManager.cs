using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance;

    public List<SEOScenario> allScenarios;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // JSON'dan senaryoları yükle
        allScenarios = JsonScenarioLoader.LoadScenariosFromJson();

        if (allScenarios.Count < 2)
        {
            Debug.LogError("En az 2 senaryo JSON'dan yüklenmiş olmalı!");
        }
        else
        {
            SelectTwoUniqueScenarios();
        }
    }

    private void SelectTwoUniqueScenarios()
    {
        int indexA = Random.Range(0, allScenarios.Count);
        int indexB;

        do
        {
            indexB = Random.Range(0, allScenarios.Count);
        }
        while (indexB == indexA);

        GameData.siteAScenario = allScenarios[indexA];
        GameData.siteBScenario = allScenarios[indexB];

        Debug.Log("✅ Site A senaryosu: " + GameData.siteAScenario.title);
        Debug.Log("✅ Site B senaryosu: " + GameData.siteBScenario.title);
    }
}