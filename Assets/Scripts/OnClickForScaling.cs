using UnityEngine;
using System.Collections;

public class OnClickForScaling : MonoBehaviour
{
    void OnMouseDown()
    {
        if (gameObject.GetComponent<ObjectSelection>().IsSelected)
        {
            if (this.GetComponent<MeshRenderer>().isVisible && this.GetComponent<MeshRenderer>().enabled)
            {
                Scaling.ScaleTransform = this.transform;
                Scaling.Figure = this.gameObject;
            }
        }
    }
}