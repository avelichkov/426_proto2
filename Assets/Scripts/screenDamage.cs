using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenDamage : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    public bool zombieClose;
    public float timeToDeath = 3f;  // Time to die in seconds when in enemy range
    private float timeInPresence = 0f;  // Timer for time spent in enemy range
    public float scale = 2;
    public Image blood;
    public canvas_cam_fade camFade;
    public GameObject canvasObject;


    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject canvasObject = GameObject.FindGameObjectWithTag("fadeGroup");
        // if (canvasObject != null) {
        //     Debug.Log("found canvas object");
        // }
        camFade = canvasObject.GetComponent<canvas_cam_fade>();
        if (camFade != null) {
            Debug.Log("found cam fade object");
        }
        zombieClose = true;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieClose) {
           // Increase the timer when the player is in the enemy's presence
            timeInPresence += Time.deltaTime;

            // Scale health based on the timeInPresence
            currentHealth = Mathf.Lerp(maxHealth, 0, scale*timeInPresence / timeToDeath);
            
            UpdateHealthImpactTransparency();
            if (currentHealth <= 0) {
                camFade.isDead = true;
                Debug.Log($"Set isDead: {camFade.isDead} on Instance ID: {camFade.GetInstanceID()}");
            }

        } else {
            //health increases out of presence of zombie
        }
    }

    void UpdateHealthImpactTransparency()
    {
        float transparency = 1f - (currentHealth / maxHealth);  // Scale transparency based on health
        Color imageColor = blood.color;  // Get current color of the UI image
        imageColor.a = transparency;  // Modify the alpha channel
        blood.color = imageColor;  // Set the updated color back to the UI image
    }
}
