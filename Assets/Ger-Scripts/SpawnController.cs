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
    [SerializeField] private Transform lowObjectSpawnPosition;
    [SerializeField] private Transform highObjectSpawnPosition;

    [Space]
    [Header("GROUND")]
    [SerializeField] private GameObject groundObject;
    [SerializeField] private Transform lowGroundSpawnPosition;
    [SerializeField] private Transform highGroundSpawnPosition;
    public bool isLowGround = false;

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

        SpawnObject();
        SpawnGround();
    }
    private void Update()
    {
        HandleObjectSpawn();
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

        if (selectedList != null && selectedList.Count > 0)
        {
            GameObject randomPrefab = selectedList[Random.Range(0, selectedList.Count)];

            if(isLowGround)
            {
                Instantiate(randomPrefab, lowObjectSpawnPosition.position, Quaternion.identity);
            }
            else
            {
                Instantiate(randomPrefab, highObjectSpawnPosition.position, Quaternion.identity);
            }
        }
    }
    public void SpawnGround()
    {
        //Transform spawnTransform = (Random.Range(0f, 1f) < 0.5f) ? lowGroundSpawnPosition : highGroundSpawnPosition;

        Transform spawnTransform;

        float reference = Random.Range(0f, 1f);

        if(reference < 0.5f)
        {
            spawnTransform = lowGroundSpawnPosition;
            isLowGround = false;
        }
        else
        {
            spawnTransform = highGroundSpawnPosition;
            isLowGround = true;
        }

        Instantiate(groundObject, spawnTransform.position, Quaternion.identity);
    }
    private List<GameObject> GetRandomList()
    {
        List<List<GameObject>> allLists = new List<List<GameObject>>()
        {
            objectsData.enemyPrefabs,
            objectsData.blockerPrefabs,
            objectsData.npcPrefabs
        };

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
