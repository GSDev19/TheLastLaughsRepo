using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float objectSpeedX = -10f;
    public float objectSpeedY = -10f;

    public Transform playerNormalPosition;

    public List<ClownObject> currentClowns;
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
        Debug.Log("LOOSE");

        SceneManager.LoadScene(1);
    }
}
