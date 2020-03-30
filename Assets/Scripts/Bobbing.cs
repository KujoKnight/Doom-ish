using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    private Vector3 pos;
    public Transform shadow;
    public float speed = 1;

    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        float value = Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(pos.x, pos.y, value * 0.1f);
        shadow.localScale = new Vector3(Mathf.Clamp(value, 0, 1), Mathf.Clamp(value, 0, 1), Mathf.Clamp(value, 0, 1));
    }
}
