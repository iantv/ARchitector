using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]

public class RotateController : MonoBehaviour 
{

	#region ROTATE
	private float _sensitivity = 1f;
	private Vector3 _mouseReference;
	private Vector3 _mouseOffset;
	private Vector3 _rotation = Vector3.zero;
	private bool _isRotating;
    #endregion
    public bool AllowUpDownSwipes = false;

	void Update()
	{
		if(_isRotating)
		{
			// Offset.
			_mouseOffset = (Input.mousePosition - _mouseReference);

            // Apply rotation.
            if (AllowUpDownSwipes)
            {
                _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            }
            else
            {
                _rotation.y = -_mouseOffset.x * _sensitivity;
            }

			// Rotate.
			gameObject.transform.Rotate(_rotation * 0.1f);

			// Store new mouse position.
			_mouseReference = Input.mousePosition;
		}
	}

	void OnMouseDown()
	{
        if (gameObject.GetComponent<ObjectSelection>().IsSelected)
        {
            if (this.GetComponent<MeshRenderer>().enabled)
            {
                // Rotating flag.
                _isRotating = true;

                // Store mouse position.
                _mouseReference = Input.mousePosition;
            }
        }
	}

	void OnMouseUp()
	{
        //if (!(GameObject.Find("Tutorial UDT") != null || GameObject.Find("Tutorial ARCore") != null))
        //{
            if (this.GetComponent<MeshRenderer>().enabled)
            {
                // Rotating flag.
                _isRotating = false;
            }
        //}
	}

}