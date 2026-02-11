using UnityEngine;

public class SeatTrigger : MonoBehaviour
{
    public enum SeatType { Driver, Shooter, Looter }
    public SeatType seatType;

    private SpiderVehicle spiderVehicle;

    private void Awake()
    {
        spiderVehicle = GetComponentInParent<SpiderVehicle>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to objects tagged as Player
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        if (spiderVehicle == null)
        {
            Debug.LogError("SeatTrigger: spiderVehicle is NULL! Make sure this trigger is inside the SpiderVehicle prefab.");
            return;
        }

        // Snap player to the correct seat
        switch (seatType)
        {
            case SeatType.Driver:
                player.transform.SetParent(spiderVehicle.driverSeat);
                break;

            case SeatType.Shooter:
                player.transform.SetParent(spiderVehicle.shooterSeat);
                break;

            case SeatType.Looter:
                player.transform.SetParent(spiderVehicle.looterSeat);
                break;
        }

        player.transform.localPosition = Vector3.zero;

        SpiderEventManager.Instance.AssignPlayerToSeat(player, seatType);
    }
}