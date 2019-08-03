using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Sprite sprite;
    private static Sprite ssprite;
    private static List<GameObject> objs;

    // Start is called before the first frame update
    void Start()
    {
        //img.Apply();
        objs = new List<GameObject>();
        ssprite = sprite;
        MakeMaze();
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CreateObject(RandomX(), RandomY());
        }
    }

    public static void MakeMaze()
    {
        ClearObjects();
        int count = Random.Range(10, 30);
        for (int i = 0; i < count; i++)
        {
            CreateObject(RandomX(), RandomY());
        }
        RandomizeGoal();
    }

    private static void ClearObjects()
    {
        foreach (GameObject go in objs)
        {
            GameObject.Destroy(go);
        }
        objs.Clear();
    }

    private static void RandomizeGoal()
    {
        int tries = 0;
        int maxTries = 100;
        bool safe = false;
        while (!safe)
        {
            GameObject goal = GameObject.Find("Goal");
            MoveGoal(RandomX(), RandomY());
            safe = true;
            foreach (GameObject go in objs)
            {
                Vector2 dist = go.transform.position - goal.transform.position;
                dist.x = Mathf.Abs(dist.x);
                dist.y = Mathf.Abs(dist.y);
                Debug.Log(go.transform.position + " and " + goal.transform.position + " gives " + dist);
                if (dist.x < 0.2f && dist.y < 0.2f)
                {
                    safe = false;
                }
            }
            if (tries == maxTries)
            {
                Debug.Log("Gave up on randomizing goal correctly");
                break;
            }
            tries++;
        }
    }

    private static void MoveGoal(float x, float y)
    {
        GameObject goal = GameObject.Find("Goal");
        goal.transform.position = new Vector2(x, y);
    }

    private static void CreateObject(float x, float y)
    {
        GameObject obj = new GameObject();
        obj.name = "Bad Piece " + objs.Count;
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = ssprite;
        obj.transform.position = new Vector2(x, y);
        obj.AddComponent<BoxCollider2D>();
        obj.AddComponent<Rigidbody2D>();
        obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        objs.Add(obj);
    }

    private static float RandomX()
    {
        float x = 0.0f;
        while (x > -0.25f && x < 0.25f)
        {
            x = Random.Range(-1.8f, 1.8f);
        }
        return x;
    }

    private static float RandomY()
    {
        float y = -69f;
        while (y < -0.6f)
        {
            y = Random.Range(-0.8f, 0.8f);
        }
        return y;
    }
}
