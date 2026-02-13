using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
    [field: SerializeField] public Color[] PlayerColors { get; private set; }
    public int PlayerCount { get; private set; }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int maxPlayerCount = Mathf.Min(SpawnPoints.Length, PlayerColors.Length);

        if (maxPlayerCount < 1)
        {
            Debug.Log("SpawnPoints or PlayerColors not assigned!");
            return;
        }

        // Assign spawn position
        playerInput.transform.position = SpawnPoints[PlayerCount].position;
        playerInput.transform.rotation = SpawnPoints[PlayerCount].rotation;

        Color color = PlayerColors[PlayerCount];

        // Increment count
        PlayerCount++;

        // Setup PlayerController
        PlayerController controller =
            playerInput.GetComponent<PlayerController>();

        controller.AssignPlayerInputDevice(playerInput);
        controller.AssignPlayerNumber(PlayerCount);
        controller.AssignColor(color);
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("Player left.");
    }
}
