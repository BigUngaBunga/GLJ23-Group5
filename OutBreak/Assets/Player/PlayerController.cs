using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 move;
    [SerializeField] float playerSpeed;
    private Rigidbody2D rb;
    private bool isAttacking;
    [SerializeField] float attackSpeed;
    [SerializeField] PlayersAction attackAction;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        move = context.ReadValue<Vector2>();

    }
    public void OnPlayerAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attackinng");

       isAttacking = context.action.triggered;

    }
 
    public void Update()
    {
        rb.velocity = new Vector2(move.x, move.y) * playerSpeed * Time.deltaTime;
        if(isAttacking)
        {
            StartCoroutine(attackAction.Attack(attackSpeed));
        }
    
  
    }
}
