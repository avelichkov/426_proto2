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
    
    public static int order;

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

    private bool _shakeStarted = false;

    public GameObject blood;
    private screenDamage sd;

    public GameObject progBar;
    private progressBar pb;

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
        blood = GameObject.FindGameObjectWithTag("blood");
        progBar = GameObject.FindGameObjectWithTag("progBar");
        pb = progBar.GetComponent<progressBar>();
        sd = blood.GetComponent<screenDamage>();
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
        if (timer < 0f && stage == Stage.CLOSE && !_shakeStarted)
        {
            _shakeStarted = true;
            sd.zombieClose = true;
            StartCoroutine(Shake());
            //make sure zombie is at top of layer
            order++;
            GetComponent<SpriteRenderer>().sortingOrder = order;
        }
        timer -= Time.deltaTime;
        if (timer < 0 && !stunned)
        {
            if (stage != Stage.CLOSE)
            {
                NextStage();
                GetComponent<Renderer>().enabled = false;
                timer = MOVE_TIME;
                needsToMove = true;
            }
            else
            {
                if (timer < -2f)
                {
                    _audioManager.Stop("loonboon");
                    pb.cleanUp();
                    GameObject.Find("Canvas").GetComponent<canvas_cam_fade>().Lose();
                }
            }
        }
    }

    public void OnMouseDown()
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
            pb.killZombie();
            if (Zombie.LeftToKill == 0 && Zombie.LeftToSpawn == 0)
            {
                Debug.Log("You win");
                _audioManager.Stop("loonboon");
                _audioManager.Play("victory jingle");
                pb.cleanUp();
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
            return;
        }
    }

    private void UpdateZ()
    {
        float newZ = (int)stage * 10;
        Vector3 newPos = transform.position;
        newPos.z = -100 - newZ;
        transform.position = newPos;
    }

    private IEnumerator HitEffect()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f,0.25f,0.25f);
        stunned = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);
        yield return new WaitForSeconds(0.3f);
        stunned = false;
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;
        float shakeMagnitude = 0.5f;
        Vector2 originalPosition = transform.position;
        while (elapsed < 2f)
        {
            // Generate a random offset
            Vector2 randomOffset = new Vector3(
                Random.Range(-shakeMagnitude, shakeMagnitude),
                Random.Range(-shakeMagnitude, shakeMagnitude)
            );

            // Apply the offset to the object's position
            transform.position = originalPosition + randomOffset;

            elapsed += Time.deltaTime;

            yield return null; // Wait until the next frame
        }

        // Reset position
        transform.position = originalPosition;
    }
}
