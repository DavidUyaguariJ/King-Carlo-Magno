using TMPro;
using System.IO;
using System;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public Vector3 playerPosition;
	public Vector3 cameraPosition;
	public Vector3 cameraOffset;
	public string saveTime;
}

public class Persistence : MonoBehaviour
{
	public float messageDuration = 3f;

	private string saveFilePath;
	private float messageTimer = 0f;
	private bool showingMessage = false;
	private Player player;
	[SerializeField] private TMP_Text feedbackText;

	void Start()
	{
		string persistenceFolder = Path.Combine(Application.dataPath, "PersistenceFiles");
		if (!Directory.Exists(persistenceFolder))
		{
			Directory.CreateDirectory(persistenceFolder);
		}

		saveFilePath = Path.Combine(persistenceFolder, "gameSave.json");
		player = FindObjectOfType<Player>();
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
		GameData data = new GameData
		{
			playerPosition = playerTransform.position,
			saveTime = DateTime.Now.ToString()
		};

		string jsonData = JsonUtility.ToJson(data);
		File.WriteAllText(saveFilePath, jsonData);
	}

	public void LoadGameState(Transform playerTransform, Camera camera)
	{
		if (File.Exists(saveFilePath))
		{
			string jsonData = File.ReadAllText(saveFilePath);
			GameData data = JsonUtility.FromJson<GameData>(jsonData);

			playerTransform.position = data.playerPosition;
		}
	}
}