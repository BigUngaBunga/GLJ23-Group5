using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : PlayersAction
{

    // Update is called once per frame
    [SerializeField] float attackRotAngle;
    
    public override IEnumerator Attack(float attackSpeed)
    {
        if (canAttack)
        {
           
            Quaternion attackRotation = Quaternion.LookRotation(Vector3.forward, transform.rotation.eulerAngles+ new Vector3(0,attackRotAngle, attackRotAngle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, attackRotation, 5);
            canAttack = false;
            controller.rb.velocity*=5;
            yield return new WaitForSeconds(0.2f);
            controller.isAttacking = false;
            hasAttacked= true;
            StopCoroutine(Attack(attackSpeed));

        }
        yield break;
        
    }
}
