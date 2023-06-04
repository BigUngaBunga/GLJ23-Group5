using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 move;
    [SerializeField] int maxHealth;
    int health;
    [SerializeField] Camera followCam;
    [SerializeField] float playerSpeed;
    public  Rigidbody2D rb;
    public  bool isAttacking;
    [SerializeField] public float attackDelay;

    [SerializeField] public float attackSpeed;
    [SerializeField] PlayersAction attackAction;
    [SerializeField] Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        move = context.ReadValue<Vector2>();

    }
    public void OnPlayerAttack(InputAction.CallbackContext context)
    {
        if(context.started) {
            Debug.Log("StartShooting");
            isAttacking = true;

        }
         if (context.canceled && attackAction.hasAttacked)
        {
            Debug.Log("StopAttack");
            isAttacking = false;
            attackAction.hasAttacked = false;
            StopAllCoroutines();
            StartCoroutine(attackAction.WaitAttack(attackDelay));  
            isAttacking = false;
        }
    }
 
    public void FixedUpdate()
    {
        if(!isAttacking)
        {
            rb.velocity = new Vector2(move.x, move.y) * playerSpeed * Time.fixedDeltaTime;
        }
        if (isAttacking && !attackAction.hasAttacked)
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
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            //death
        }

    }
}
