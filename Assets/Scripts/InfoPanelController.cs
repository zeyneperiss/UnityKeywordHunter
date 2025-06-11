using UnityEngine;
using TMPro;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI scenarioTitleText;

    private void Start()
    {
        // Kalıcı kontrol: Sadece ilk oyunda göster
        if (PlayerPrefs.GetInt("InfoShownOnce", 0) == 0)
        {
            infoPanel.SetActive(true);
            scenarioTitleText.enabled = false; // 🔕 Başlığı gizle
            PlayerPrefs.SetInt("InfoShownOnce", 1); // kalıcı olarak bir daha açmamak üzere işaretle
            Debug.Log("ℹ️ InfoPanel ilk kez gösterildi.");
            
        }
        else
        {
            infoPanel.SetActive(false);
            scenarioTitleText.enabled = true;  // 🎯 Açık başlasın
        }
    }

    public void OpenInfo()
    {
        infoPanel.SetActive(true);
        scenarioTitleText.enabled = false; // 🔕 Panel açılınca gizle
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
        scenarioTitleText.enabled = true; // ✅ Panel kapanınca göster
    }

    // DEBUG veya Ayarlar için sıfırlamak istersen:
    [ContextMenu("Reset InfoPanel Memory")]
    public void ResetInfoMemory()
    {
        PlayerPrefs.DeleteKey("InfoShownOnce");
        Debug.Log("ℹ️ InfoPanel gösterim durumu sıfırlandı.");
    }
}