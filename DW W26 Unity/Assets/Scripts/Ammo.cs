using UnityEngine;

public class Ammo : MonoBehaviour
{
<<<<<<< main
    [SerializeField] private int ammoAmount = 10;
=======
    [SerializeField] private int ammoAmount = 5;
    public AudioManager Manager;

>>>>>>> Alice
    private void OnTriggerEnter2D(Collider2D other)
    {
        TeamPlayerController player = other.GetComponentInParent<TeamPlayerController>();

<<<<<<< main
        if (player != null)
        {
            Debug.Log("Picked up ammo");
            player.AddAmmo(ammoAmount);   
            Destroy(gameObject);
        }
=======
        // play sound
        if(Manager == null)
        {
            Manager = FindAnyObjectByType<AudioManager>();
        }
        Manager.PlaySound("ammoPickup");

        // Remove pickup
        Destroy(gameObject);
>>>>>>> Alice
    }
}