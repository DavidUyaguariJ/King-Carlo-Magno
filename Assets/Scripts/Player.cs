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
    private Vector3 lastCheckpointPosition;
	private Persistence persistence;


    public void makeWalk()
    {
        float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");
		Vector3 movementDirection = new Vector3(horizontalInput,0, verticalInput);
        movementDirection.Normalize();
        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
    }

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        textDetect.SetActive(false);

        // Inicializar la variable persistence con la instancia del singleton
        persistence = Persistence.Instance;

        if (persistence != null)
        {
            // Cargar la posición del jugador al inicio
            Vector3 loadedPosition = persistence.LoadPlayerPosition();
            transform.position = loadedPosition == Vector3.zero ? transform.position : loadedPosition;
            lastCheckpointPosition = transform.position; // Inicializar con la posición de inicio o cargada
        }
        else
        {
            Debug.LogError("Persistence.Instance no está inicializado. Asegúrate de que el objeto Persistence esté presente en la escena.");
        }
    }


    private void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
		{
			DeselectObject();
			SelectedObject(hit.transform);
		}
		else {
			DeselectObject();
		}
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
