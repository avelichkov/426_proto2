using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private float timer = 2.0f;

    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
        SpawnZombie();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            timer = 2.0f;
        }
    }

    private void SpawnZombie()
    {
        Instantiate(zombie);
    }
}
