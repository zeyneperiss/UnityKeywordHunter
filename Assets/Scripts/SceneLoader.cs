using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    public void LoadSiteEditScene(int slotNumber)
    {
        GameData.siteAIsPlaying = (slotNumber % 2 == 0);

        if (ScenarioManager.Instance != null && ScenarioManager.Instance.allScenarios.Count > 0)
        {
            var all = ScenarioManager.Instance.allScenarios;
            var used = ScenarioManager.Instance.usedScenarios;

            // Kullanılmayanlardan filtrele
            var available = all.Except(used).ToList();

            // Eğer yeterli yoksa sıfırla
            if (available.Count == 0)
            {
                used.Clear();
                available = new List<SEOScenario>(all);
                Debug.Log(" Senaryo havuzu sıfırlandı.");
            }

            
            SEOScenario selected = available[Random.Range(0, available.Count)];

            // player 1  mi oynuyor, player 2 mi?
            if (GameData.siteAIsPlaying)
            {
                GameData.siteAScenario = selected;
                Debug.Log(" Player 1  senaryosu: " + selected.title);
            }
            else
            {
                // Aynı senaryoyu verme
                while (selected == GameData.siteAScenario && available.Count > 1)
                {
                    selected = available[Random.Range(0, available.Count)];
                }

                GameData.siteBScenario = selected;
                Debug.Log(" Player 2 senaryosu: " + selected.title);
            }

            used.Add(selected); // seçilen senaryoyu listeye ekle
        }
        else
        {
            Debug.LogError("Senaryo seçilemedi!");
        }

        SceneManager.LoadScene("SiteEditScene");
    }
}