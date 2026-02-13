using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5;
    public AudioManager Manager;

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

        // play sound
        if(Manager == null)
        {
            Manager = FindAnyObjectByType<AudioManager>();
        }
        Manager.PlaySound("ammoPickup");

        // Remove pickup
        Destroy(gameObject);
    }
}