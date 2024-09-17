using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class Zombie : MonoBehaviour
{
    public int Health;

    [HideInInspector]
    public Collider2D feetCol;

    [HideInInspector]
    
    public static float order;

    public static int LeftToSpawn;
    public static int LeftToKill; 
    public Collider2D zone1;

    public Stage stage = Stage.NEW;

    public enum Stage { NEW = 1,MID = 2, CLOSE = 3,}

    private bool needsToMove = true;

    public const float MOVE_TIME = 5.0f;

    private float timer = MOVE_TIME;

    private AudioManager _audioManager;

    private bool stunned = false;

    void Awake()
    {
        Health = 3;
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
        if (timer < 0 && !stunned)
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

    public void OnClicked()
    {
        Debug.Log("Clicked");
        _audioManager.Play("punch");
        StartCoroutine(HitEffect());
        Health--;
        string Sprite = "New";
        if (Health == 2)
        {
            Sprite = "AlmostNew";
        }
        if (Health == 1)
        {
            Sprite = "AlmostDead";
        }
        if (Health == 0)
        {
            Sprite = "Dead";
        }
        GetComponent<SpriteResolver>().SetCategoryAndLabel("Zombie",Sprite);
        if (Health <= 0)
        {
            Zombie.LeftToKill--;
            if (Zombie.LeftToKill == 0 && Zombie.LeftToSpawn == 0)
            {
                Debug.Log("You win");
                _audioManager.Stop("loonboon");
                _audioManager.Play("victory jingle");
                GameObject.Find("Canvas").GetComponent<canvas_cam_fade>().Win();

            }
            Destroy(gameObject);
        }
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
            GameObject.Find("Canvas").GetComponent<canvas_cam_fade>().Lose();
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

    IEnumerator HitEffect()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f,0.25f,0.25f);
        stunned = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);
        yield return new WaitForSeconds(0.3f);
        stunned = false;
    }
}
