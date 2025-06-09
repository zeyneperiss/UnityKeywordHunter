using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel;

    private void Start()
    {
        // Kalıcı kontrol: Sadece ilk oyunda göster
        if (PlayerPrefs.GetInt("InfoShownOnce", 0) == 0)
        {
            infoPanel.SetActive(true);
            PlayerPrefs.SetInt("InfoShownOnce", 1); // kalıcı olarak bir daha açmamak üzere işaretle
            Debug.Log("ℹ️ InfoPanel ilk kez gösterildi.");
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }

    public void OpenInfo()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
    }

    // DEBUG veya Ayarlar için sıfırlamak istersen:
    [ContextMenu("Reset InfoPanel Memory")]
    public void ResetInfoMemory()
    {
        PlayerPrefs.DeleteKey("InfoShownOnce");
        Debug.Log("ℹ️ InfoPanel gösterim durumu sıfırlandı.");
    }
}