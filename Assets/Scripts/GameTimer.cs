using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 30f;
    private float currentTime;
    private bool isRunning = false;
    private bool hasEnded = false; // 🔹 Süre bir kez bittiğinde flag

    public TextMeshProUGUI timerText;
    public Button startButton;
    public Button finishButton;
    public GameObject keywordPanel;

    private void Start()
    {
        currentTime = totalTime;
        keywordPanel.SetActive(false); // Başlangıçta keyword’ler gizli
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            int seconds = Mathf.Clamp(Mathf.CeilToInt(currentTime), 0, 999);
            timerText.text = " " + seconds.ToString();

            if (currentTime <= 0 && !hasEnded)
            {
                isRunning = false;
                hasEnded = true;

                Debug.Log("⏰ Süre bitti! Otomatik olarak bitiriliyor...");

                // Finish butonuna otomatik tıklama
                if (finishButton != null)
                {
                    finishButton.onClick.Invoke();
                }
                else
                {
                    Debug.LogWarning("FinishButton atanmadı!");
                }
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
        startButton.interactable = false;
        keywordPanel.SetActive(true); // Keyword'leri göster
    }
}