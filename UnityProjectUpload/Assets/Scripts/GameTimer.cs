using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 30f;
    private float currentTime;
    private bool isRunning = false;

    public TextMeshProUGUI timerText;
    public Button startButton;
    public Button finishButton;
    public GameObject keywordPanel; // Tüm keyword butonları bu panelde olabilir

    private void Start()
    {
        currentTime = totalTime;
        //finishButton.interactable = false;
        keywordPanel.SetActive(false); // Başlangıçta keyword’ler gizli
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            int seconds = Mathf.Clamp(Mathf.CeilToInt(currentTime), 0, 999);
            timerText.text = "Süre: " + seconds.ToString();

            if (currentTime <= 0)
            {
                isRunning = false;
                //finishButton.interactable = true;
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
