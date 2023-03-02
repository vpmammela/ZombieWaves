using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    int zombieCount = 1;
    public int totalZombiesKilled = 0;
    public GameObject zombiePreFab;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (zombieCount == totalZombiesKilled)
            for (int i = 0; i < zombieCount; i++)
            {
                Instantiate(zombiePreFab, transform.position, Quaternion.identity);
            }
        zombieCount = zombieCount * 2;
            
    }
    public void AddKill()
    {
        Debug.Log("AddKill called");
        Instantiate(zombiePreFab, transform.position, Quaternion.identity);
        totalZombiesKilled += 1;
    }
}
