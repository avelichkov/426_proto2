using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
    public Image progBar;
    public Image red;
    public Image border;
    public float totalZombies = 60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void killZombie() {
        totalZombies--;
        progBar.fillAmount = totalZombies/60f;
    }

    public void cleanUp() {
        Debug.Log("clean up");
        Color transparent = progBar.color;
        transparent.a = 0;
        progBar.color = transparent;
        red.color = transparent;
        border.color = transparent;
    }
}
