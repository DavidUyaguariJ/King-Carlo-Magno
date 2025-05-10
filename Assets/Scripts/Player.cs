using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	float speed = 1.5f;
	LayerMask mask;
	public float distance = 5f;
	public Texture2D pointer;
	public GameObject textDetect;
	GameObject lastRecognized = null;
	private Persistence persistence;
	private Camera playerCamera;




	void Start()
	{
		mask = LayerMask.GetMask("Raycast Detect");
		textDetect.SetActive(false);
		persistence = FindObjectOfType<Persistence>();
		playerCamera = FindObjectOfType<Camera>();

		if (persistence != null && playerCamera != null)
		{
			persistence.LoadGameState(transform, playerCamera);
		}
	}

	void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
		{
			DeselectObject();
			SelectedObject(hit.transform);

			if (Input.GetKeyDown(KeyCode.E) && persistence != null)
			{
				persistence.SaveGameState(transform);
			}
		}
		else
		{
			DeselectObject();
		}
	}

	public void makeWalk()
	{
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");
		Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
		movementDirection.Normalize();
		transform.position = transform.position + movementDirection * speed * Time.deltaTime;
	}
	public bool interactWithObject()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask);
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
		{
			if (hit.collider.tag == "bounty")
			{
				return hit.collider.transform.GetComponent<AddOn>().activeAddOn();
			}
		}
		return false;
	}

	void SelectedObject(Transform target)
	{
		target.GetComponent<MeshRenderer>().material.color = Color.green;
		lastRecognized = target.gameObject;

		if (textDetect != null)
		{
			textDetect.SetActive(true);
			SetActiveAllChildren(textDetect.transform, true);

			TMPro.TextMeshProUGUI textComponent = textDetect.GetComponentInChildren<TMPro.TextMeshProUGUI>();
			if (textComponent != null)
			{
				textComponent.text = "Mantén presionado E para guardar";
				textComponent.enabled = true;
			}
		}
	}

	void DeselectObject()
	{
		if (lastRecognized != null)
		{
			lastRecognized.GetComponent<Renderer>().material.color = Color.white;
			lastRecognized = null;
		}

		if (textDetect != null)
		{
			textDetect.SetActive(false);
		}
	}

	// Método auxiliar para activar/desactivar todos los hijos recursivamente
	private void SetActiveAllChildren(Transform parent, bool state)
	{
		foreach (Transform child in parent)
		{
			child.gameObject.SetActive(state);
			SetActiveAllChildren(child, state);
		}
	}

	void OnGUI()
	{
		Rect rect = new Rect(Screen.width, Screen.height, pointer.width, pointer.height);
		GUI.DrawTexture(rect, pointer);

		if (lastRecognized)
		{
			textDetect.gameObject.SetActive(true);
		}
		else
		{
			textDetect.gameObject.SetActive(false);
		}
	}
}