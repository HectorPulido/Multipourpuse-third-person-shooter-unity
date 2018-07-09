using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Animator))]
public class PlayerController : MonoBehaviour {

	public Transform rightGunBone;
	public List<Weapon> arsenal = new List<Weapon>();
    public Weapon currentWeapon;
    private Animator animator;

    int i = 0;

	void Awake() {
		animator = GetComponent<Animator> ();
		if (arsenal.Count > 0)
            SetWeapon(arsenal[0]);

    }

    public void NextWeapon()
    {
        i++;
        if(i >= arsenal.Count)
        {
            i = 0;
        }

        SetWeapon(arsenal[i]);
    }


    public void SetWeapon(Weapon hand)
    {
        if (rightGunBone.childCount > 0)
            for (int i = 0; i < rightGunBone.childCount; i++)
            {
                rightGunBone.GetChild(i).gameObject.SetActive(false);
            }

        if (hand != null)
        {
            hand.gameObject.SetActive(true);
            currentWeapon = hand;
            currentWeapon.OnShoot = new System.Action(() => { SendMessage("AttackAnim"); });
        }
        animator.runtimeAnimatorController = hand.controller;
    }
    
    public void SetWeaponInHand(Weapon w)
    {
        w.rb.isKinematic = true;
        w.bc.enabled = false;

        w.transform.parent = rightGunBone;
        w.transform.localPosition = Vector3.zero;
        w.transform.localRotation = Quaternion.Euler(90, 0, 0);
        w.gameObject.SetActive(false);

        currentWeapon.OnShoot = null;
    }

    public void TrowWeapon()
    {
        if (currentWeapon == null)
            return;
        if (!currentWeapon.trowable)
            return;
        currentWeapon.transform.parent = null;
        currentWeapon.rb.isKinematic = false;
        currentWeapon.bc.enabled = true;
        currentWeapon.OnShoot = null;

        currentWeapon.rb.AddForce(transform.forward * 10, ForceMode.Impulse);

        currentWeapon = null;
        arsenal.RemoveAt(i);

        SetWeapon(arsenal[0]);

    }

    private void OnTriggerEnter(Collider collision)
    {
        var c = collision.GetComponent<Weapon>();

        if (c != null)
        {
            if (arsenal.Contains(c))
                return;

            arsenal.Add(c);
            SetWeaponInHand(c); 
        }
    }

}
