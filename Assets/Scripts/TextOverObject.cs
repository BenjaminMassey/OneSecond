using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOverObject : MonoBehaviour
{
    private GameObject me;

    public static void Create(GameObject go)
    {
        go.AddComponent<TextOverObject>();
        go.GetComponent<TextOverObject>().Setup();
        go.GetComponent<TextOverObject>().SetText("WEEE");
    }

    public void Setup()
    {
        me = Instantiate(GameObject.Find("GOText"));
        me.name = name + " Label";
        SetPos();
    }

    public void SetText(string text)
    {
        me.GetComponent<TMPro.TextMeshPro>().SetText(text);
    }

    public void SetPos()
    {
        me.transform.position = transform.position;
    }
}
