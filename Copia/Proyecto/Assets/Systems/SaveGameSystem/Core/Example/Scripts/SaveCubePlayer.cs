using UnityEditor.Overlays;
using UnityEngine;

public class SaveCubePlayer : MonoBehaviour
{
    GuidComponent guidComponent;

    private void Awake()
    {
        guidComponent = GetComponent<GuidComponent>();
    }

    void Start()
    {
        LoadSavedData();
    }

    void Update()
    {
        SaveData();
    }
    #region SaveGame
    private void SaveData()
    {
        PlayerPrefs.SetFloat(guidComponent.GetGuid() + SaveGameStrings.posicionX, transform.position.x);
        PlayerPrefs.SetFloat(guidComponent.GetGuid() + SaveGameStrings.posicionY, transform.position.y);
        PlayerPrefs.SetFloat(guidComponent.GetGuid() + SaveGameStrings.posicionZ, transform.position.z);
    }
    private void LoadSavedData()
    {
        Vector3 loadedPosition = new Vector3();
        loadedPosition.x = PlayerPrefs.GetFloat(guidComponent.GetGuid() + SaveGameStrings.posicionX, transform.position.x);
        loadedPosition.y = PlayerPrefs.GetFloat(guidComponent.GetGuid() + SaveGameStrings.posicionY, transform.position.y);
        loadedPosition.z = PlayerPrefs.GetFloat(guidComponent.GetGuid() + SaveGameStrings.posicionZ, transform.position.z);

        transform.position = loadedPosition;
    }
    #endregion
}
