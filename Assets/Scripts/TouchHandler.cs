using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private Vector2 start;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bad"))
        {
            StopAllCoroutines();
            StartCoroutine("TryAgain");
        }
        if (collision.gameObject.name.Equals("Goal"))
        {
            StopAllCoroutines();
            StartCoroutine("Win");
        }
    }

    IEnumerator Win()
    {
        Blocker.Off();
        TextEditor.Write("You won!");
        Movement.block = true;
        yield return new WaitForSeconds(3.0f);
        transform.position = start;
        Generator.MakeMaze();
        Blocker.SendOnSignal();
    }

    IEnumerator TryAgain()
    {
        Blocker.Off();
        TextEditor.Write("You failed!");
        Movement.block = true;
        yield return new WaitForSeconds(1.5f);
        transform.position = start;
        Generator.MakeMaze();
        Blocker.SendOnSignal();
    }
}
