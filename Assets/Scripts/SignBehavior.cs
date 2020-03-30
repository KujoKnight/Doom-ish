using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignBehavior : MonoBehaviour
{
    public string message;
    public TextMeshProUGUI messageText;

    void Start()
    {
        messageText.text = string.Empty;
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            messageText.text = string.Empty;
            messageText.gameObject.SetActive(false);
        }
    }
}
