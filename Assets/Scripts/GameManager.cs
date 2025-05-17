using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OfficeSlotDisplay[] slots; // slot dizisi: 0 = üst sol, 7 = alt sağ
    private int currentLevel = 0;     // hangi katta olduğumuzu tutar (0 = zemin)

    public void ResolveRound(bool siteAWon)
    {
        Debug.Log("ResolveRound çağrıldı!");

        if (currentLevel >= 4)
        {
            Debug.Log("Tüm roundlar bitti.");
            return;
        }

        int slotAIndex = 6 - (currentLevel * 2); // A için: 6, 4, 2, 0
        int slotBIndex = 7 - (currentLevel * 2); // B için: 7, 5, 3, 1

        Debug.Log($"Round: {currentLevel} | A Slot: {slotAIndex}, B Slot: {slotBIndex}");

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

        currentLevel++;
    }
}
