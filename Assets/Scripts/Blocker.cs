using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    private static GameObject blocker;
    
    // Start is called before the first frame update
    void Start()
    {
        blocker = gameObject;
        SendOnSignal();
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.T))
        {
            Toggle();
        }
    }

    public static void SendOnSignal()
    {
        blocker.GetComponent<Blocker>().StartCoroutine("OnSignal");
    }

    public static void Toggle()
    {
        blocker.GetComponent<SpriteRenderer>().enabled = !GetHidden();
    }

    public static void On()
    {
        blocker.GetComponent<SpriteRenderer>().enabled = true;
    }

    public static void Off()
    {
        blocker.GetComponent<SpriteRenderer>().enabled = false;
    }

    private static bool GetHidden()
    {
        return blocker.GetComponent<SpriteRenderer>().enabled;
    }

    IEnumerator OnSignal()
    {
        Movement.block = true;
        TextEditor.Write("");
        int count = 20;
        for (int i = 0; i < count; i++)
        {
            TextEditor.Append("=");
            yield return new WaitForSeconds(1.0f / count);
        }
        On();
        TextEditor.Write("");
        Movement.block = false;
    }
}
