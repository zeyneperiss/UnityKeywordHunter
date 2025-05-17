using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SiteEditor : MonoBehaviour
{
    public TextMeshProUGUI siteLabel;
    public Button finishButton;
    public GameObject keywordTemplate;
    public Transform keywordPanel;
    public GameObject[] allKeywords;

    private void Start()
    {
        Debug.Log("SiteEditor Start");

        siteLabel.text = GameData.siteAIsPlaying ? "Site A düzenleniyor..." : "Site B düzenleniyor...";

        finishButton.onClick.AddListener(OnFinishGame);

        var scenario = ScenarioManager.Instance?.selectedScenario;
        if (scenario == null)
        {
            Debug.LogError("Senaryo bulunamadı!");
            return;
        }

        allKeywords = new GameObject[scenario.keywords.Length];

        for (int i = 0; i < scenario.keywords.Length; i++)
        {
            var keywordGO = Instantiate(keywordTemplate, keywordPanel);
            var text = keywordGO.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = scenario.keywords[i].text; // 🔥 Doğru alan

            var data = keywordGO.GetComponent<KeywordData>();
            if (data != null)
                data.correctDropArea = scenario.keywords[i].correctDropArea; // 🔥 Doğru alan

            allKeywords[i] = keywordGO;
        }

        keywordTemplate.SetActive(false);
    }

    public void OnFinishGame()
    {
        Debug.Log("OnFinishGame çalıştı");

        int correctCount = 0;
        foreach (var kw in allKeywords)
        {
            var data = kw.GetComponent<KeywordData>();
            if (data != null && data.IsCorrectlyDropped())
                correctCount++;
        }

        Debug.Log("Site A skoru: " + correctCount);

        bool siteAWon = correctCount >= 3;
        GameData.siteAWon = siteAWon;

        Debug.Log("Kazanan: " + (siteAWon ? "A" : "B"));

        SceneManager.LoadScene("MainScene");
    }
}
