using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public int PlayerNumber { get; private set; }
    [field: SerializeField] public Color PlayerColor { get; private set; }
    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;

    //parameters for shooting projectiles
    [Header("Combat")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 12f;
    [SerializeField] private float rotationSpeed = 720f;

    //parameters for player stats
    [Header("Player Stats")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int currentHP;

    //parameters for ammo management
    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int currentAmmo;

    
    //Spider Event Roles
    public enum SpiderRole
    {
        None, Driver, Shooter, Looter
    }

    public SpiderRole CurrentRole = SpiderRole.None;

    // Input values
    private Vector2 moveInput;
    private Vector2 aimInput;

    // Player input information
    private PlayerInput PlayerInput;
    private InputAction InputActionMove;
    private InputAction InputActionAim;
    private InputAction InputActionFire;

    void Awake()
    {
        currentHP = maxHP;
        currentAmmo = maxAmmo;
        Debug.Log($"[AMMO] Start Ammo: {currentAmmo}");
    }

    // Assign color value on spawn from main spawner
    public void AssignColor(Color color)
    {
        // record color
        PlayerColor = color;

        // Assign to sprite renderer
        if (SpriteRenderer == null)
            Debug.Log($"Failed to set color to {name} {nameof(PlayerController)}.");
        else
            SpriteRenderer.color = color;
    }

    // Set up player input
    public void AssignPlayerInputDevice(PlayerInput playerInput)
    {
        // Record our player input (ie controller).
        PlayerInput = playerInput;
        // Find the references to the "Move" and "Jump" actions inside the player input's action map
        // Here I specify "Player/" but it in not required if assigning the action map in PlayerInput inspector.
        InputActionMove = playerInput.actions.FindAction($"Player/Move");
        InputActionAim = playerInput.actions.FindAction($"Player/Aim");
        InputActionFire = playerInput.actions.FindAction($"Player/Fire");
    }

    // Assign player number on spawn
    public void AssignPlayerNumber(int playerNumber)
    {
        this.PlayerNumber = playerNumber;
    }


    // Runs each frame
    public void Update()
    {
        // Read aim input every frame
        aimInput = InputActionAim.ReadValue<Vector2>();

        if (CurrentRole == SpiderRole.Driver && SpiderEventManager.Instance.spiderEventActive)
        {
            SpiderEventManager.Instance.spiderVehicle.SetMovementInput(moveInput);
            SpiderEventManager.Instance.spiderVehicle.SetRotationInput(moveInput.x);
        }

        RotateTowardAim();

        if (InputActionFire.WasPressedThisFrame())
            FireProjectile();

    }

    // Runs each phsyics update
    void FixedUpdate()
    {// Read movement input in physics update
        moveInput = InputActionMove.ReadValue<Vector2>();

        // If driving, DO NOT move the player body
        if (SpiderEventManager.Instance.spiderEventActive &&
            CurrentRole == SpiderRole.Driver)
        {
            return;
        }

        // Normal player movement
        if (moveInput.sqrMagnitude > 0.01f)
            Rigidbody2D.AddForce(moveInput * MoveSpeed);

    }

    private void RotateTowardAim()
    {
        Vector2 aimInput = InputActionAim.ReadValue<Vector2>();

        // Mouse aiming
        if (PlayerInput.currentControlScheme == "KeyboardMouse")
        {
            Vector3 mouseWorld =
                Camera.main.ScreenToWorldPoint(aimInput);
            Vector2 dir = mouseWorld - transform.position;
            Rotate(dir);
        }
        // Stick aiming
        else if (aimInput.sqrMagnitude > 0.1f)
        {
            Rotate(aimInput);
        }
    }

    private void Rotate(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }
    private void FireProjectile()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        Debug.Log($"[AMMO] Fired. Ammo now: {currentAmmo}");

        // Get the forward direction of the fire point (the right vector in local space)
        // We normalize it so the direction has length of 1
        Vector2 forward = firePoint.right.normalized;

        // OFFSET so it does NOT spawn inside the player
        Vector2 spawnPos = (Vector2)firePoint.position + forward * 0.3f;

        // Instantiate the projectile prefab at the spawn position and with the same rotation as the fire point
        GameObject proj = Instantiate(
            projectilePrefab,
            spawnPos,
            firePoint.rotation
        );

        // Ignore collision between the projectile and the player who fired it
        Collider2D projCol = proj.GetComponent<Collider2D>();
        Collider2D playerCol = GetComponent<Collider2D>();

        // Check if both colliders exist before trying to ignore collision
        if (projCol && playerCol)
        {
            Physics2D.IgnoreCollision(projCol, playerCol);
        }
        // Set the projectile's velocity in the forward direction
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

        // We set gravity scale to 0 and zero out any existing velocity
        // to ensure the projectile moves straight in the intended direction
        // without being affected by gravity or any inherited velocity.
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        //Apply an instantaneous force to the projectile in the forward direction,
        //scaled by projectileSpeed
        rb.AddForce(forward * projectileSpeed, ForceMode2D.Impulse);

    }
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
    }


    // OnValidate runs after any change in the inspector for this script.
    private void OnValidate()
    {
        Reset();
    }

    // Reset runs when a script is created and when a script is reset from the inspector.
    private void Reset()
    {
        // Get if null
        if (Rigidbody2D == null)
            Rigidbody2D = GetComponent<Rigidbody2D>();
        if (SpriteRenderer == null)
            SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
