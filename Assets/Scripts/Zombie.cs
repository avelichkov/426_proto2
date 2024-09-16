using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int Health = 5;

    public GameObject feet;

    [HideInInspector]
    public BoxCollider2D feetCol;

    [HideInInspector]
    public GameObject zone1;

    public Stage stage = Stage.NEW;

    public enum Stage { NEW = 1,MID = 2, CLOSE = 3,}

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = new Vector3(0.06f, 0.06f);
        GameObject zone1 = GameObject.Find("Zone1");
        feetCol = GetComponent<BoxCollider2D>();
        GoToRandomPos();
        for (int i = 0; i < 10000; i++)
        {
            PolygonCollider2D zone1col = zone1.GetComponent<PolygonCollider2D>();
            if (!zone1.GetComponent<PolygonCollider2D>().IsTouching(feetCol))
            {
                GoToRandomPos();
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        Debug.Log("BRAINZ!");
    }

    private void GoToRandomPos()
    {
        float randx = Random.Range(-10.0f,10.0f);
        float randy = Random.Range(-5.0f,5.0f);
        randx = 0;
        randy = 0.6f;
        transform.position = new Vector2(randx,randy);
        Debug.Log("Going to pos: " + randx + " " + randy);
    }
}
