using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel;

    private void Start()
    {
        if (!GameData.infoShownOnce)
        {
            infoPanel.SetActive(true);
            GameData.infoShownOnce = true;
            Debug.Log("ℹ️ İlk kez gösterildi: InfoPanel");
        }
        else
        {
            infoPanel.SetActive(false); // tekrar oyunlara girince otomatik açılmasın
        }
    }

    public void OpenInfo()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfo()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
            Debug.Log("📴 Info Panel kapatıldı");
        }
    }
}