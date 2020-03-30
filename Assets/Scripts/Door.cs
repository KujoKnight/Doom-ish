using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorModel;
    public GameObject colliderObj;
    public float openSpeed;
    private bool doorOpen;

    void Update()
    {
        if (doorOpen && doorModel.position.z != -1f)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, doorModel.position.y, -1f), openSpeed * Time.deltaTime);
            if (doorModel.position.z == -1f)
            {
                colliderObj.SetActive(false);
            }
        }
        else if (!doorOpen && doorModel.position.z != 0f)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, doorModel.position.y, 0f), openSpeed * Time.deltaTime);
            if (doorModel.position.z == 0)
            {
                colliderObj.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            doorOpen = true;
            AudioController.instance.PlayDoorOpen();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            doorOpen = false;
            AudioController.instance.PlayDoorClose();
        }
    }
}
