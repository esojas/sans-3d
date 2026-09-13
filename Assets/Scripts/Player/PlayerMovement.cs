using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControlScript;
    private PlayerDeath playerDeathScript;
    Vector2 direction;
    Rigidbody rb;
    Ray ray;
    bool jumpPressed = false;
    private float jumpCooldownTimerValue = 0f;
    private Vector3 lastVelocity;
    private int jumpCount = 0;
    private float jumpBufferCounter;
    private float totalJumpForce;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 25f;
    //[SerializeField] private float distanceToGround;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Camera cam;
    [Header("JumpSettings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldownTimer;
    [SerializeField] private int maxJump = 1;
    [SerializeField] private float maxJumpForce = 7f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [Header("Collision")]
    [SerializeField] private Transform groundCheck;

    public bool deathThisFrame = false;
    public bool isVisible;

    // For animator
    //public event Action OnJumpExecuted;
    public bool IsGrounded => isGrounded();
    public float PlanarSpeed => new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

    private void OnDestroy()
    {
        playerControlScript.OnMove -= HandleDirection;
        playerControlScript.OnJumpPressed -= JumpPressed;
        playerControlScript.OnJumpReleased -= JumpReleased;
    }

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControls>();
        rb = GetComponent<Rigidbody>();
        rb.maxDepenetrationVelocity = 2f;
        playerDeathScript = GetComponent<PlayerDeath>();
    }

    void Start()
    {
        playerControlScript.OnMove += HandleDirection;
        playerControlScript.OnJumpPressed += JumpPressed;
        playerControlScript.OnJumpReleased += JumpReleased;
    }

    private void Update()
    {
        ResetJumpCounter();
        HandleJump();
        if (jumpCooldownTimerValue > 0) jumpCooldownTimerValue -= Time.deltaTime;
        if (jumpPressed) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void HandleDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void Movement()
    {
        Transform cam_Transform = cam.transform;
        Vector3 camForward = cam_Transform.forward;
        camForward.y = 0f;
        if (camForward.sqrMagnitude < 0.0001f) camForward = cam_Transform.up; // camera looking straight down/up, fallback
        camForward.Normalize();

        Vector3 camRight = cam_Transform.right;
        camRight.y = 0f;
        camRight.Normalize(); // camRight rarely degenerates unless camera rolls, but keep the pattern consistent

        Vector3 inputDir = (camForward * direction.y + camRight * direction.x);
        inputDir = Vector3.ClampMagnitude(inputDir, 1);

        Vector3 targetVelocity = inputDir * movementSpeed;
        Vector3 currentHorizontal = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        float rate = (inputDir.sqrMagnitude > 0.0001f) ? acceleration : deceleration;
        Vector3 newHorizontal = Vector3.MoveTowards(currentHorizontal, targetVelocity, rate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(newHorizontal.x, rb.linearVelocity.y, newHorizontal.z);

        // Flattened camera basis — also the character's facing basis, since yaw follows the camera
        Vector3 flatForward = camForward; flatForward.y = 0f;
        if (flatForward.sqrMagnitude < 0.0001f) flatForward = cam_Transform.up;
        flatForward.Normalize();
        Vector3 flatRight = Vector3.Cross(Vector3.up, flatForward) * -1f;

        Quaternion camYawRotation = Quaternion.LookRotation(flatForward, Vector3.up);

        Quaternion targetRotation;
        if (inputDir.sqrMagnitude > 0.0001f)
        {
            float forwardAmount = Vector3.Dot(inputDir, flatForward); // -1 (back) .. 1 (fwd)
            float rightAmount = Vector3.Dot(inputDir, flatRight);   // -1 (left) .. 1 (right)

            // Only dive forward — clamp out the backward case so it never lifts the front up
            float pitchAngle = 10 * Mathf.Max(forwardAmount, 0f);
            float rollAngle = 10 * rightAmount;

            Quaternion tilt = Quaternion.AngleAxis(rollAngle, Vector3.forward) * Quaternion.AngleAxis(pitchAngle, Vector3.right);
            targetRotation = camYawRotation * tilt;
        }
        else
        {
            targetRotation = camYawRotation;
        }

        Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
        rb.MoveRotation(newRotation);
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, layerToHit);
    }

    private void ResetJumpCounter()
    {
        if (isGrounded())
        {
            jumpCount = 0;
            totalJumpForce = 0;
        }
    }

    private void HandleJump()
    {
        if (jumpPressed && jumpCount < maxJump && totalJumpForce < maxJumpForce)
        {
            totalJumpForce += jumpForce * Time.deltaTime;
            Debug.Log("TotalJump Force: " + totalJumpForce);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            Debug.Log("Jumpcount is: " + jumpCount);
            jumpCooldownTimerValue = jumpCooldownTimer;
            jumpBufferCounter = 0f;
        }
    }

    public void SetGravityScale(float scale)
    {
        rb.AddForce(Physics.gravity * scale, ForceMode.Acceleration);
    }

    private void JumpPressed()
    {
        if (jumpCooldownTimerValue > 0) return;
        jumpPressed = true;
    }

    private void JumpReleased()
    {
        jumpCount++;
        jumpPressed = false;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null)
        {
            return;
        }

        bool grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, layerToHit);

        Gizmos.color = grounded ? Color.green : Color.red;

        Vector3 startPos = groundCheck.position;

        Gizmos.DrawWireSphere(startPos, groundCheckRadius);
    }
}
