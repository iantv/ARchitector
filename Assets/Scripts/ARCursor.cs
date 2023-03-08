using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARCursor : MonoBehaviour
{
    public GameObject Cursor;
    private GameObject cursorChildObject;
    public GameObject[] ObjectToPlace;
    private ARRaycastManager rayManager;
    public Selectors selectors;
    public bool DebugMode = false;
    public List<GameObject> ObjectsOnScene;
    public ControlButtons buttons;
    public GameObject obj;
    public bool WaitForPlacing = false;
    public ScaleManager scale;
    private int previousCall = 0;

    void Start()
    {
        cursorChildObject = Cursor.transform.GetChild(0).gameObject;
        rayManager = GetComponent<ARRaycastManager>();
        CursorState(false);
    }

    public void CursorState(bool state)
    {
        cursorChildObject.SetActive(state);
    }
    void Update()
    {
        if (cursorChildObject.activeInHierarchy)
        {
            // shoot a raycast from the center of the screen
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

            // if we hit an AR plane surface, update the position and rotation
            if (hits.Count > 0)
            {
                Cursor.transform.position = hits[0].pose.position;
                Cursor.transform.rotation = hits[0].pose.rotation;

                // enable the visual if it's disabled
                if (!cursorChildObject.activeInHierarchy)
                    cursorChildObject.SetActive(true);
            }
        }
    }

    public void InstantiateObjects()
    {
        if (selectors.ActiveButton != previousCall)
        {
            previousCall = selectors.ActiveButton;
            ClearActiveStatus();
            if (selectors.ActiveButton != 0)
            {
                if (WaitForPlacing)
                {
                    buttons.move = false;
                    buttons.movable = null;
                    Destroy(obj);
                }
                WaitForPlacing = true;
                obj = Instantiate(ObjectToPlace[selectors.ActiveButton - 1], Cursor.transform.position, Cursor.transform.rotation);
                obj.GetComponent<ObjectSelection>().IsSelected = true;
                scale.ShowScale();
                buttons.movable = obj;
                buttons.move = true;
                buttons.MoveButton.SetActive(false);
                buttons.FixPositionButton.SetActive(true);
                
                UpdateZones();
            }
        }
    }

    public void PlaceObject()
    {
        WaitForPlacing = false;
        ObjectsOnScene.Add(obj);
        selectors.SetActiveButton(0);
        previousCall = 0;
        obj.GetComponent<ObjectSelection>().IsSelected = false;
        CursorState(false);
        UpdateZones();
    }
    public void UpdateZones()
    {
        GameObject[] zones = GameObject.FindGameObjectsWithTag("SafeZone");
        foreach (GameObject tmp in zones)
        {
            tmp.GetComponent<SafeZone>().UpdateZones();
        }
    }

    public void ClearActiveStatus()
    {
        foreach (GameObject obj in ObjectsOnScene)
        {
            if (!obj.GetComponent<ObjectSelection>().WasTouch)
            {
                obj.GetComponent<ObjectSelection>().IsSelected = false;
            }
            else
            {
                obj.GetComponent<ObjectSelection>().WasTouch = false;
            }
        }
    }
    /*public void DisableRotationAndScailing()
    {

        if (!Scaling.InScaling)
        {
            foreach (GameObject obj in ObjectsOnScene)
            {
                if (!obj.GetComponent<ObjectSelection>().WasTouch)
                {
                    obj.GetComponent<Outline>().enabled = false;
                    obj.GetComponent<ObjectSelection>().IsSelected = false;
                    obj.GetComponent<RotateController>().enabled = false;
                    Scaling.ScaleTransform = null;
                    Scaling.Figure = null;
                    Destroy(obj.GetComponent<OnClickForScaling>());
                }
                else
                {
                    obj.GetComponent<ObjectSelection>().WasTouch = false;
                }
            }
        }
    }*/
}
