using UnityEngine;
using UnityEngine.InputSystem;

public class TeamPlayerController : MonoBehaviour
{
    [field: SerializeField] public int PlayerNumber { get; private set; }
    [field: SerializeField] public Color PlayerColor { get; private set; }
    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    [Header("Combat")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 12f;

    [Header("Score")]
    public int teamScore;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 10;
    private int currentAmmo;

    // INPUT ACTIONS
    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction fireAction;

    // ===============================
    // AWAKE
    // ===============================
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
            Debug.LogError($"{name} missing Rigidbody2D!");

        currentAmmo = maxAmmo;
    }

    // ===============================
    // PLAYER SETUP
    // ===============================

    public void AssignColor(Color color)
    {
        PlayerColor = color;

        if (SpriteRenderer != null)
            SpriteRenderer.color = color;
        else
            Debug.LogWarning($"{name} missing SpriteRenderer");
    }

    public void AssignPlayerNumber(int playerNumber)
    {
        PlayerNumber = playerNumber;
    }

    // MOVEMENT PLAYER (driver)
    public void AssignMovementPlayer(PlayerInput input)
    {
        moveAction = input.currentActionMap.FindAction("Move", true);
        moveAction.Enable();
        Debug.Log("Movement player assigned");
    }

    // SHOOTER PLAYER (gunner)
    public void AssignShooterPlayer(PlayerInput input)
    {
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
            Fire();
    }

    // ===============================
    // MOVEMENT
    // ===============================
    void FixedUpdate()
    {
        if (rb == null || moveAction == null) return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector2 targetVelocity = moveInput * moveSpeed;

        rb.linearVelocity = Vector2.Lerp(
            rb.linearVelocity,
            targetVelocity,
            10f * Time.fixedDeltaTime
        );
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
        if (firePoint == null) return;

        currentAmmo--;

        GameObject proj = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D prb = proj.GetComponent<Rigidbody2D>();
        prb.linearVelocity = firePoint.up * projectileSpeed;

        proj.GetComponent<Projectile>().owner = this;
    }

    // ===============================
    // AMMO + SCORE
    // ===============================
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
    }

    public void AddScore(int amount)
    {
        teamScore += amount;
        GameManager.Instance.UpdateScoreUI(teamScore);
    }
}
