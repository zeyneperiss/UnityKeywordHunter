using UnityEngine;
using TMPro;

public class ScorePanelDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] roundTexts; // RoundText1 - RoundText4
    public TextMeshProUGUI winnerLabelText;

    private void OnEnable()
    {
        // Round sonuçlarını panele yaz
        for (int i = 0; i < roundTexts.Length; i++)
        {
            if (i < GameData.roundResults.Length && !string.IsNullOrEmpty(GameData.roundResults[i]))
            {
                roundTexts[i].text = GameData.roundResults[i];
            }
            else
            {
                roundTexts[i].text = $"Round {i + 1}: -";
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
                roundTexts[i].text = GameData.roundResults[i];
            }
            else
            {
                roundTexts[i].text = $"Round {i + 1}: -";
            }
        }
    }
}