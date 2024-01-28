using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float objectSpeedX = -10f;
    public float objectSpeedY = -10f;

    public Transform playerNormalPosition;

    public List<ClownObject> currentClowns;


    public float minTimeBetweenEvents = 10.0f;
    public float maxTimeBetweenEvents = 15.0f;

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
        InvokeRepeating(nameof(InstantiateRandomSound), 0f, Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents));
    }
    void InstantiateRandomSound()
    {
        if (AudioManager.Instance != null)
        {
            int reference = Random.Range(0, 2);

            if (reference == 0)
            {
                AudioManager.Instance.PlayOneShot(FmodEvents.Instance.Alarm, transform.position);
            }
            else
            {
                AudioManager.Instance.PlayOneShot(FmodEvents.Instance.Siren, transform.position);
            }
        }

    }
    public void CreateClown(GameObject go)
    {
        Vector3 pos = go.transform.position;
        Destroy(go.gameObject);
        SpawnController.Instance.InstantiateClown(pos);
    }
    public void AddClown(ClownObject obj)
    {
        if(!currentClowns.Contains(obj))
        {
            currentClowns.Add(obj);
        }
    }

    public void HandleEnemyHit(EnemyObject enemy)
    {
        if(currentClowns.Count > 0)
        {
            if(currentClowns.Count >= enemy.necessaryHits)
            {
                CreateClown(enemy.gameObject);
            }
            else
            {
                HandleLooseGame();

            }
        }
        else
        {
            HandleLooseGame();
        }
    }

    private void RemoveClown()
    {
        if (currentClowns.Count > 0)
        {
            int randomIndex = Random.Range(0, currentClowns.Count);

            ClownObject randomClown = currentClowns[randomIndex];

            currentClowns.RemoveAt(randomIndex);

            Destroy(randomClown.gameObject);
        }
    }
    public void HandleLooseGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.Defeat, transform.position);
        }

        SceneManager.LoadScene(2);
    }
}
