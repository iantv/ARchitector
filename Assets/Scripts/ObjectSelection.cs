using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    private bool isSelected = false;
    public int ScaleState = 1;
    
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            //Check if the bloolen variable changes from false to true
            if (isSelected && !value)
            {
                gameObject.GetComponent<Outline>().enabled = false;
                gameObject.GetComponent<RotateController>().enabled = false;
                //Scaling.ScaleTransform = null;
                //Scaling.Figure = null;
                //Scaling.InScaling = false;
                //Destroy(gameObject.GetComponent<OnClickForScaling>());
                scale.SetScale(0);
            }
            //Update the boolean variable
            isSelected = value;
        }
    }
    public bool WasTouch = false;
    private ARCursor cursor;
    private ScaleManager scale;

    private void Start()
    {
        cursor = GameObject.Find("AR Session Origin").GetComponent<ARCursor>();
        scale = GameObject.Find("Scale Manager").GetComponent<ScaleManager>();
    }
    private void OnMouseDown()
    {
        if (!cursor.WaitForPlacing)
        {
            WasTouch = true;
            foreach (GameObject obj in cursor.ObjectsOnScene)
            {
                if (obj != gameObject)
                {
                    obj.GetComponent<ObjectSelection>().IsSelected = false;
                }
            }
            gameObject.GetComponent<Outline>().enabled = true;
            gameObject.GetComponent<RotateController>().enabled = true;
            IsSelected = true;
            scale.ShowScale();
        }
    }
    private void OnMouseUp()
    {
        /*if (!gameObject.GetComponent<OnClickForScaling>() && IsSelected)
        {
            gameObject.AddComponent<OnClickForScaling>();
        }
        */
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        WasTouch = false;
    }
}
