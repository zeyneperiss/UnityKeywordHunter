using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSiteA()
    {
        GameData.siteAIsPlaying = true;
        SceneManager.LoadScene("SiteEditScene");
    }

    public void LoadSiteB()
    {
        GameData.siteAIsPlaying = false;
        SceneManager.LoadScene("SiteEditScene");
    }
}
