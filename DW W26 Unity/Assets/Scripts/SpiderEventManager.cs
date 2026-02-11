using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderEventManager : MonoBehaviour
{
    public static SpiderEventManager Instance { get; private set; }

    public bool spiderEventActive;

    public SpiderVehicle spiderVehicle;
    public SpiderVehicle spiderVehiclePrefab;

    public PlayerController movementPlayer;
    public PlayerController shooterPlayer;
    public PlayerController looterPlayer;

    public Transform[] spiderSpawnPoints;

    public enum SpawnMode { PlayerPosition, RandomPoint, FirstPoint }
    public SpawnMode spawnMode = SpawnMode.PlayerPosition;
    void Awake()
    {
        Instance = this;
        Debug.Log("SpiderEventManager Awake");
    }



    private Vector3 GetSpiderSpawnPosition(PlayerController mover)
    {
        switch (spawnMode)
        {
            case SpawnMode.RandomPoint:
                if (spiderSpawnPoints.Length > 0)
                    return spiderSpawnPoints[Random.Range(0, spiderSpawnPoints.Length)].position;
                break;

            case SpawnMode.FirstPoint:
                if (spiderSpawnPoints.Length > 0)
                    return spiderSpawnPoints[0].position;
                break;
        }

        return mover.transform.position;
    }
    public void StartSpiderEvent(PlayerController mover)
    {
        if (spiderVehicle != null)
            return; // already spawned

        Vector3 spawnPos = GetSpiderSpawnPosition(mover);
        spiderEventActive = true;
    }

    public void EndSpiderEvent()
    {
        if (movementPlayer == null || shooterPlayer == null || looterPlayer == null)
        {
            Debug.LogError("A player reference is NULL — cannot end event.");
            return;
        }

        // Detach players
        movementPlayer.transform.SetParent(null);
        shooterPlayer.transform.SetParent(null);

        // Re-enable scripts
        movementPlayer.enabled = true;
        shooterPlayer.enabled = true;

        // Re-enable physics
        movementPlayer.GetComponent<Rigidbody2D>().simulated = true;
        shooterPlayer.GetComponent<Rigidbody2D>().simulated = true;

        // Destroy spider
        if (spiderVehicle != null)
            Destroy(spiderVehicle.gameObject);

        spiderEventActive = false;

        Debug.Log("SPIDER EVENT ENDED");
    }
    // Assign a player to a seat and set their role
    public void AssignPlayerToSeat(PlayerController player, SeatTrigger.SeatType seat)

    {
        if (!spiderEventActive)
            StartSpiderEvent(player);


        Debug.Log(player.name + " is now " + seat);
    }
}
