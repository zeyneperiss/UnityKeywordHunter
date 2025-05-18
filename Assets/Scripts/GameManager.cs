using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OfficeSlotDisplay[] slots;

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

        int slotAIndex = 6 - (currentLevel * 2); // A için: 6, 4, 2, 0
        int slotBIndex = 7 - (currentLevel * 2); // B için: 7, 5, 3, 1

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
        var tmp = textObj.GetComponent<TMPro.TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = siteAWon ? "Site A Kazandı!" : "Site B Kazandı!";
        }
    }

    public void CheckFinalWinner()
    {
        int aWins = 0;
        int bWins = 0;

        // İlk 4 turda kazanan slotları kontrol et (0–7)
        for (int i = 0; i < 4; i++)
        {
            int aIndex = 6 - (i * 2); // 6,4,2,0
            int bIndex = 7 - (i * 2); // 7,5,3,1

            if (slots[aIndex].IsWin()) aWins++;
            if (slots[bIndex].IsWin()) bWins++;
        }

        // Çatı slotu (index 8) → sadece kazanan yansın
        if (aWins > bWins)
        {
            slots[8].SetWin();
            Debug.Log("🏁 Final: Site A kazandı!");
        }
        else if (bWins > aWins)
        {
            slots[8].SetLose();
            Debug.Log("🏁 Final: Site B kazandı!");
        }
        else
        {
            Debug.Log("🏁 Final: Berabere!");
            // Dilersen farklı renk ya da efekt verebilirsin
        }
    }
}