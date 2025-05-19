using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioClip victoryClip;              // 🔊 Zafer sesi
    private AudioSource audioSource;           // 🎧 AudioSource bileşeni

    public OfficeSlotDisplay[] slots;
    public GameObject winnerCharacterIcon;     // 🏆 Kazanan karakter görseli (UI > Image)

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (GameData.resolveRoundOnNextLoad)
        {
            Debug.Log("MainScene açıldıktan sonra ışıklar uygulanacak...");

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
        Debug.Log("ResolveRound çağrıldı!");

        int currentLevel = GameData.currentSlotLevel;
        if (currentLevel >= 4)
        {
            Debug.Log("Tüm roundlar bitti.");
            return;
        }

        int slotAIndex = 6 - (currentLevel * 2);
        int slotBIndex = 7 - (currentLevel * 2);

        if (siteAWon)
        {
            slots[slotAIndex].SetWin();
            slots[slotBIndex].SetLose();
        }
        else
        {
            slots[slotAIndex].SetLose();
            slots[slotBIndex].SetWin();
        }

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
        if (textObj == null)
        {
            Debug.LogWarning("WinnerText objesi bulunamadı!");
            return;
        }

        textObj.SetActive(true);
        var tmp = textObj.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = siteAWon ? "Site A Kazandı!" : "Site B Kazandı!";
        }
    }

    public void CheckFinalWinner()
    {
        int aWins = 0;
        int bWins = 0;

        for (int i = 0; i < 4; i++)
        {
            int aIndex = 6 - (i * 2);
            int bIndex = 7 - (i * 2);

            if (slots[aIndex].IsWin()) aWins++;
            if (slots[bIndex].IsWin()) bWins++;
        }

        if (aWins > bWins)
        {
            Debug.Log("🏁 Final: Site A kazandı!");
            StartCoroutine(BlinkWinnerCharacter("Site A Kazandı!", Color.white));
        }
        else if (bWins > aWins)
        {
            Debug.Log("🏁 Final: Site B kazandı!");
            StartCoroutine(BlinkWinnerCharacter("Site B Kazandı!", Color.white));
        }
        else
        {
            Debug.Log("🏁 Final: Berabere!");
            StartCoroutine(BlinkWinnerCharacter("Berabere!", Color.white));
        }
    }

    private IEnumerator BlinkWinnerCharacter(string winnerText, Color color)
    {
        if (audioSource != null && victoryClip != null)
        {
            audioSource.PlayOneShot(victoryClip);
        }

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

            img.color = new Color(color.r, color.g, color.b, 1f); // Kalıcı görünür
        }
    }
}