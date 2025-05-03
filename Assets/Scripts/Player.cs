using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 1f;
	LayerMask mask;
	public float distance = 5f;

	public void makeWalk()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(0, 0, verticalInput);
        movementDirection.Normalize();
        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
    }

    void Start() 
    {
        mask = LayerMask.GetMask("Raycast Detect");
	}

    public bool interactWithObject()
    {
		RaycastHit hit;
		Debug.Log(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask));
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
		{
			if (hit.collider.tag == "bounty")
			{
				return hit.collider.transform.GetComponent<AddOn>().activeAddOn();
			}
		}
			return false;
	}
}
