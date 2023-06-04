using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage;
    PlayerController playerController;
    // Update is called once per frame
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController.isAttacking)
        {
            if (playerController.gameObject.GetComponent<PlayerMelee>().isComboAttack)
            {
               
                if (collision.tag == "Zombie")
                {
                    Debug.Log("ComboMelee");

                    collision.GetComponent<EnemyScript>().TakeDamage(100,Vector2.zero);
                    //enemies hit with comboattack
                }
            }
            else if (playerController.rb.velocity.magnitude > 20)
            {
                if (collision.tag == "Zombie")
                {
                    playerController.comboTracker.GetComponent<ComboManager>().AddCombo();

                    collision.GetComponent<EnemyScript>().TakeDamage(damage);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerController.isAttacking)
        {
            if (playerController.gameObject.GetComponent<PlayerMelee>().isComboAttack)
            {
               
                if (collision.tag == "Zombie")
                {
                    Debug.Log("ComboMelee");

                    collision.GetComponent<EnemyScript>().TakeDamage(100,Vector2.zero);
                    //enemies hit with comboattack
                }
            }
           
        }
    }
}
