using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Player1Prefab;
    [SerializeField] private GameObject Player2Prefab;
    [SerializeField] private GameObject Player1Spawner;
    [SerializeField]private GameObject Player2Spawner;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Player1 = Instantiate(Player1Prefab, Player1Spawner.transform);
        PlayerInput player1 = PlayerInput.Instantiate(Player1, controlScheme: "player1", pairWithDevice: Keyboard.current);
        //if (PlayerPrefs.GetFloat("Players")==2)
        //{
            GameObject Player2 = Instantiate(Player2Prefab, Player2Spawner.transform);
            PlayerInput player2 = PlayerInput.Instantiate(Player2, controlScheme: "Player2", pairWithDevice: Keyboard.current);
        //}
    
    }

   
}
