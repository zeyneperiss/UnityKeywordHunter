using UnityEngine;
using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance;

    public List<SEOScenario> scenarios;

    [HideInInspector]
    public SEOScenario selectedScenario;

    private void Awake()
    {
        // Singleton setup
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

        SelectRandomScenario();
    }

    public void SelectRandomScenario()
    {
        if (scenarios == null || scenarios.Count == 0)
        {
            Debug.LogError("Senaryo bulunamadı!");
            return;
        }

        selectedScenario = scenarios[Random.Range(0, scenarios.Count)];
        Debug.Log("Seçilen senaryo: " + selectedScenario.title);
    }
}
