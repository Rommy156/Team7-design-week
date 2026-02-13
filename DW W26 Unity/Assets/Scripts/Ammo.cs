using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 10;
    private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        TeamPlayerController player = other.GetComponentInParent<TeamPlayerController>();
        if(audioManager == null)
        {
            audioManager = FindAnyObjectByType<AudioManager>();
        }
        if (player != null)
        {
            Debug.Log("Picked up ammo");
            player.AddAmmo(ammoAmount);
            audioManager.PlaySound("ammoPickup");
            Destroy(gameObject);
        }
    }
}