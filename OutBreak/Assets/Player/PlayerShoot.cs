using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : PlayersAction
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] GameObject bullet;
    public override IEnumerator Attack(float attackSpeed)
    {        
        Instantiate(bullet, transform.position, Quaternion.LookRotation(transform.forward, transform.up));
        yield return new WaitForSeconds(attackSpeed);
    }
}
