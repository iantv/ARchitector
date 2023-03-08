using UnityEngine;
using System.Collections;

public class Scaling : MonoBehaviour
{

    public float InitialFingersDistance;
    public Vector3 InitialScale;
    public static Transform ScaleTransform;
    public static GameObject Figure;
    public static bool InScaling = false;

    void Update()
    {
        if (Figure != null)
        {
            if (Figure.GetComponent<MeshRenderer>().isVisible && Figure.GetComponent<MeshRenderer>().enabled)
            {
                int fingersOnScreen = 0;
                foreach (Touch touch in Input.touches)
                {
                    // Count fingers (or rather touches) on screen as you iterate through all screen touches.
                    fingersOnScreen++;
                    // You need two fingers on screen to pinch.
                    if (fingersOnScreen == 2)
                    {
                        InScaling = true;
                        // First set the initial distance between fingers so you can compare.
                        if (touch.phase == TouchPhase.Began)
                        {
                            InitialFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                            InitialScale = ScaleTransform.localScale;
                        }
                        else
                        {
                            float currentFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                            float scaleFactor = currentFingersDistance / InitialFingersDistance;
                            ScaleTransform.localScale = InitialScale * scaleFactor;
                        }
                    }
                    else
                    {
                        InScaling = false;
                    }
                }
            }
        }
    }
}