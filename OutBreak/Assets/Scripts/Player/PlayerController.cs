using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 move;
    [SerializeField] GameObject bloodSplatter;
    [SerializeField] int maxHealth;
    [SerializeField] int health;
    [SerializeField] GameObject corpse;
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
        if(gameObject.name == "Player1(Clone)")
        {
            healthBar = GameObject.FindGameObjectWithTag("HP1").GetComponent<Image>();
            
            comboTracker = GameObject.FindGameObjectWithTag("Combo1").GetComponent<Image>();
        }
        else if (gameObject.name == "Player2(Clone)")
        {
            healthBar = GameObject.FindGameObjectWithTag("HP2").GetComponent<Image>();
            comboTracker = GameObject.FindGameObjectWithTag("Combo2").GetComponent<Image>();
        }
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
        
        if(!isAttacking || attackAction.isComboAttack)
        {
            rb.velocity = new Vector2(move.x, move.y) * playerSpeed * Time.fixedDeltaTime;
        }
        if (isAttacking && !attackAction.hasAttacked)
        {
            if (comboTracker.GetComponent<ComboManager>().combo == comboTracker.GetComponent<ComboManager>().maxCombo)
            {
                comboTracker.GetComponent<ComboManager>().Empty();
                StartCoroutine(attackAction.ComboAttack());
            }
            else
            {
                StartCoroutine(attackAction.Attack(attackSpeed));
            }
            
        }

        if (move != Vector2.zero /*&& !attackAction.isComboAttack*/)
        {
           
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
        }
        followCam.transform.localRotation = Quaternion.Inverse(gameObject.transform.rotation);
        targetIndicator.transform.localRotation = Quaternion.Inverse(gameObject.transform.rotation);
    }
    public void TakeDamage(int dmg)
    {
        Debug.Log("takingDamage;");
        Instantiate(bloodSplatter,transform.position,Quaternion.identity);
        health -= dmg;
        healthBar.fillAmount = (float)Decimal.Divide(health, maxHealth);
        if(health <= 0)
        {
            EnemyManager.SharedInstance.players.Remove(transform);
            Debug.Log("dead");
            Instantiate(corpse, transform.position, Quaternion.identity);
            Destroy(gameObject.GetComponent<PlayerInput>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
     
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            targetIndicator.enabled= false;
            
            Destroy(this);
        }

    }

}
