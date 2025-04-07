using UnityEngine;
using Unity.Netcode;

public class PlayerSaveHandler : NetworkBehaviour
{
    private PlayerControllerNetwork player;

    private void Awake()
    {
        player = GetComponent<PlayerControllerNetwork>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsLocalPlayer)
        {
            LoadPlayerData(); // Cargar datos solo para el jugador local al entrar en la red
        }
    }

    private void Update()
    {
        if (!IsLocalPlayer) return; // Asegurarse de que solo el jugador local pueda guardar

        // Guarda los datos del jugador al pulsar la tecla 'J'
        if (Input.GetKeyDown(KeyCode.J))
        {
            SavePlayerData();
        }
    }

    private void OnApplicationQuit()
    {
        if (IsLocalPlayer)
        {
            SaveGameSystem.Save(); // Guarda datos al cerrar la aplicación
        }
    }

    private void SavePlayerData()
    {
        // Guarda la posición, rotación y estado agachado del jugador
        SaveGameSystem.SetFloat("Player_Pos_X", transform.position.x);
        SaveGameSystem.SetFloat("Player_Pos_Y", transform.position.y);
        SaveGameSystem.SetFloat("Player_Pos_Z", transform.position.z);
        SaveGameSystem.SetFloat("RotationX", player.GetRotationX());
        SaveGameSystem.SetBool("IsCrouched", player.IsCrouched());
        SaveGameSystem.Save();
        Debug.Log("Datos del jugador guardados.");
    }

    private void LoadPlayerData()
    {
        // Cargar la posición, rotación y estado agachado del jugador
        float x = SaveGameSystem.GetFloat("Player_Pos_X", transform.position.x);
        float y = SaveGameSystem.GetFloat("Player_Pos_Y", transform.position.y);
        float z = SaveGameSystem.GetFloat("Player_Pos_Z", transform.position.z);
        transform.position = new Vector3(x, y, z);

        float rotX = SaveGameSystem.GetFloat("RotationX", 0f);
        player.SetRotationX(rotX);

        bool wasCrouched = SaveGameSystem.GetBool("IsCrouched", false);
        player.SetCrouchState(wasCrouched);
    }
}
