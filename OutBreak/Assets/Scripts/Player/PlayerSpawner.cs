using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Player1Prefab;
    [SerializeField] private GameObject Player2Prefab;
    [SerializeField] private GameObject Player1Spawner;
    [SerializeField]private GameObject Player2Spawner;

    private void Awake()
    {
     
        PlayerInput player1 = PlayerInput.Instantiate(Player1Prefab, controlScheme: "player1", pairWithDevice: Keyboard.current);
        player1.transform.position = Player1Spawner.transform.position;
        if (PlayerPrefs.GetInt("Players") == 2) { 
            PlayerInput player2 = PlayerInput.Instantiate(Player2Prefab, controlScheme: "Player2", pairWithDevice: Keyboard.current);
            player2.transform.position=Player2Spawner.transform.position;
        }
    }
}
