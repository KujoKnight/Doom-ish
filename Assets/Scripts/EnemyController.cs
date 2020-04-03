using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Added this state check for the enemies, since it's faster than checking distances etc.
public enum ECState
{
    InRange,
    NotInRange
}

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
    private Rigidbody2D rb; //Turned this private, getting via component ref
    public GameObject explosionFX;

    //Added this
    public ECState state = ECState.NotInRange;
    //and this 
    public float StateCheckRate = 1f;
    //Added this
    private int MaxHealth;
    //Added this 
    private void Awake()
    {
        MaxHealth = health;
    }
    //Added this
    private void Start()
    {
        MaxHealth = health;
        rb = GetComponent<Rigidbody2D>();
    }

    //Added this
    private void OnEnable()
    {
        health = MaxHealth;
        StartCoroutine(CheckState(StateCheckRate));
    }

    //Added this
    IEnumerator CheckState(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            state = Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange ? ECState.InRange : ECState.NotInRange;
            if (state == ECState.NotInRange)
            {

                rb.velocity = Vector2.zero;

            }
        }
    }

    void Update()
    {

        if (state == ECState.InRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;

            if (canFire)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(projectile, fireTarget.position, fireTarget.rotation);
                    shotCounter = fireRate;
                }
            }
            /*  -- Removed, too much checking, only needs to be set once not every frame.
            else
            {
                rb.velocity = Vector2.zero;
            } */
        }

    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
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

            //Spawner GameObject Handling - if spawner exists.
            if (Spawner.instance != null)
            {
                Spawner.instance.Dispose(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            AudioController.instance.PlayEnemyShot();
        }
    }

    /*
    public void TakeDamage()
    {
        health--;
        if (health <= 0)
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
    }*/


}

