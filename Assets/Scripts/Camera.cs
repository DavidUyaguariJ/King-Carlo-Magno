using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform objetive;
    public float cameraSpeed = 1f;
    public Vector3 movement;
	private Vector3 initialOffset;
	private bool hasInitialOffset = false;

	private void Start()
	{
		if (objetive != null)
		{
			initialOffset = transform.position - objetive.position;
			hasInitialOffset = true;
		}
	}

	private void LateUpdate()
	{
		if (objetive != null && hasInitialOffset)
		{
			Vector3 desiredPosition = objetive.position + initialOffset;
			transform.position = desiredPosition;
		}
	}
}
