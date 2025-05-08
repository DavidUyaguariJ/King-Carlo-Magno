using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float positionX;
    public float positionY;
    public float positionZ;
}

public class Persistence : MonoBehaviour
{
    private string saveFileName = "playerData.json";
    private string saveFilePath;

    public static Persistence Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: Si quieres que la persistencia sobreviva entre escenas
            saveFilePath = Application.persistentDataPath + "/" + saveFileName;
            LoadPlayerPosition();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerData data = new PlayerData
        {
            positionX = position.x,
            positionY = position.y,
            positionZ = position.z
        };

        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Posición del jugador guardada en: " + saveFilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al guardar la posición del jugador: " + e.Message);
        }
    }

    public Vector3 LoadPlayerPosition()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                Vector3 loadedPosition = new Vector3(data.positionX, data.positionY, data.positionZ);
                Debug.Log("Posición del jugador cargada desde: " + saveFilePath);
                return loadedPosition;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al cargar la posición del jugador: " + e.Message);
                return Vector3.zero; // Devuelve la posición cero en caso de error
            }
        }
        else
        {
            Debug.Log("No se encontró el archivo de guardado. Usando la posición inicial.");
            return Vector3.zero; // Devuelve la posición cero si no hay archivo
        }
    }
}