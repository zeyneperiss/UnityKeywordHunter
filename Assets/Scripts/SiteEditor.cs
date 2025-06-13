using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SiteEditor : MonoBehaviour
{
    public TextMeshProUGUI siteLabel;
    public TextMeshProUGUI scenarioTitleText;
    public Button finishButton;
    public GameObject keywordTemplate;
    public Transform keywordPanel;
    public GameObject[] allKeywords;
    public AudioClip finishClickSound;        
    public AudioSource audioSource;          

    public void PlayFinishClickSound()
    {
        if (audioSource != null && finishClickSound != null)
            audioSource.PlayOneShot(finishClickSound, 0.7f); 
    }
    private void Start()
    {
        Debug.Log("SiteEditor Start");

        // player1 mi player2 mi düzenleniyor göstergesi
        siteLabel.text = GameData.siteAIsPlaying ? "Site A düzenleniyor..." : "Site B düzenleniyor...";

        finishButton.onClick.AddListener(OnFinishGame);

        // Senaryo başlığı ilk açıldığında gizli olucak
        if (scenarioTitleText != null)
        {
            scenarioTitleText.enabled = false;
        }

        // Senaryoyu al
        var scenario = GameData.siteAIsPlaying ? GameData.siteAScenario : GameData.siteBScenario;
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

        keywordTemplate.SetActive(true); // template gizlenir
    }

    
    public void StartGame()
    {
        var scenario = GameData.siteAIsPlaying ? GameData.siteAScenario : GameData.siteBScenario;

        if (scenario != null && scenarioTitleText != null)
        {
            scenarioTitleText.text = scenario.title;
            scenarioTitleText.enabled = true;
        }

        Debug.Log(" Oyun başladı, senaryo başlığı gösterildi.");
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
            GameData.siteACompletionTime = Time.timeSinceLevelLoad;
        }
        else
        {
            GameData.siteBResult = won;
            GameData.siteBPlayed = true;
            GameData.siteBCompletionTime = Time.timeSinceLevelLoad;
        }

        Debug.Log("Kazanan (bu tur): " + (won ? "Kazandı" : "Kaybetti"));
        GameData.resolveRoundOnNextLoad = true;

        SceneManager.LoadScene("MainScene");
    }
}