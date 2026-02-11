using UnityEngine;

public class SpiderVehicle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public Transform driverSeat;
    public Transform shooterSeat;
    public Transform looterSeat;

    private float moveInput;
    private float rotationInput;


    public void SetMovementInput(Vector2 input)
    {
        moveInput = input.y;
    }

    public void SetRotationInput(float input)
    {
        rotationInput = input;
    }

    void Update()
    {
        // TEMP: force movement so we can confirm the spider actually moves
        moveInput = 1f;
        rotationInput = 1f;

        transform.Translate(Vector2.up * moveInput * moveSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward, -rotationInput * rotationSpeed * Time.deltaTime);
    }
}
