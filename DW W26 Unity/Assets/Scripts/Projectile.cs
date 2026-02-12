using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TeamPlayerController owner;
    public float lifeTime = 3f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        TeamPlayerController hitPlayer =
            other.GetComponent<TeamPlayerController>();

        if (hitPlayer != null && hitPlayer != owner)
        {
            hitPlayer.TakeDamage(damage);
            owner.AddScore(5); // 5 points per hit
        }

        Destroy(gameObject);
    }
}
