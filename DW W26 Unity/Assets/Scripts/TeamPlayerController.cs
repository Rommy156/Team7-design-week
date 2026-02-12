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
    public float projectileForce = 12f;
    public float rotationSpeed = 720f;

    [Header("Health")]
    public int maxHP = 100;
    private int currentHP;

    [Header("Score")]
    public int teamScore;

    // PlayerInput references
    private PlayerInput movementPlayer;
    private PlayerInput shooterPlayer;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction fireAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    public void AssignMovementPlayer(PlayerInput input)
    {
        movementPlayer = input;

        moveAction = input.currentActionMap.FindAction("Move", true);
        moveAction.Enable();

        Debug.Log("Move value: " + moveAction);
        Debug.Log("Move action assigned: " + moveAction);
    }

    public void AssignShooterPlayer(PlayerInput input)
    {
        shooterPlayer = input;

        aimAction = input.actions.FindAction("Aim", true);
        fireAction = input.actions.FindAction("Fire", true);

        aimAction.Enable();
        fireAction.Enable();

        Debug.Log("Aim + Fire assigned");
    }

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
        if (moveAction != null)
        {
            Vector2 move = moveAction.ReadValue<Vector2>();
            rb.AddForce(move * moveSpeed);
        }

    }

    void HandleAim()
    {
        if (aimAction == null) return;

        Vector2 aim = aimAction.ReadValue<Vector2>();
        if (aim.sqrMagnitude < 0.1f) return;

        float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Fire()
    {
        GameObject proj = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D prb = proj.GetComponent<Rigidbody2D>();
        prb.AddForce(firePoint.right * projectileForce, ForceMode2D.Impulse);

        proj.GetComponent<Projectile>().owner = this;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Team died");
        Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        teamScore += amount;
        Debug.Log("Score: " + teamScore);
    }
}
