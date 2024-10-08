using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_cam_fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    public float fadeSpeed;
    public bool isDead;
    public float waitTime;
    public GameObject jumpscare;
    public GameObject winscreen;

    private float timer;
    private AudioManager audioManager;
    private bool fadeDone = false;

    void Awake()
    {
        jumpscare.SetActive(false);
        winscreen.SetActive(false);
        // transform.Find("win screen").gameObject.SetActive(false);
        timer = waitTime;
    }

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Update check - isDead: {isDead}, Instance ID: {GetInstanceID()}");
         if (isDead){ 
            if (cg.alpha < 1) {
                cg.alpha += Time.deltaTime*fadeSpeed;
                if (cg.alpha >= 1 ){
                    isDead = false;
                    fadeDone = true;
                }
            }
        }
        if (fadeDone) {
            timer -= Time.deltaTime;
            if (timer <= 0){
                jumpscare.SetActive(true);  
                audioManager.Play("jumpscare");
                timer = 10000;
            }
        }
        //Debug.Log(timer);
    }
    public void Win()
    {
        winscreen.SetActive(true);
        isDead = false;
        Debug.Log("You Win");
    }

    public void Lose()
    {
        GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>().ClearAllZombies();
        Debug.Log("You Lose");
    }
}
