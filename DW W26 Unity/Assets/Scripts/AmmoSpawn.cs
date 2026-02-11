using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject ammoPrefab;

    // List of allowed spawn locations
    public Transform[] spawnPoints;

    public float spawnCooldown = 10f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnCooldown)
        {
            SpawnAmmo();
            timer = 0f;
        }
    }

    void SpawnAmmo()
    {
        // Safety check
        if (spawnPoints.Length == 0)
            return;

        // Pick random spawn point
        Transform point = spawnPoints[
            Random.Range(0, spawnPoints.Length)
        ];

        Instantiate(ammoPrefab, point.position, Quaternion.identity);
    }
}
