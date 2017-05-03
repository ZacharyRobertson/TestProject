using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squanch : Enemy
{
    public float chargeSpeed = 20f;
    public float chargeDelay = 1f;
    public GameObject onCharge;

    private Rigidbody rigid;
    private Animator anim;
    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    protected override void Attack()
    {
        if(!isAttacking)
        {
            StartCoroutine(Charge(chargeDelay));
        }
    }
    IEnumerator Charge(float delay)
    {
        //Set us to attacking
        isAgentActive = false;
        isAttacking = true;
        rigid.AddForce(transform.forward * chargeSpeed, ForceMode.Impulse);
        PlayParticles();
        PlayAnim();

        yield return new WaitForSeconds(delay);

        isAgentActive = true;
        isAttacking = false;
    }
    void PlayParticles()
    {
        //Make a clone of our charge particle
        GameObject chargeParticles = Instantiate(onCharge);
        //place it on our position
        chargeParticles.transform.position = transform.position;
        //and our rotation
        chargeParticles.transform.rotation = transform.rotation;
        //rotate it to match the direction we are travelling
        chargeParticles.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
        //make it our child
        chargeParticles.transform.SetParent(transform);
    }
    void PlayAnim()
    {
        //play the charge animation
        anim.SetTrigger("Charge");
    }
}
