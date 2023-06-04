using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerMelee : PlayersAction
{

    // Update is called once per frame

    public float comboRotation = 0;
    public float comboRotationMax;
    [SerializeField] public float rotationDuration;
    public override IEnumerator Attack(float attackSpeed)
    {
        if (canAttack)
        {                  
            controller.rb.velocity*=4.5f;  
            controller.isAttacking = false;
            hasAttacked= true;
            StopCoroutine(Attack(attackSpeed));
        }
        yield break;
        
    }
    public override IEnumerator ComboAttack()
    {
        isComboAttack = true;
        float startRot = transform.eulerAngles.y;
        comboRotationMax = startRot +1080.0f;
        float t = 0;
        while(t<rotationDuration)
        {
            t += Time.deltaTime;
            float initRot = transform.eulerAngles.y;
            float zRotation = Mathf.Lerp(initRot, comboRotationMax, t/rotationDuration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
            gameObject.GetComponent<PlayerController>().followCam.transform.localRotation = Quaternion.Inverse(gameObject.transform.rotation);
            yield return new WaitForSeconds(0.01f); 
        }
        Debug.Log("StopAttack");
        gameObject.GetComponent<PlayerController>().isAttacking = false;
        hasAttacked= false;   
        isComboAttack= false;
        StartCoroutine(WaitAttack(0.5f));
    }
}
