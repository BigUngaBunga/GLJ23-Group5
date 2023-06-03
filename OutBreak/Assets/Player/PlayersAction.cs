using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAction : MonoBehaviour
{
    public bool canAttack = true;
    // Start is called before the first frame update
    protected PlayerController controller;
    public void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
    }
    public virtual IEnumerator Attack(float attackSpeed)
    {
        return null;
    }
    public virtual IEnumerator WaitAttack(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

}
