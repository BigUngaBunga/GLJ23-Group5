using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    PlayerInput input;
    PlayerActions playerActions;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Start()
    {
        playerActions = new PlayerActions();
        input= GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
