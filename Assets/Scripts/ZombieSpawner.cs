using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private float timer = 0.0f;

    private float count = 50;

    public GameObject zombie;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f && count > 0)
        {
            timer = 0.25f;
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        Instantiate(zombie);
        count--;
    }
}
