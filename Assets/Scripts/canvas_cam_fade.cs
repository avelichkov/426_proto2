using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_cam_fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    public float fadeSpeed;
    public bool fadeIn;
    public float waitTime;
    public GameObject jumpscare;
    private float timer;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>(); 
        jumpscare.SetActive(false);
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
         if (fadeIn)
        {
            if (cg.alpha < 1) {
                cg.alpha += Time.deltaTime*fadeSpeed;
                if (cg.alpha >= 1 ){
                    fadeIn = false;
                }
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0){
            jumpscare.SetActive(true);
            audioManager.Play("jumpscare");
            timer = 10000;
        }
        //Debug.Log(timer);
    }
}
