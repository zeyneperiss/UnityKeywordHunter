using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OfficeSlotDisplay[] slots; // slot dizisi: 0 = üst sol, 7 = alt sağ
    private int currentLevel = 0;     // hangi katta olduğumuzu tutar (0 = zemin)

    private void Start()
    {
        if (GameData.resolveRoundOnNextLoad)
        {
            Debug.Log("MainScene açıldıktan sonra ışıklar uygulanacak...");

            if (GameData.siteAPlayed && GameData.siteBPlayed)
            {
                // Her iki site de oynandıysa round değerlendir
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

        ShowWinnerText(siteAWon); // 👈 Kazanan metnini göster

        currentLevel++;
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
}