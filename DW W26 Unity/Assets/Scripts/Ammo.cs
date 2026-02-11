using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only players can pick this up
        if (!other.CompareTag("Player"))
            return;

        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player == null)
            return;

        // Give ammo
        player.AddAmmo(ammoAmount);

        // Remove pickup
        Destroy(gameObject);
    }
}