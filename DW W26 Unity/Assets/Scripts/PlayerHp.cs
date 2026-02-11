

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    public int currentHP;
    public int damage = 20;

    private bool isDead = false;
    private void Awake()
    {
        // Start player at full health
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);

        Debug.Log($"{gameObject.name} HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log(gameObject.name + " died.");

        // Disable player
        gameObject.SetActive(false);

        // OR you could:
        // Destroy(gameObject);
    }

}