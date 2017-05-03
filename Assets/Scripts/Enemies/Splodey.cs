using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splodey : Enemy
{
    [Header("Splodey")]
    public GameObject onSelfDestruct;
    public SphereCollider explosionSphere;
    public float explosionDelay = 2f;
    private Rigidbody2D rigid2D;
    private Animator anim;
    protected override void Awake()
    {

        base.Awake();
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    protected override void Attack()
    {
        StartCoroutine(StartDestruction(explosionDelay));
    }
    protected override void OnDeath()
    {
        SelfDestruct();
    }
    IEnumerator StartDestruction(float delay)
    {
        // Ignite the player
        anim.SetTrigger("Explode");

        // Wait for seconds
        yield return new WaitForSeconds(delay);

        // Check if we are still at target
        if (IsAtTarget())
        {
            // Self Destruct
            SelfDestruct();
        }
        else
        {
            // Reset animation
            anim.SetTrigger("Deactivate");
        }

    }
    void SelfDestruct()
    {
        health = 0;
        PlayExplosion();
        Destroy(gameObject);
    }
    void PlayExplosion()
    {
        // Spawn our explosion prefab
        GameObject explosion = Instantiate(onSelfDestruct);
        explosion.transform.position = transform.position;
    }
    // CTRL + M + O
    // CTRL + M + P

}
