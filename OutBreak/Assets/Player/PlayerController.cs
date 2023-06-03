using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 move;
    [SerializeField] Camera followCam;
    [SerializeField] float playerSpeed;
    public  Rigidbody2D rb;
    public  bool isAttacking;
    [SerializeField] public float attackDelay;

    [SerializeField] public float attackSpeed;
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
        if(context.started) {
            Debug.Log("start shooting");
            isAttacking = true;

        }
         if (context.canceled)
        {
        Debug.Log("Stop Attackinng");
           StartCoroutine(attackAction.WaitAttack(attackDelay));
           isAttacking= false;
        }
    }
 
    public void FixedUpdate()
    {
        if(!isAttacking)
        {

        rb.velocity = new Vector2(move.x, move.y) * playerSpeed * Time.fixedDeltaTime;
        }
        if (isAttacking && attackAction.canAttack)
        {
           
            StartCoroutine(attackAction.Attack(attackSpeed));
        }

        if (move != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 5);
        }
            followCam.transform.localRotation = Quaternion.Inverse(gameObject.transform.rotation);
        


    }
}
