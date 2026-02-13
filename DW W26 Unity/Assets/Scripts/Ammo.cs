using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 10;
    TeamPlayerController TeamPlayerController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        TeamPlayerController player = other.GetComponentInParent<TeamPlayerController>();

        if (player != null)
        {
            Debug.Log("Picked up ammo");
            player.AddAmmo(ammoAmount);   
            Destroy(gameObject);
        }
    }
}