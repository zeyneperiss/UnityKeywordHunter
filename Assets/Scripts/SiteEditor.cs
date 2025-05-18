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

        // A mı B mi düzenleniyor göstergesi
        siteLabel.text = GameData.siteAIsPlaying ? "Site A düzenleniyor..." : "Site B düzenleniyor...";

        finishButton.onClick.AddListener(OnFinishGame);

        // Seçilen senaryoyu al
        var scenario = ScenarioManager.Instance?.selectedScenario;
        if (scenario == null)
        {
            Debug.LogError("Senaryo bulunamadı!");
            return;
        }

        // Anahtar kelimeleri sahneye yerleştir
        allKeywords = new GameObject[scenario.keywords.Length];

        for (int i = 0; i < scenario.keywords.Length; i++)
        {
            var keywordGO = Instantiate(keywordTemplate, keywordPanel);
            var text = keywordGO.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = scenario.keywords[i].text;

            var data = keywordGO.GetComponent<KeywordData>();
            if (data != null)
                data.correctDropArea = scenario.keywords[i].correctDropArea;

            allKeywords[i] = keywordGO;
        }

        keywordTemplate.SetActive(false); // template gizlenir
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

        bool won = correctCount >= 3;

        if (GameData.siteAIsPlaying)
        {
            GameData.siteAResult = won;
            GameData.siteAPlayed = true;
        }
        else
        {
            GameData.siteBResult = won;
            GameData.siteBPlayed = true;
        }

        Debug.Log("Kazanan (bu tur): " + (won ? "Kazandı" : "Kaybetti"));

        GameData.resolveRoundOnNextLoad = true;

        SceneManager.LoadScene("MainScene");
    }

}
