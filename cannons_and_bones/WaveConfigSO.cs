using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemiesPrefabs; 
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed;
    [SerializeField] float timeBetweenEnemySpawns;
    [SerializeField] float spawnTimeVariance;
    [SerializeField] float minimumSpawnTime;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetMoveSpeed() 
    { 
        return moveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemiesPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemiesPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

}
