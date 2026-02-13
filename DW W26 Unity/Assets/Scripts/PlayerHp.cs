using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    public int currentHP;

    private void Awake()
    {
        // Start player at full health
        currentHP = maxHP;
    }

    // Call this when the player takes damage
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // Clamp HP so it never goes below zero
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // Check if player is dead
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");

        // Disable player controls (simple approach)
        gameObject.SetActive(false);

    }
}
