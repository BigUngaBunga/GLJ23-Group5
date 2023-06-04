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
        if ((currentHealth -= damage) <= 0)
            Die();
    }


    private void Die()
    {
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
        rigidbody.velocity = direction * speed * Time.fixedDeltaTime;
        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 5);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player1") || collision.gameObject.tag.Equals("Player2"))
            playersInRange.Add(collision.GetComponent<PlayerController>());

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player1") || collision.gameObject.tag.Equals("Player2") && playersInRange.Contains(collision.GetComponent<PlayerController>()))
            playersInRange.Remove(collision.GetComponent<PlayerController>());
    }
}