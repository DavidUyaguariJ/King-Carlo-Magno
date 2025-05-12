using UnityEngine;

public class Camera : MonoBehaviour
{
	public Transform objetive;
	public float cameraSpeed = 1.5f;

	public Transform target;
	private Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void LateUpdate()
	{
		if (target != null)
		{
			transform.position = target.position + offset;
		}
	}
}