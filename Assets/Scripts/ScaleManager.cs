using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleManager : MonoBehaviour
{
    public List<Image> buttons;
    public int ActiveButton = 0;
    public ARCursor cursor;
    public int[] ScaleFactor;

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
    public void SetScale(int activeButton)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].color = Color.white;
        }
        GameObject tmp = null;
        if (activeButton != 0)
        {
            if (cursor.WaitForPlacing)
            {
                tmp = cursor.obj;
            }
            else
            {
                foreach (GameObject obj in cursor.ObjectsOnScene)
                {
                    if (obj.GetComponent<ObjectSelection>().IsSelected)
                    {
                        tmp = obj;
                        break;
                    }
                }
            }
            buttons[activeButton - 1].color = Color.yellow;
            ActiveButton = activeButton;
            tmp.transform.localScale = new Vector3(1.0f / ScaleFactor[activeButton - 1], 1.0f / ScaleFactor[activeButton - 1], 1.0f / ScaleFactor[activeButton - 1]);
            tmp.GetComponent<ObjectSelection>().ScaleState = activeButton;
        }
    }
    public void ShowScale()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].color = Color.white;
        }
        if (cursor.WaitForPlacing)
        {
            buttons[cursor.obj.GetComponent<ObjectSelection>().ScaleState - 1].color = Color.yellow;
        }
        else
        {
            foreach (GameObject obj in cursor.ObjectsOnScene)
            {
                if (obj.GetComponent<ObjectSelection>().IsSelected)
                {
                    buttons[obj.GetComponent<ObjectSelection>().ScaleState - 1].color = Color.yellow;
                    break;
                }
            }
        }
    }
}
