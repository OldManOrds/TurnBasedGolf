using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public GameObject[] spawnPoints;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SetActive(true);
        }
    }

    public void NextSpawn()
    {
        spawnPoints[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % spawnPoints.Length;
        spawnPoints[currentIndex].SetActive(true);
    }
}
