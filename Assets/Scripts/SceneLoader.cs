using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSiteEditScene(int slotNumber)
    {
        GameData.siteAIsPlaying = (slotNumber % 2 == 0);

        // 🔥 Rastgele senaryo seç
        if (ScenarioManager.Instance != null && ScenarioManager.Instance.allScenarios.Count > 0)
        {
            var randomIndex = Random.Range(0, ScenarioManager.Instance.allScenarios.Count);
            var selected = ScenarioManager.Instance.allScenarios[randomIndex];

            if (GameData.siteAIsPlaying)
                GameData.siteAScenario = selected;
            else
                GameData.siteBScenario = selected;
        }

        // Edit sahnesine geç
        SceneManager.LoadScene("SiteEditScene");
    }
}