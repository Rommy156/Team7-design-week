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

        // ✅ Safety check
        if (maxPlayerCount < 1)
        {
            Debug.LogError("SpawnPoints or PlayerColors not assigned!");
            return;
        }

        // ✅ PREVENT OVERFLOW (VERY IMPORTANT)
        if (PlayerCount >= maxPlayerCount)
        {
            Debug.LogWarning($"Max players reached ({maxPlayerCount}). Destroying extra player.");
            Destroy(playerInput.gameObject);
            return;
        }

        // ✅ Cache index BEFORE increment
        int playerIndex = PlayerCount;

        // =========================
        // POSITION PLAYER
        // =========================
        playerInput.transform.position = SpawnPoints[playerIndex].position;
        playerInput.transform.rotation = SpawnPoints[playerIndex].rotation;

        Color color = PlayerColors[playerIndex];

        // =========================
        // SETUP CONTROLLER
        // =========================
        PlayerController controller = playerInput.GetComponent<PlayerController>();

        if (controller == null)
        {
            Debug.LogError("PlayerController missing on player prefab!");
            return;
        }

        controller.AssignPlayerInputDevice(playerInput);
        controller.AssignPlayerNumber(playerIndex + 1); // human-readable
        controller.AssignColor(color);


        PlayerCount++;

        Debug.Log($"Player {PlayerCount} joined.");
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        PlayerCount = Mathf.Max(0, PlayerCount - 1);
        Debug.Log("Player left.");
    }
}
