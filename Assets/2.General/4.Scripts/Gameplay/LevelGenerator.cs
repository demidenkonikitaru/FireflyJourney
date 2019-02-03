using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoSingleton<LevelGenerator>
{

    [Tooltip("Array of platform prefabs.")]
    [SerializeField]
    private GameObject[] platformPrefabs;

    [HideInInspector]
    public List<GameObject> objectPool; //contains pooled object which wait for redistributing
    //_______________________________________________

    [Header("Spawning parameters.")]

    [Tooltip("Number of platform to spawn on Awake.")]
    [SerializeField]
    private int numberOfPlatformsToSpawn; //number of platforms for ferst spawn

    [Tooltip("Minimal height between platforms.")]
    [SerializeField]
    private float minHeight;

    [Tooltip("Maximal height between platforms.")]
    [SerializeField]
    private float maxHeight;

    [Tooltip("Level width determines where to spawn on x axis.")]
    [SerializeField]
    private float levelWidth;

    private Vector3 spawnPosition = new Vector3(); //position to spawn next item. Never sets lower than it is.

    [Header("Start spawning parameters.")]

    public int bottomPlatformsCount;

    public Vector2 spawningStartPoint;

    public float spawningDistance;

    private void Awake()
    {

    }

    void Start () {
		
	}
	
	void Update () {

    }

    public void GenerateLevel()
    {
        for (int i = 0; i < numberOfPlatformsToSpawn; i++)
        {
            spawnPosition.y += Random.Range(minHeight, maxHeight); //changes spawn position so platforms can't overlap
            spawnPosition.x = Random.Range(-levelWidth, levelWidth); //changes spawn position on x axis to distribute objects over level
            GameObject newPlatform = Instantiate(platformPrefabs[0], spawnPosition, Quaternion.identity);
            objectPool.Add(newPlatform);
        }
    }

    public void GenerateBottomPlatforms()
    {
        Vector2 spawnPoint = spawningStartPoint;

        for(int i = 0; i < bottomPlatformsCount; i++)
        {
            Instantiate(platformPrefabs[0], spawnPoint, Quaternion.identity);
            spawnPoint = new Vector2(spawnPoint.x + spawningDistance, spawningStartPoint.y);
        }
    }

    public void DistributePlatform(Collider2D collision)
    {
        spawnPosition.y += Random.Range(minHeight, maxHeight); 
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        collision.GetComponent<Transform>().position = new Vector2(spawnPosition.x, spawnPosition.y);
    }

    public void DestroyAndResetLevel()
    {
        spawnPosition = new Vector3();

        foreach(GameObject platform in objectPool)
        {
            Destroy(platform);
        }

    }
}
