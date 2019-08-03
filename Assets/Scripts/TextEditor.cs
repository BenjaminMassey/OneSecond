using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEditor : MonoBehaviour
{
    private static GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        me = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Write(string text)
    {
        if (me == null)
        {
            me = GameObject.Find("Text");
        }
        me.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    public static void Append(string text)
    {
        if (me == null)
        {
            me = GameObject.Find("Text");
        }
        me.GetComponent<UnityEngine.UI.Text>().text += text;
    }
}
