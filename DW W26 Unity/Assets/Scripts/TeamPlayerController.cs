using UnityEngine;
using UnityEngine.InputSystem;

public class TeamPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    [Header("Combat")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 12f;

    [Header("Score")]
    public int teamScore;

    [Header("Ammo")]
    public int maxAmmo = 10;
    private int currentAmmo;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction fireAction;

    private PlayerInput movementPlayer;
    private PlayerInput shooterPlayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAmmo = maxAmmo;
    }

    // ===============================
    // ASSIGN PLAYERS
    // ===============================

    public void AssignMovementPlayer(PlayerInput input)
    {
        movementPlayer = input;
        moveAction = input.currentActionMap.FindAction("Move", true);
        moveAction.Enable();
        Debug.Log("Movement player assigned");
    }

    public void AssignShooterPlayer(PlayerInput input)
    {
        shooterPlayer = input;
        aimAction = input.currentActionMap.FindAction("Aim", true);
        fireAction = input.currentActionMap.FindAction("Fire", true);

        aimAction.Enable();
        fireAction.Enable();

        Debug.Log("Shooter player assigned");
    }

    // ===============================
    // UPDATE
    // ===============================

    void Update()
    {
        HandleAim();

        if (fireAction != null && fireAction.WasPressedThisFrame())
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        if (moveAction == null) return;

        Vector2 move = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = move * moveSpeed;
    }

    // ===============================
    // AIM
    // ===============================

    void HandleAim()
    {
        if (aimAction == null) return;

        Vector2 aim = aimAction.ReadValue<Vector2>();
        if (aim.sqrMagnitude < 0.1f) return;

        float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ===============================
    // FIRE
    // ===============================

    void Fire()
    {
        if (currentAmmo <= 0) return;
        if (projectilePrefab == null) return;
        if (firePoint == null) return;

        currentAmmo--;

        GameObject proj = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D prb = proj.GetComponent<Rigidbody2D>();

        // IMPORTANT: use velocity not AddForce
        prb.linearVelocity = firePoint.up * projectileSpeed;

        proj.GetComponent<Projectile>().owner = this;
    }

    // ===============================
    // AMMO + SCORE
    // ===============================

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        Debug.Log("Ammo: " + currentAmmo);
    }

    public void AddScore(int amount)
    {
        teamScore += amount;
        Debug.Log("Score: " + teamScore);
    }
}
