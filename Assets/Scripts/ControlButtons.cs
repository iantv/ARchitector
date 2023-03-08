using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtons : MonoBehaviour
{
    public ARCursor cursor;
    public GameObject MoveButton;
    public GameObject FixPositionButton;
    public GameObject movable;
    public bool move = false;

    public void Move()
    {
        foreach (GameObject obj in cursor.ObjectsOnScene)
        {
            if (obj.GetComponent<ObjectSelection>().IsSelected)
            {
                movable = obj;
                move = true;
                break;
            }
        }
        if (move)
        {
            cursor.CursorState(true);
            MoveButton.SetActive(false);
            FixPositionButton.SetActive(true);
        }
    }
    public void Freeze()
    {
        if (cursor.WaitForPlacing)
        {
            cursor.PlaceObject();
        }
        cursor.CursorState(false);
        MoveButton.SetActive(true);
        FixPositionButton.SetActive(false);
        move = false;
        movable = null;
    }
    public void Delete()
    {
        bool deleteNotPlacedObj = false;
        if (cursor.WaitForPlacing)
        {
            deleteNotPlacedObj = true;
        }
        Freeze();
        if (deleteNotPlacedObj)
        {
            cursor.obj.GetComponent<ObjectSelection>().IsSelected = true;
        }
        foreach (GameObject obj in cursor.ObjectsOnScene)
        {
            if (obj.GetComponent<ObjectSelection>().IsSelected)
            {
                GameObject tmp = obj;
                cursor.ObjectsOnScene.Remove(obj);
                Destroy(tmp);
                cursor.scale.SetScale(0);
                break;
            }
        }
    }
    public void Update()
    {
        if (move)
        {
            movable.transform.position = cursor.Cursor.transform.position;
            movable.transform.rotation = cursor.Cursor.transform.rotation;
        }
    }
}
