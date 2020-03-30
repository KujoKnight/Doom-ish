using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damageAmount;
    public float projectileSpeed = 5;
    public Rigidbody2D rb;
    private Vector3 direction;

    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * projectileSpeed;
    }

    void Update()
    {
        rb.velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.TakeDamage(damageAmount);
            Destroy(gameObject, 5f);
        }
        if(other.tag == "Level")
        {
            Destroy(gameObject);
        }
    }
}
