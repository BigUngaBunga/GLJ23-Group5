using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    [SerializeField] private List<Transform> targets;
    private List<PlayerController> playersInRange;
    [SerializeField] private Vector2 direction;
    private Vector3 targetPos;
    private int closestIndex;
    private new Rigidbody2D rigidbody;

    [SerializeField] private int originalHealth = 2;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private int attackDamage = 1;
    private float latestHit;
    private int currentHealth;
    bool isTakingDamage;
    [SerializeField] GameObject bloodSplatter;

    private bool CanHit => latestHit + attackSpeed < Time.time;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        latestHit= Time.time;
        playersInRange= new List<PlayerController>();
    }

    public void Instantiate(Vector3 startPos, List<Transform> targets)
    {
        transform.position = startPos;
        this.targets = targets;

        currentHealth = originalHealth;
        gameObject.SetActive(true);
    }

    public void TakeDamage(int damage) => TakeDamage(damage, Vector2.zero);
    public void TakeDamage(int damage, Vector2 knockbackForce)
    {
        isTakingDamage= true;
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);

        currentHealth -= damage;
        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(KnockBack(knockbackForce));
        }
        if ((currentHealth -= damage) <= 0)
            Die();
       
    }
    public IEnumerator KnockBack(Vector2 force)
    {
        rigidbody.AddForce(force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isTakingDamage= false;
    }

    private void Die()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().RemoveTime(0.5f);
        gameObject.SetActive(false);
        CancelInvoke();

    }

    private void Update()
    {
        InvokeRepeating(nameof(UpdateClosestPlayer), 0f, 1f);

        direction = (targets[closestIndex].position - transform.position).normalized;
        
        if (playersInRange.Count > 0 && CanHit)
            AttackClosest();
    }

    private void UpdateClosestPlayer()
    {
        
        if (targets.Count > 1)
        {
            float closestRange = float.MaxValue;
            int index = 0;
            for (int i = 0; i < playersInRange.Count; i++)
            {
                float range = Vector3.Distance(playersInRange[i].transform.position, transform.position);
                if (range < closestRange)
                {
                    closestRange = range;
                    index = i;
                }
            }
            closestIndex= index;
        }
        else
            closestIndex= 0;
    }

    private void AttackClosest()
    {

        float closestRange = float.MaxValue;
        int closestIndex = 0;
        for (int i = 0; i < playersInRange.Count; i++)
        {
            float range = Vector3.Distance(playersInRange[i].transform.position, transform.position);
            if (range <  closestRange)
            {
                closestRange = range;
                closestIndex = i;
            }
        }

        playersInRange[closestIndex].TakeDamage(attackDamage);
        latestHit = Time.time;
    }

    private void FixedUpdate()
    {
        if (!isTakingDamage)
        {
            rigidbody.velocity = direction * speed * Time.fixedDeltaTime;
        }
        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 5);
        }
    }


    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player1(Clone)" || collision.gameObject.name == "Player2(Clone)")
        {
            Debug.Log("found Play");
            playersInRange.Add(collision.gameObject.GetComponent<PlayerController>());
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player1(Clone)" || collision.gameObject.name == "Player2(Clone)")
        {
            Debug.Log("found Play");
            playersInRange.Remove(collision.gameObject.GetComponent<PlayerController>());
        }
    }
}