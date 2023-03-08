using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectors : MonoBehaviour
{
    public List<Image> buttons;
    public int ActiveButton = 0;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Image>())
            {
                buttons.Add(child.GetComponent<Image>());
            }
        }
    }

    public void SetActiveButton(int activeButton)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].color = Color.white;
        }
        if (activeButton != 0)
        {
            buttons[activeButton - 1].color = Color.yellow;
        }
        ActiveButton = activeButton;
    }
}
