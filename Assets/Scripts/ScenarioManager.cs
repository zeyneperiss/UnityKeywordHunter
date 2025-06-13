using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance;

    public List<SEOScenario> allScenarios;

    public List<SEOScenario> usedScenarios = new List<SEOScenario>(); 

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
        List<SEOScenario> available = allScenarios.Except(usedScenarios).ToList();

        if (available.Count < 2)
        {
            usedScenarios.Clear();
            available = new List<SEOScenario>(allScenarios);
            Debug.Log(" Kullanılan senaryolar sıfırlandı.");
        }

        int indexA = Random.Range(0, available.Count);
        SEOScenario scenarioA = available[indexA];

        SEOScenario scenarioB;
        do
        {
            scenarioB = available[Random.Range(0, available.Count)];
        } while (scenarioB == scenarioA);

        GameData.siteAScenario = scenarioA;
        GameData.siteBScenario = scenarioB;

        usedScenarios.Add(scenarioA);
        usedScenarios.Add(scenarioB);

        Debug.Log("Player 1 senaryosu: " + scenarioA.title);
        Debug.Log("Player 2 senaryosu: " + scenarioB.title);
    }
}