using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.AddHealth(healthAmount);
            AudioController.instance.PlayHealthPickup();
            GameManager.instance.score += 10;
            UIManager.instance.UpdateScore();
            Destroy(gameObject);
        }
    }
}
