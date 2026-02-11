using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        // If spider event active, only looter can pick up
        if (SpiderEventManager.Instance != null &&
            SpiderEventManager.Instance.spiderEventActive)
        {
            if (player.CurrentRole != PlayerController.SpiderRole.Looter)
                return;

            if (player.transform.parent != null)
                return;
        }

        //  Add ammo
        player.AddAmmo(ammoAmount);

        Debug.Log("Ammo picked up!");

        // Destroy pickup
        Destroy(gameObject);
    }

}
