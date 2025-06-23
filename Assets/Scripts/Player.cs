using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 1f;
    LayerMask mask;
    public float distance = 5f;
    public Texture2D pointer;
    public GameObject textDetect;
    GameObject lastRecognized = null;

    public SerialReader serialReader;
    public float threshold = 0.05f;

    public void makeWalk()
    {
        Vector3 movementDirection = Vector3.zero;

        if (serialReader != null)
        {
            Vector3 accel = serialReader.accel;
            if (accel.x > threshold)
                movementDirection.x = 1f;
            else if (accel.x < -threshold)
                movementDirection.x = -1f;
            if (accel.y > threshold)
                movementDirection.z = 1f;
            else if (accel.y < -threshold)
                movementDirection.z = -1f;
        }

        movementDirection.Normalize();
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        textDetect.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
        {
            DeselectObject();
            SelectedObject(hit.transform);
        }
        else
        {
            DeselectObject();
        }
    }

    public bool interactWithObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
        {
            if (hit.collider.tag == "bounty")
            {
                return hit.collider.transform.GetComponent<AddOn>().activeAddOn();
            }
        }
        return false;
    }

    void SelectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        lastRecognized = transform.gameObject;
    }

    void DeselectObject()
    {
        if (lastRecognized)
        {
            lastRecognized.GetComponent<Renderer>().material.color = Color.white;
            lastRecognized = null;
        }
    }

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width, Screen.height, pointer.width, pointer.height);
        GUI.DrawTexture(rect, pointer);

        textDetect.SetActive(lastRecognized != null);
    }
}
