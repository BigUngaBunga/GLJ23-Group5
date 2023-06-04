using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed;

    [SerializeField] protected int bulletDamage;
    [SerializeField] protected float bulletForce;
    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>().comboTracker.GetComponent<ComboManager>().AddCombo();
            Vector2 forceDir = collision.transform.position-transform.position;
            Debug.Log(forceDir);
        
            collision.GetComponent<EnemyScript>().TakeDamage(bulletDamage, forceDir * bulletForce);
            //zombie take dmg
            Destroy(gameObject);
        }
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
    
}
