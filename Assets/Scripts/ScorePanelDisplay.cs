using UnityEngine;
using TMPro;

public class ScorePanelDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] roundTexts; // RoundText1 - RoundText4
    public TextMeshProUGUI winnerLabelText;

    private void OnEnable()
    {
        // Her roundun sadece kazananını göster
        for (int i = 0; i < roundTexts.Length; i++)
        {
            if (i < GameData.roundResults.Length && !string.IsNullOrEmpty(GameData.roundResults[i]))
            {
                roundTexts[i].text = GameData.roundResults[i]; // Sadece "Player 1" veya "Player 2"
            }
            else
            {
                roundTexts[i].text = "-";
            }
        }

        // Kazanan bilgisi
        if (winnerLabelText != null)
        {
            if (!string.IsNullOrEmpty(GameData.finalWinnerText))
            {
                winnerLabelText.text = "Kazanan: " + GameData.finalWinnerText;
            }
            else
            {
                winnerLabelText.text = "Kazanan: -";
            }
        }
    }

    public void UpdateScorePanel()
    {
        for (int i = 0; i < roundTexts.Length; i++)
        {
            if (i < GameData.roundResults.Length && !string.IsNullOrEmpty(GameData.roundResults[i]))
            {
                roundTexts[i].text = GameData.roundResults[i]; // Sadece "Player 1" veya "Player 2"
            }
            else
            {
                roundTexts[i].text = "-";
            }
        }
    }
}