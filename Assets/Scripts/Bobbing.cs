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
        //Put an 'f' on all the integers, pretty sure the compiler catches it, but rather be safe.
        //Could be a tiny optimization, if the compiler was trying to clamp and convert ints to floats constantly.
        //shadow.localScale = new Vector3(Mathf.Clamp(value, 0, 1), Mathf.Clamp(value, 0, 1), Mathf.Clamp(value, 0, 1));
        shadow.localScale = new Vector3(Mathf.Clamp(value, 0, 1f), Mathf.Clamp(value, 0, 1f), Mathf.Clamp(value, 0, 1f));
    }
}
