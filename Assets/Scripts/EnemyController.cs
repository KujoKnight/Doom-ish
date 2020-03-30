using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public float playerRange = 10f;
    public float moveSpeed;
    public bool canFire;
    public float fireRate = 0.5f;
    private float shotCounter;
    public bool isBoss;
    public int scoreValue;
    public GameObject projectile;
    public Transform fireTarget;
    public Rigidbody2D rb;
    public GameObject explosionFX;

    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;

            if(canFire)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    Instantiate(projectile, fireTarget.position, fireTarget.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosionFX, transform.position, transform.rotation);
            AudioController.instance.PlayEnemyDeath();
            GameManager.instance.score += scoreValue;
            UIManager.instance.UpdateScore();
            if (isBoss)
            {
                GameManager.instance.score += 100;
                AudioController.instance.Play(AudioController.instance.victorySound);
                GameManager.instance.Invoke("GoToNextLevel", 3.5f);
            }
        }
        else
        {
            AudioController.instance.PlayEnemyShot();
        }
    }
}
