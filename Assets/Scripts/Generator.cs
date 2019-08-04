using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Sprite sprite;
    private static Sprite ssprite;
    private static List<GameObject> objs;
    private static int[][] map;
    //private static int jfcount = 0; OLD

    // Start is called before the first frame update
    void Start()
    {
        //img.Apply();
        objs = new List<GameObject>();
        map = new int[5][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new int[12];
        }
        ssprite = sprite;
        //MakeMaze();
        RandomMap();
        GenerateFromMap();
    }

    // Update is called once per frame
    void Update()
    {
        /* DEBUG
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CreateObject(RandomX(), RandomY());
        }
        */
    }
    /* OLD
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
    */
    public static void GenerateFromMap()
    {
        ClearObjects();
        int x = 0;
        int y = 0;
        foreach (int[] line in map)
        {
            foreach (int spot in line)
            {
                float xPos = (-2.305f) + (x * 0.4f);
                float yPos = (-0.82f) + (y * 0.4f);
                //Debug.Log("(" + x + "," + y + ") goes to (" + xPos + "," + yPos + ")");
                if (spot == 1)
                {
                    CreateObject(xPos, yPos);
                }
                if (spot > 1)
                {
                    CreateGoal(spot - 1, xPos, yPos);
                }
                /* OLD
                if (spot == 2)
                {
                    MoveGoal(xPos, yPos);
                }
                */
                x++;
            }
            x = 0;
            y++;
        }
    }

    public static void CreateGoal(int num, float x, float y)
    {
        GameObject go = Instantiate(GameObject.Find("Goal"));
        go.name = "Goal " + num;
        go.transform.position = new Vector2(x, y);
        TextOverObject.Create(go);
        go.GetComponent<TextOverObject>().SetText(num.ToString());
        objs.Add(go);
        objs.Add(GameObject.Find(go.name + " Label"));
    }

    public static void RandomMap()
    {
        int numGoals = TouchHandler.streak + 1;
        if (numGoals > 60)
        {
            numGoals = 60;
        }
        List<Vector2> goalSpots = new List<Vector2>(numGoals);
        for (int i = 0; i < numGoals; i++)
        {
            Vector2 goalSpot = Vector2.zero;
            do
            {
                goalSpot.x = Random.Range(0, map[0].Length);
                goalSpot.y = Random.Range(0, map.Length);
            }
            while (goalSpot.x > 4 && goalSpot.x < 9 && goalSpot.y < 1 && !goalSpots.Contains(goalSpot));
            goalSpots.Add(goalSpot);
        }
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                map[y][x] = 0;
                float rand = Random.Range(0.0f, 1.0f);
                if (rand < 0.1f)
                {
                    map[y][x] = 1;
                }
                if (x > 4 && x < 9 && y < 1)
                {
                    //Debug.Log("Didn't do " + x + ", " + y);
                    map[y][x] = 0;
                }
                for (int i = 0; i < goalSpots.Count; i++)
                {
                    if (goalSpots[i].x == x && goalSpots[i].y == y)
                    {
                        map[y][x] = 2 + i;
                    }
                }
            }
        }
        /* CURRENTLY BROKEN
        if (!PathExists(new Vector2(11, 1), goalSpot))
        {
            RandomMap();
        }
        */
    }

    /* BROKEN
    public static bool PathExists(Vector2 start, Vector2 end)
    {
        bool result = false;
        List<Vector2> currents = new List<Vector2>();
        List<Vector2> nexts = new List<Vector2>();
        currents.Add(start);
        do
        {
            foreach (Vector2 current in currents)
            {
                int x = (int)current.x;
                int y = (int)current.y;
                nexts.Add(new Vector2(x - 1, y - 1));
                nexts.Add(new Vector2(x, y - 1));
                nexts.Add(new Vector2(x - 1, y));
                nexts.Add(new Vector2(x + 1, y + 1));
                nexts.Add(new Vector2(x + 1, y - 1));
                nexts.Add(new Vector2(x - 1, y + 1));
                nexts.Add(new Vector2(x + 1, y));
                nexts.Add(new Vector2(x, y + 1));
                List<Vector2> removes = new List<Vector2>();
                foreach (Vector2 next in nexts)
                {
                    x = (int)next.x;
                    y = (int)next.y;
                    if (x < 0 || y < 0 || x > map[0].Length || y > map.Length)
                    {
                        removes.Add(next);
                    }
                    else
                    {
                        if (map[y][x] == 1)
                        {
                            removes.Add(next);
                        }
                    }
                }
                foreach (Vector2 remove in removes)
                {
                    nexts.Remove(remove);
                }
                if (nexts.Contains(end))
                {
                    result = true;
                    break;
                }
                jfcount++;
                if (jfcount > 1000)
                {
                    Debug.Log("Ah jeez");
                    //Application.Quit();
                    nexts.Clear();
                    break;
                }
            }
            currents = nexts;
        }
        while (nexts.Count > 0);
        return result;
    }
    */

    private static void ClearObjects()
    {
        foreach (GameObject go in objs)
        {
            GameObject.Destroy(go);
        }
        objs.Clear();
    }
    /* OLD
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
    */
    /* OLD
    private static void MoveGoal(float x, float y)
    {
        GameObject goal = GameObject.Find("Goal");
        goal.transform.position = new Vector2(x, y);
        if (goal.GetComponent<TextOverObject>() == null)
        {
            TextOverObject.Create(goal);
            goal.GetComponent<TextOverObject>().SetText("1");
        }
        else
        {
            goal.GetComponent<TextOverObject>().SetPos();
        }
    }
    */
    private static void CreateObject(float x, float y)
    {
        GameObject obj = new GameObject();
        obj.name = "Bad Piece " + objs.Count;
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = ssprite;
        obj.transform.position = new Vector2(x, y);
        obj.transform.localScale = new Vector2(2.0f, 2.0f);
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
