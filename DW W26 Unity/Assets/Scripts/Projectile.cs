using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 10;


    void Start()
    {
        Destroy(gameObject, lifeTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only damage objects tagged "Player"
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(damage); // call function on THAT player
        }

        Destroy(gameObject); // destroy projectile after hit
    }
}
