using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] int explosionDamage;
    float delay = 0.2f;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {           
            collision.GetComponent<EnemyScript>().TakeDamage(explosionDamage,Vector2.zero);
            LevelManager.levelScore += 1;
            //zombie take dmg
            Destroy(this);
        }
    }
    }
