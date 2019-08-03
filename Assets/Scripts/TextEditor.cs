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

    public static void Write(string loc, string text)
    {
        me = GameObject.Find(loc);
        if (me != null)
        {
            me.GetComponent<UnityEngine.UI.Text>().text = text;
        }
    }
    public static void Append(string loc, string text)
    {
        me = GameObject.Find(loc);
        if (me != null)
        {
            me.GetComponent<UnityEngine.UI.Text>().text += text;
        }
    }
}
