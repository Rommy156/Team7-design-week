using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TeamPlayerController owner;
    public int scoreValue = 5;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TeamPlayerController hit =
            other.GetComponent<TeamPlayerController>();

        if (hit != null && hit != owner)
        {
            owner.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
