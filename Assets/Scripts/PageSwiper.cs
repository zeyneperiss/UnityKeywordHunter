using UnityEngine;
using UnityEngine.UI;

public class PageSwiper : MonoBehaviour
{
    public GameObject[] pages; // Page1, Page2, Page3
    public Image[] dots;       // Dot1, Dot2, Dot3

    private int currentPage = 0;
    private Vector2 startTouchPosition, endTouchPosition;
    private float swipeThreshold = 100f;

    void Start()
    {
        ShowPage(currentPage);
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    float deltaX = endTouchPosition.x - startTouchPosition.x;

                    if (Mathf.Abs(deltaX) > swipeThreshold)
                    {
                        if (deltaX > 0) PreviousPage();
                        else NextPage();
                    }
                    break;
            }
        }
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(i == index);

        for (int i = 0; i < dots.Length; i++)
            dots[i].color = (i == index) ? Color.white : new Color(1, 1, 1, 0.3f);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }
}