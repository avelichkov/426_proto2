using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{
    public int Health = 5;

    [HideInInspector]
    public Collider2D feetCol;

    [HideInInspector]
    
    public Collider2D zone1;

    public Stage stage = Stage.NEW;

    public enum Stage { NEW = 1,MID = 2, CLOSE = 3,}

    private bool created = false;


    void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        transform.localScale = new Vector2(0.06f,0.06f);
        GoToRandomPos();
    }

    // Doing this in update so physics update has a change to run
    void Update()
    {
        if (!created)
        {
            if (!IsColliding())
            {
                print("not in good spot");
                GoToRandomPos();
            }
            else
            {
                print("in good spot");
                GetComponent<SpriteRenderer>().color = new Color(0,255,255,255);
                GetComponent<Renderer>().enabled = true;
                created = true;
            }
        }
    }

    private void OnMouseDown() {
        Debug.Log("BRAINZ!");
        Collider2D colA = GetComponent<Collider2D>();
        Collider2D colB = GameObject.Find("Zone1").GetComponent<Collider2D>();
        Debug.Log(IsColliding());
    }

    private void GoToRandomPos()
    {
        float randx = Random.Range(-5.0f,5.0f);
        float randy = Random.Range(-3.0f,3.0f);
        transform.position = new Vector2(randx,randy);
        //Debug.Log("Going to pos: " + randx + " " + randy);
    }

    private bool IsColliding()
    {
        Collider2D colA = transform.GetChild(0).GetComponent<Collider2D>();
        Collider2D colB = GameObject.Find("Zone1").GetComponent<Collider2D>();
        return colA.IsTouching(colB);
    }
}
