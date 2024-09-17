using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Zombie : MonoBehaviour
{
    public int Health = 9;

    [HideInInspector]
    public Collider2D feetCol;

    [HideInInspector]
    
    public static float order;
    public Collider2D zone1;

    public Stage stage = Stage.NEW;

    public enum Stage { NEW = 1,MID = 2, CLOSE = 3,}

    private bool needsToMove = true;

    public const float MOVE_TIME = 5.0f;

    private float timer = MOVE_TIME;

    private AudioManager _audioManager;

    void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        transform.localScale = new Vector2(0.06f,0.06f);
        GoToRandomPos();
    }

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>();
    }

    // Doing this in update so physics update has a change to run
    void Update()
    {
        //needsToMove = false;
        if (needsToMove)
        {
            if (!IsColliding())
            {
                GoToRandomPos();
            }
            else
            {
                if (stage == Stage.CLOSE)
                {
                    Vector2 newPos = transform.position;
                    newPos.y -= 1.5f;
                    transform.position = newPos;
                }
                UpdateZ();
                GetComponent<Renderer>().enabled = true;
                needsToMove = false;
            }
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (stage != Stage.CLOSE)
            {
                NextStage();
            }
            GetComponent<Renderer>().enabled = false;
            timer = MOVE_TIME;
            needsToMove = true;
        }
    }

    private void OnMouseDown() {

        // Debug.Log("Clicked");
        // _audioManager.Play("punch");
        // Destroy(gameObject);
        // Debug.Log("BRAINZ!");
        // Collider2D colA = GetComponent<Collider2D>();
        // Collider2D colB = GameObject.Find("Zone1").GetComponent<Collider2D>();
        // Debug.Log(IsColliding());
    }

    public void OnClicked()
    {
        Debug.Log("Clicked");
        _audioManager.Play("punch");
        Destroy(gameObject);
    }

    private void GoToRandomPos()
    {
        float randx = Random.Range(-10.0f,10.0f);
        float randy = Random.Range(-5.0f,5.0f);
        transform.position = new Vector2(randx,randy);
        //Debug.Log("Going to pos: " + randx + " " + randy);
    }

    private bool IsColliding()
    {
        string zone = null;
        if (stage == Stage.NEW)
        {
           zone = "Zone1";
        }
        else if (stage == Stage.MID)
        {
            zone = "Zone2";
        }
        else if (stage == Stage.CLOSE)
        {
           zone = "Zone3"; 
        }
        Collider2D colA = transform.GetChild(0).GetComponent<Collider2D>();
        Collider2D colB = GameObject.Find(zone).GetComponent<Collider2D>();
        return colA.IsTouching(colB);
    }

    private void NextStage()
    {
        if (stage == Stage.NEW)
        {
            stage = Stage.MID;
            transform.localScale = new Vector2(0.1f,0.1f);
        }
        else if (stage == Stage.MID)
        {
            stage = Stage.CLOSE;
            transform.localScale = new Vector2(0.5f,0.5f);
        }
        else if (stage == Stage.CLOSE)
        {
            Debug.Log("no more stages");
            return;
        }
    }

    private void UpdateZ()
    {
        float newZ = (int)stage * 10;
        newZ += order;
        Vector3 newPos = transform.position;
        newPos.z = -100 - newZ;
        transform.position = newPos;
        order += 0.01f;
    }
}
