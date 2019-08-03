using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private Vector2 start;
    public static int streak;
    private int current;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        streak = 0;
        current = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* Moved into else
        if (collision.gameObject.name.Contains("Bad")) { }
        */
        Debug.Log("Hit " + collision.gameObject.name);
        if (collision.gameObject.name.Contains("Goal") &&
            collision.gameObject.name.Contains(current.ToString()))
        {
            if (current == streak + 1)
            {
                StopAllCoroutines();
                StartCoroutine("Win");
                streak++;
                current = 1;
            }
            else
            {
                StartCoroutine("HitGoal");
                Destroy(collision.gameObject);
                Destroy(GameObject.Find(collision.gameObject.name + " Label"));
                current++;
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine("TryAgain");
        }
        TextEditor.Write("Level", "Streak: " + streak);
    }

    IEnumerator Win()
    {
        Blocker.Off();
        TextEditor.Write("Heading", "You won!");
        Movement.block = true;
        yield return new WaitForSeconds(3.0f);
        transform.position = start;
        for (int i = 1; i < streak + 1; i++)
        {
            Destroy(GameObject.Find("Goal " + i + " Label"));
        }
        //Generator.MakeMaze();
        Generator.RandomMap();
        Generator.GenerateFromMap();
        Blocker.SendOnSignal();
    }

    IEnumerator TryAgain()
    {
        Blocker.Off();
        TextEditor.Write("Heading", "You failed!");
        Movement.block = true;
        yield return new WaitForSeconds(1.5f);
        transform.position = start;
        //Generator.MakeMaze();
        for (int i = 1; i < streak + 1; i++)
        {
            Debug.Log(i);
            Destroy(GameObject.Find("Goal " + i + " Label"));
        }
        Destroy(GameObject.Find("Goal 1 Label")); // debug trying
        streak = 0;
        current = 1;
        TextEditor.Write("Level", "Streak: " + streak);
        Generator.RandomMap();
        Generator.GenerateFromMap();
        Blocker.SendOnSignal();
        //TextEditor.Write("Level", "Streak: " + streak);
    }
    IEnumerator HitGoal()
    {
        TextEditor.Write("Heading", "Hit Goal " + current);
        yield return new WaitForSeconds(1.0f);
        TextEditor.Write("Heading", "");
    }
}
