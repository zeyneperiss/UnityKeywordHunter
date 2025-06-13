using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioClip victoryClip;
    private AudioSource audioSource;

    public OfficeSlotDisplay[] slots;
    public GameObject winnerCharacterIcon;
    public GameObject scorePanel;
    public ScorePanelDisplay scorePanelDisplay;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (GameData.resolveRoundOnNextLoad)
        {
            if (GameData.siteAPlayed && GameData.siteBPlayed)
            {
                ResolveRound(GameData.siteAResult);
                GameData.siteAPlayed = false;
                GameData.siteBPlayed = false;
            }

            GameData.resolveRoundOnNextLoad = false;
        }
    }

    public void ResolveRound(bool siteAWon)
    {
        int currentLevel = GameData.currentSlotLevel;
        if (currentLevel >= 4) return;

        // Kazananı GameData'a kaydet
        GameData.siteAWins[currentLevel] = siteAWon;

        //  Önceki slotları sabit renkte yeniden boya
        for (int i = 0; i < currentLevel; i++)
        {
            int prevA = 6 - (i * 2);
            int prevB = 7 - (i * 2);

            if (GameData.siteAWins[i])
            {
                slots[prevA].SetWin(false); // sabit yeşil
                slots[prevB].SetLose(false); // sabit kırmızı
            }
            else
            {
                slots[prevA].SetLose(false);
                slots[prevB].SetWin(false);
            }
        }

        //  Bu turdaki slotları yanıp söndür
        int slotAIndex = 6 - (currentLevel * 2);
        int slotBIndex = 7 - (currentLevel * 2);

        if (siteAWon)
        {
            slots[slotAIndex].SetWin();   // yanıp sönsün
            slots[slotBIndex].SetLose();
        }
        else
        {
            slots[slotAIndex].SetLose();
            slots[slotBIndex].SetWin();
        }

        // Round skoru
        string winner = siteAWon ? "Player 1" : "Player 2";
        GameData.roundResults[currentLevel] = $"  {winner}";

        ShowWinnerText(siteAWon);
        GameData.currentSlotLevel++;

        if (GameData.currentSlotLevel >= 4)
        {
            CheckFinalWinner();
        }
    }

    private void ShowWinnerText(bool siteAWon)
    {
        GameObject textObj = GameObject.Find("WinnerText");
        if (textObj == null) return;

        textObj.SetActive(true);
        var tmp = textObj.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = siteAWon ? "Player 1 Kazandı!" : "Player 2 Kazandı!";
        }
    }

    public void CheckFinalWinner()
    {
        int aWins = 0;
        int bWins = 0;

        //  Gerçek sonuç GameData.siteAWins üzerinden sayılıyor
        for (int i = 0; i < 4; i++)
        {
            if (GameData.siteAWins[i]) aWins++;
            else bWins++;
        }

        string finalResult = "";

        if (aWins > bWins)
        {
            finalResult = "Player 1 Kazandı!";
        }
        else if (bWins > aWins)
        {
            finalResult = "Player 2 Kazandı!";
        }
        else
        {
            if (GameData.siteACompletionTime < GameData.siteBCompletionTime)
                finalResult = "Player 1 Kazandı (süre)";
            else if (GameData.siteBCompletionTime < GameData.siteACompletionTime)
                finalResult = "Player 2 Kazandı (süre)";
            else
                finalResult = "Berabere!";
        }

        Debug.Log("🏁 Final sonucu: " + finalResult);
        StartCoroutine(BlinkWinnerCharacter(finalResult, Color.white));
        GameData.finalWinnerText = finalResult;

        if (scorePanel != null && scorePanelDisplay != null)
        {
            scorePanel.SetActive(true);
            scorePanelDisplay.UpdateScorePanel();
        }
    }

    private IEnumerator BlinkWinnerCharacter(string winnerText, Color color)
    {
        if (audioSource != null && victoryClip != null)
            audioSource.PlayOneShot(victoryClip);

        if (winnerCharacterIcon != null)
        {
            winnerCharacterIcon.SetActive(true);
            Image img = winnerCharacterIcon.GetComponent<Image>();

            int blinkCount = 4;
            float blinkDuration = 0.25f;

            for (int i = 0; i < blinkCount; i++)
            {
                img.color = new Color(color.r, color.g, color.b, 1f);
                yield return new WaitForSeconds(blinkDuration);
                img.color = new Color(color.r, color.g, color.b, 0f);
                yield return new WaitForSeconds(blinkDuration);
            }

            img.color = new Color(color.r, color.g, color.b, 1f);
        }
    }
    public void RestartGame()
    {
        Debug.Log(" Oyun yeniden başlatılıyor...");
        GameData.currentSlotLevel = 0;
        GameData.siteAPlayed = false;
        GameData.siteBPlayed = false;
        GameData.roundResults = new string[4];
        GameData.siteAWins = new bool[4];
        GameData.resolveRoundOnNextLoad = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}