using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject Gun;
    public RuntimeAnimatorController controller;
    public Rigidbody rb;
    public BoxCollider bc;
    public bool trowable;

    public Transform canon;
    public GameObject bulletPrefab;
    public float candency;
    public bool shooting;
    public int maxMunition;
    public int munition;
    public int maxCharge;
    public int charge;
    bool canShoot = true;

    public System.Action OnShoot;


    IEnumerator Shooting()
    {
        if (canShoot)
        {
            canShoot = false;
            Shoot();
            yield return new WaitForSeconds(candency);
            canShoot = true;
            if (shooting)
                StartCoroutine(Shooting());
        }
    }


    void Shoot()
    {
        if (charge > 0)
        {            

            Instantiate(bulletPrefab, canon.position, canon.rotation);
            charge--;

            if (OnShoot != null)
            {
                OnShoot.Invoke();
            }

        }
        else
        {
            if (munition > maxCharge)
            {
                charge = maxCharge;
                munition -= maxCharge;
            }
            else
            {
                charge = munition;
                munition = 0;
            }
        }
    }

}
