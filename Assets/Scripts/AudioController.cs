using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource ammo, enemyDeath, enemyShot, gunShot, health, playerHurt, doorOpen, doorClose, victorySound;

    private void Awake()
    {
        //Jenny: Safety optimization, otherwise two game managers would be very hard to control.
        if (instance != null)
        {
            Debug.LogError("Two GameManagers Active!");
            DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    public void PlayAmmoPickup()
    {
        ammo.Stop();
        ammo.Play();
    }
    public void PlayEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }
    public void PlayEnemyShot()
    {
        enemyShot.Stop();
        enemyShot.Play();
    }
    public void PlayGunShot()
    {
        gunShot.Stop();
        gunShot.Play();
    }
    public void PlayHealthPickup()
    {
        health.Stop();
        health.Play();
    }
    public void PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }
    public void PlayDoorOpen()
    {
        doorOpen.Stop();
        doorOpen.Play();
    }
    public void PlayDoorClose()
    {
        doorClose.Stop();
        doorClose.Play();
    }

    public void Play(AudioSource audio)
    {
        audio.Stop();
        audio.Play();
    }
}
