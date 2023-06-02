using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : PlayersAction
{
   
    // Update is called once per frame
    public override IEnumerator Attack(float attackSpeed)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return  new WaitForSeconds(attackSpeed);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
