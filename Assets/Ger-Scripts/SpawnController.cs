using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController Instance;


    public float objectSpawnTime = 1.5f;
    public float currentObjectSpawnTime = 0f;
    [Header("OBJECTS")]
    public ObjectsData objectsData = new ObjectsData();
    [SerializeField] private Transform objectSpawnPosition;

    [Space]
    [Header("GROUND")]
    //public float groundSpawnTime = 2.5f;
    //public float currentGroundSpawnTime = 2.5f;
    [SerializeField] private GameObject groundObject;
    [SerializeField] private Transform lowGroundSpawnPosition;
    [SerializeField] private Transform highGroundSpawnPosition;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        currentObjectSpawnTime = 0;
        //currentGroundSpawnTime = 0;

        SpawnObject();
        SpawnGround();
    }
    private void Update()
    {
        HandleObjectSpawn();
        //HandleGorundSpawn();
    }

    private void HandleObjectSpawn()
    {
        if(currentObjectSpawnTime <= objectSpawnTime)
        {
            currentObjectSpawnTime += Time.deltaTime;
        }
        else
        {
            currentObjectSpawnTime = 0f;

            SpawnObject();
        }
    }
    private void SpawnObject()
    {
        List<GameObject> selectedList = GetRandomList();

        // Instantiate a random object from the selected list
        if (selectedList != null && selectedList.Count > 0)
        {
            GameObject randomPrefab = selectedList[Random.Range(0, selectedList.Count)];
            Instantiate(randomPrefab, objectSpawnPosition.position, Quaternion.identity);
        }
    }

    //private void HandleGorundSpawn()
    //{
    //    if (currentGroundSpawnTime <= groundSpawnTime)
    //    {
    //        currentGroundSpawnTime += Time.deltaTime;
    //    }
    //    else
    //    {
    //        currentGroundSpawnTime = 0f;

    //        SpawnGround();
    //    }
    //}
    public void SpawnGround()
    {
        Transform spawnTransform = (Random.Range(0f, 1f) < 0.5f) ? lowGroundSpawnPosition : highGroundSpawnPosition;

        // Instantiate the ground object at the calculated position
        Instantiate(groundObject, spawnTransform.position, Quaternion.identity);
    }
    private List<GameObject> GetRandomList()
    {
        // Create a list of lists
        List<List<GameObject>> allLists = new List<List<GameObject>>()
        {
            //objectsData.clownPrefabs,
            objectsData.enemyPrefabs,
            objectsData.blockerPrefabs,
            objectsData.npcPrefabs
        };

        // Choose a random list
        List<GameObject> selectedList = allLists[Random.Range(0, allLists.Count)];

        return selectedList;
    }
}

[System.Serializable]
public class ObjectsData
{
    public List<GameObject> clownPrefabs;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> blockerPrefabs;
    public List<GameObject> npcPrefabs;
}
