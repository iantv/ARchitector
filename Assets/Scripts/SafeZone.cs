using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    /*public float SafezoneWidth = 1.0f;
    public float SafezoneLength = 1.0f;
    public float ObjectWidth = 1.0f;
    public float ObjectLength = 1.0f;*/
    public GameObject Zone;
    public Material ClearedZone;
    public Material NonClearedZone;
    private GameObject[] tempZones;
    public List<GameObject> allZones;
    public bool ReadyToPlace = true;

    private void Start()
    {
        //Zone.transform.localScale = new Vector3(ObjectLength + SafezoneLength, 1, ObjectWidth + SafezoneWidth);
        UpdateZones();
    }
    public void UpdateZones()
    {
        tempZones = GameObject.FindGameObjectsWithTag("SafeZone");
        allZones.Clear();
        foreach (GameObject plane in tempZones)
        {
            if (plane != null)
            {
                allZones.Add(plane);
            }
        }
    }
    private void Update()
    {
        ReadyToPlace = true;
        foreach (GameObject plane in allZones)
        {
            if (plane == null)
            {
                allZones.Remove(plane);
                break;
            }
        }
        foreach (GameObject plane in allZones)
        {
            if (plane != gameObject && gameObject.GetComponent<Collider>().bounds.Intersects(plane.GetComponent<Collider>().bounds))
            {
                ReadyToPlace = false;
            }
        }
        if (!ReadyToPlace)
        {
            GetComponent<MeshRenderer>().material = NonClearedZone;
        }
        else
        {
            GetComponent<MeshRenderer>().material = ClearedZone;
        }
    }
}
