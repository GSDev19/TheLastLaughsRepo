using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnController : MonoBehaviour
{
    public static SpawnController Instance;


    public float objectSpawnTime = 1.5f;
    public float currentObjectSpawnTime = 0f;
    [Header("OBJECTS")]
    public bool canStartSpawningObjects = false;
    public ObjectsData objectsData = new ObjectsData();
    [SerializeField] private Transform lowObjectSpawnPosition;
    [SerializeField] private Transform highObjectSpawnPosition;

    [Space]
    [Header("GROUND")]
    [SerializeField] private List<GameObject> groundObject;
    [SerializeField] private Transform lowGroundSpawnPosition;
    [SerializeField] private Transform highGroundSpawnPosition;
    public bool isLowGround = false;

    [Space]
    [Header("GROUND")]
    [SerializeField] private GameObject bgObject;
    [SerializeField] private Transform BGSpawnPosition;

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
        canStartSpawningObjects = false;
    }
    private void Update()
    {
        HandleObjectSpawn();
    }

    private void HandleObjectSpawn()
    {
        if(canStartSpawningObjects == false)
        {
            return;
        }
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

    public void SpawnBG(Vector3 pos)
    {
        Vector3 spawnPosition = new Vector3(pos.x, BGSpawnPosition.position.y, 0);
        Instantiate(bgObject, spawnPosition, Quaternion.identity);
    }
    public void SpawnGround(Vector3 position)
    {
        canStartSpawningObjects = true;

        Transform spawnTransform;
        float positionRef = Random.Range(0f, 1f);

        if(positionRef < 0.5f)
        {
            spawnTransform = lowGroundSpawnPosition;
            isLowGround = false;
        }
        else
        {
            spawnTransform = highGroundSpawnPosition;
            isLowGround = true;
        }

        GameObject ground;
        float prefabRef = Random.Range(0f, 1f);

        if (prefabRef < 0.5f)
        {
            ground = groundObject[0];
        }
        else
        {
            ground = groundObject[1];
        }

        Vector3 spawnPosition = new Vector3(position.x, spawnTransform.position.y, 0);

        if(groundObject!= null && spawnTransform != null)
        {
            Instantiate(ground, spawnPosition, Quaternion.identity);
        }

    }

    public void InstantiateClown(Vector3 pos)
    {
        if (objectsData.clownPrefabs.Count > 0)
        {
            GameObject randomPrefab = objectsData.clownPrefabs[Random.Range(0, objectsData.clownPrefabs.Count)];

            if (isLowGround)
            {
                Instantiate(randomPrefab, pos, Quaternion.identity);
            }
            else
            {
                Instantiate(randomPrefab, pos, Quaternion.identity);
            }
        }
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
