using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed=1;

    List<Transform> targets;
    Vector3 direction;
    Vector3 targetPos;
    float distance =10000;

    [SerializeField] int originalHealth=2;

    int currentHealth;

    bool takenDamage, doneDamage;
    float takenDamTime, takenDamTimer;
    float doneDamTime, doneDamTimer;

    // Start is called before the first frame update
    void Start()
    {


    }


    public void Instanciate(Vector3 startPos, List<Transform> targets)
    {
        transform.position = startPos;
        this.targets = targets;

        currentHealth = originalHealth;
        gameObject.SetActive(true);

        takenDamTime = 0.2f;
        takenDamTimer = takenDamTime;
        doneDamTime = 0.5f;
        doneDamTimer = doneDamTime;
    }

    // Update is called once per frame
    void Update()
    {
        distance = 100000;

        //finds the closest player
        for (int i = 0; i < targets.Count; i++)
        {
            if (Vector3.Distance(targets[i].position, transform.position) < distance)
            {
                distance = Vector3.Distance(targets[i].position, transform.position);
                targetPos = targets[i].position;
            }
        }

        direction = (targetPos - transform.position).normalized;
        distance = Vector3.Distance(targetPos, transform.position);
        transform.up = direction;

        if (distance > 0.1f)
        {
            transform.position += direction * speed / 20;
        }



        //Counts down when the enemy can receive or deal damage again.

        if (takenDamage)
        {
            takenDamTimer -= Time.deltaTime;
            if (takenDamTimer <= 0)
            {
                takenDamTimer = takenDamTime;
                takenDamage = false;
            }
        }

        if (doneDamage)
        {
            doneDamTimer -= Time.deltaTime;
            if (doneDamTimer <= 0)
            {
                doneDamTimer = doneDamTime;
                doneDamage = false;
            }
        }

        //Checks if the enemy dies.
        //Has to be deactivated, NOT destroyed!
        if (currentHealth <= 0)
        {
            //uppdatera high score

            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Weapon") //justera efter vad de egentligen heter 
        {
            currentHealth--;
            takenDamage = true;
        }
        else if(collision.gameObject.tag=="Player")
        {
            doneDamage = true;
        }
    }
}