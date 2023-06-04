using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 move;
    [SerializeField] int maxHealth;
    int health;
    [SerializeField] int maxComboPoints;
    int comboPoints;
    [SerializeField] public Camera followCam;
    [SerializeField] public TargetIndicator targetIndicator;
    [SerializeField] float playerSpeed;
    [SerializeField] float rotationSpeed;
    public  Rigidbody2D rb;
    public  bool isAttacking;
    [SerializeField] public float attackDelay;

    [SerializeField] public float attackSpeed;
    [SerializeField] PlayersAction attackAction;
    [SerializeField] Image healthBar;
    [SerializeField] public Image comboTracker;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        health = maxHealth;
        comboPoints = 100;
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        move = context.ReadValue<Vector2>();

    }
    public void OnPlayerAttack(InputAction.CallbackContext context)
    {
        if(context.started) {
            isAttacking = true;

        }
        if (context.canceled && attackAction.hasAttacked && !attackAction.isComboAttack)
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
        if (gameObject.tag == "Player1" && rb.velocity.magnitude>10)
        {
            Debug.Log(gameObject.name + rb.velocity.magnitude);
        }
        if(!isAttacking || attackAction.isComboAttack)
        {
            rb.velocity = new Vector2(move.x, move.y) * playerSpeed * Time.fixedDeltaTime;
        }
        if (isAttacking && !attackAction.hasAttacked)
        {
            if (comboPoints == maxComboPoints)
            {
                StartCoroutine(attackAction.ComboAttack());
            }
            else
            {
                StartCoroutine(attackAction.Attack(attackSpeed));
            }
            
        }

        if (rb.velocity != Vector2.zero && !attackAction.isComboAttack)
        {
            Debug.Log("Rotating");
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
        }
        followCam.transform.localRotation = Quaternion.Inverse(gameObject.transform.rotation);
        targetIndicator.transform.localRotation = Quaternion.Inverse(gameObject.transform.rotation);
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
    public void AddComboPoint()
    {
        if(comboPoints < maxComboPoints) { comboPoints++; }
    }
}
