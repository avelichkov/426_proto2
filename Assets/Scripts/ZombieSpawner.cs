using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private float timer = 0.0f;

    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
        Zombie.order = 0;
        Zombie.LeftToSpawn = 10;
        Zombie.LeftToKill = Zombie.LeftToSpawn;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f && Zombie.LeftToSpawn > 0)
        {
            timer = 0.25f;
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        Zombie.LeftToSpawn--;
        Instantiate(zombie, transform);
    }
}
