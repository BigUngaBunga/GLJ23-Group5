using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class MultiPlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;


    // Start is called before the first frame update
    void Start()
    {
        PlayerInput player1 = PlayerInput.Instantiate(Player1, controlScheme: "player1", pairWithDevice: Keyboard.current);
        PlayerInput player2 = PlayerInput.Instantiate(Player2, controlScheme: "Player2", pairWithDevice: Keyboard.current);
    }

   
}
