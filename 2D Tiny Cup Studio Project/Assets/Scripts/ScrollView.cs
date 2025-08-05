using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

public class ScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject instructions;

    [SerializeField]
    private GameObject scrollView;

    private bool isShown = false;

    private void Update()
    {
        if (isShown)
        {
            instructions.SetActive(false);
        }
        else
        {
            instructions.SetActive(true);
        }

        if (Input.GetButtonDown("x"))
        {
            ShowScroll();
        }
    }

    private void ShowScroll()
    {
        Menu pauseMenu = PlayerMenus.GetMenuWithName("Pause");

        if (!isShown)
        {


            scrollView.SetActive(true);
            isShown = true;
            return;
        }
        else
        {
            scrollView.SetActive(false);
            isShown = false;
            return;
        }
    }
}
