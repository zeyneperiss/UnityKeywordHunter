using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OfficeSlotDisplay : MonoBehaviour
{
    private Image image;

    public Color winColor = Color.green;
    public Color loseColor = Color.red;
    public Color defaultColor = new Color(0.8f, 0.8f, 0.8f); // açık gri

    private void Awake()
    {
        image = GetComponent<Image>();
        ResetColor(); // sahne başında gri renkte olsun
    }

    public void SetWin(bool blink = true)
    {
        if (image == null) image = GetComponent<Image>();

        StopAllCoroutines();
        if (blink)
            StartCoroutine(Blink(winColor));
        else
            image.color = winColor;
    }

    public void SetLose(bool blink = true)
    {
        if (image == null) image = GetComponent<Image>();

        StopAllCoroutines();
        if (blink)
            StartCoroutine(Blink(loseColor));
        else
            image.color = loseColor;
    }

    public void ResetColor()
    {
        if (image != null)
            image.color = defaultColor;
    }

    private IEnumerator Blink(Color targetColor)
    {
        int blinkCount = 4;
        float blinkDuration = 0.2f;

        for (int i = 0; i < blinkCount; i++)
        {
            image.color = targetColor;
            yield return new WaitForSeconds(blinkDuration);
            image.color = defaultColor;
            yield return new WaitForSeconds(blinkDuration);
        }

        image.color = targetColor; // en son sabit kalsın
    }
    public bool IsWin()
    {
        return image.color == winColor;
    }
}
