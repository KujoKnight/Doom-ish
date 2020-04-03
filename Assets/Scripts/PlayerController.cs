using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Instance & Rigidbody2D
    public static PlayerController instance;
    public Rigidbody2D rb;

    //Input Vectors
    private Vector2 moveInput;
    private Vector2 mouseInput;

    //Input Speeds
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;

    //Camera & Gun
    public Camera cameraView;
    public GameObject bulletImpact;
    public Animator gunAnim;
    public Animator cameraAnim;

    //Ammo
    public int currentAmmo;
    public int maxAmmo = 50;

    //Health & Death Screen
    public int currentHealth;
    public int maxHealth = 100;
    public GameObject deathScreen;
    private bool isDead;

    private Vector3 move = Vector3.zero;

    //UI Text
    public TextMeshProUGUI healthText, ammoText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        deathScreen.SetActive(false);
        currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }

    void Update()
    {
        //Check if dead
        if (!isDead)
        {
            //Movement Controls
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //Vector3 moveHoriz = transform.up * -moveInput.x;
            //Vector3 moveVert = transform.right * moveInput.y;

            //Collected all the calculations into one vector.
            move = transform.up * -moveInput.x + transform.right * moveInput.y;

            rb.velocity = move * moveSpeed;

            //View Controls
            mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * mouseSensitivity);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
            cameraView.transform.localRotation = Quaternion.Euler(cameraView.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

            //Shooting Controls
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (currentAmmo > 0)
                {
                    Ray ray = cameraView.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("Hit: " + hit.transform.name);
                        Instantiate(bulletImpact, hit.point, transform.rotation);

                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                        }
                        AudioController.instance.PlayGunShot();
                    }
                    currentAmmo--;
                    gunAnim.SetTrigger("Shoot");
                    UpdateAmmoUI();
                }
            }

            if (moveInput != Vector2.zero)
            {
                cameraAnim.SetBool("IsMoving", true);
            }
            else
            {
                cameraAnim.SetBool("IsMoving", false);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            isDead = true;
            currentHealth = 0;
            Time.timeScale = 0;
        }
        healthText.text = currentHealth + " / " + maxHealth;
        AudioController.instance.PlayPlayerHurt();
    }

    public void AddHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthText.text = currentHealth + " / " + maxHealth;
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}
