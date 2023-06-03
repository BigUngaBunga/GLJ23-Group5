using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : PlayersAction
{
   
    // Update is called once per frame
    public override IEnumerator Attack(float attackSpeed)
    {
        if (canAttack)
        {
            canAttack = false;
            controller.rb.velocity*=5;
            yield return new WaitForSeconds(0.2f);
            controller.isAttacking = false;
            StopCoroutine(Attack(attackSpeed));
        }
        yield break;
        
    }
}
