using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzleObjects;
    [SerializeField] private Scene[] scene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for(int i = 0; i < puzzleObjects.Length; i++)
            {
                puzzleObjects[i].gameObject.SetActive(true);
            }
        }
    }
}
