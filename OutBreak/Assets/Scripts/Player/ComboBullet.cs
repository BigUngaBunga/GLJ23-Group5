using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBullet : BulletScript
{
    [SerializeField] GameObject explosion;
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
       
    }
}
