using TMPro;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Networking;
using System.Collections;

public class Persistence : MonoBehaviour
{
	[System.Serializable]
	public class GameData
	{
		public Vector3 playerPosition;
		public Vector3 cameraPosition;
		public Vector3 cameraOffset;
		public string saveTime;
	}

	[System.Serializable]
	private class ChuckNorrisJoke
	{
		public string value;
	}

	public float messageDuration = 3f;
	public Camera mainCamera;
	[SerializeField] private TMP_Text feedbackText;
	public TMP_Text jokeText;

	private string saveFilePath;
	private float messageTimer = 0f;
	private bool showingMessage = false;
	private Player player;

	void Start()
	{
		string persistenceFolder = Path.Combine(Application.dataPath, "PersistenceFiles");
		if (!Directory.Exists(persistenceFolder))
		{
			Directory.CreateDirectory(persistenceFolder);
		}

		saveFilePath = Path.Combine(persistenceFolder, "gameSave.json");
		player = FindObjectOfType<Player>();

		if (player != null && mainCamera != null)
		{
			LoadGameState(player.transform, mainCamera);
		}
	}

	void Update()
	{
		if (showingMessage)
		{
			messageTimer -= Time.deltaTime;
			if (messageTimer <= 0)
			{
				if (player != null && player.textDetect != null)
				{
					player.textDetect.SetActive(false);
				}
				showingMessage = false;
			}
		}
	}

	public void SaveGameState(Transform playerTransform)
	{
		if (mainCamera == null)
		{
			return;
		}

		GameData data = new GameData
		{
			playerPosition = playerTransform.position,
			cameraPosition = mainCamera.transform.position,
			cameraOffset = mainCamera.transform.position - playerTransform.position,
			saveTime = DateTime.Now.ToString()
		};

		string jsonData = JsonUtility.ToJson(data);
		File.WriteAllText(saveFilePath, jsonData);
		Debug.Log("Game saved: " + jsonData);

		if (feedbackText != null)
		{
			feedbackText.text = "Partida Guardada";
			feedbackText.gameObject.SetActive(true);
			messageTimer = messageDuration;
			showingMessage = true;
		}

		StartCoroutine(GetChuckNorrisJoke());
	}

	public void LoadGameState(Transform playerTransform, Camera camera)
	{
		if (!File.Exists(saveFilePath))
		{
			return;
		}

		string jsonData = File.ReadAllText(saveFilePath);
		GameData data = JsonUtility.FromJson<GameData>(jsonData);

		playerTransform.position = data.playerPosition;

		if (camera != null)
		{
			camera.transform.position = data.cameraPosition;
		}
	}

	private IEnumerator GetChuckNorrisJoke()
	{
		using (UnityWebRequest www = UnityWebRequest.Get("https://api.chucknorris.io/jokes/random"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Error al obtener broma: " + www.error);
			}
			else
			{
				string jsonResult = www.downloadHandler.text;
				ChuckNorrisJoke joke = JsonUtility.FromJson<ChuckNorrisJoke>(jsonResult);

				if (jokeText != null)
				{
					jokeText.text = joke.value;
					jokeText.gameObject.SetActive(true);
				}
			}
		}
	}
}
