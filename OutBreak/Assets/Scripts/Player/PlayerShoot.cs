using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : PlayersAction
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;
   

    public override IEnumerator Attack(float attackSpeed)
    {
        
        canAttack = false;
        while (!canAttack)
        {
            controller.rb.velocity = Vector2.zero;

            float spread = Random.Range(-0.13f,0.13f);
            Debug.Log("SHooting");
            Instantiate(bullet, muzzle.transform.position, Quaternion.LookRotation(transform.forward, transform.up + new Vector3( spread,spread,spread)));
            hasAttacked = true;
            yield return new WaitForSeconds(attackSpeed);

           
        }

    }

    //public bool CanShoot()
    //{
    //    bool enoughAmmo = currentAmmo > 0;
    //    return enoughAmmo && !isReloading;
    //}
    //public IEnumerator ReloadGun()
    //{
    //    isReloading = true;
    //    readyShot = false;
    //    gunZoom.SetBool("IsShooting", false);
    //    if (currentAmmo == maxAmmo) { yield return null; }

    //    soundManager.PlaySFX(soundManager.reload);
    //    yield return reloadWait;
    //    currentAmmo = maxAmmo;
    //    isReloading = false;
    //    readyShot = true;


    //}
}
