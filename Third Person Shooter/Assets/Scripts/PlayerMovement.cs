using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vel;
    public Vector3 aimOffset;

    Rigidbody rb;
    Animator anim;
    Transform cam;
    CameraController cc;
    PlayerController pc;

	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();

        cam = Camera.main.transform;

        cc = FindObjectOfType<CameraController>();
    }

    float h, v;
    bool a;
    void Update ()
    {
        h = InputManager.Horizontal;
        v = InputManager.Vertical;
        a = InputManager.Aim && v == 0;

        LookAtCam();

        if (InputManager.Shoot &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            h = 0;
            v = 0;
            a = true;
            pc.currentWeapon.shooting = true;
            pc.currentWeapon.StartCoroutine("Shooting");
        }
        else
        {
            pc.currentWeapon.shooting = false;
        }

        if (InputManager.ChangeWeaponPressed)
        {
            pc.NextWeapon();
        }
        if (InputManager.TrowWeaponPressed)
        {
            pc.TrowWeapon();
        }

        //a = true;

        cc.Aim = a;

        anim.SetFloat("Speed", v);
        anim.SetBool("Aiming", a);

        //foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKey(kcode))
        //        Debug.Log("KeyCode down: " + kcode);
        //}

    }

    void AttackAnim()
    {
        anim.SetTrigger("Attack");
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * v * vel;
    }

    void LookAtCam()
    {
        if (v != 0 || a)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                cam.eulerAngles.y,
                                                transform.eulerAngles.z);
        }
    }

    void OnAnimatorIK()
    {
        if (a)
        {
            anim.SetLookAtWeight(1, 1, 1);
            anim.SetLookAtPosition(cam.forward * 100  + aimOffset);
        }
        else
        {
            anim.SetLookAtWeight(0.5f);
            anim.SetLookAtPosition(cam.forward * 100);
        }
    }
}
