using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 0.02f;
    public static bool block;

    // Start is called before the first frame update
    void Start()
    {
        block = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !block)
        {
            transform.Translate(Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.A) && !block)
        {
            transform.Translate(Vector2.left * speed);
        }
        if (Input.GetKey(KeyCode.S) && !block)
        {
            transform.Translate(Vector2.down * speed);
        }
        if (Input.GetKey(KeyCode.D) && !block)
        {
            transform.Translate(Vector2.right * speed);
        }
    }
}
