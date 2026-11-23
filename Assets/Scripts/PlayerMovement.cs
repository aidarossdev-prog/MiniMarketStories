using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 6f;

    [Header("Potenciador de velocidad")]
    public float boostedSpeed = 8.5f;

    [Header("Efecto visual de velocidad")]
    public TrailRenderer speedTrail;

    [Range(0.1f, 3f)]
    public float boostDuration = 2.5f;

    private float normalSpeed;
    private Coroutine speedBoostCoroutine;
    
    [Header("Detección del suelo")]
    public float groundCheckDistance = 1.15f;
    public LayerMask groundLayer;

    [Header("Controles móviles")]
    public VirtualJoystick joystick;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpRequested;

    void Awake()
    {
    rb = GetComponent<Rigidbody>();
    normalSpeed = moveSpeed;

    // La estela permanece desactivada al iniciar.
    if (speedTrail != null)
    {
        speedTrail.emitting = false;
        speedTrail.Clear();
    }
    }

    void Update()
    {
        // Movimiento con teclado
        Vector2 keyboardInput = Vector2.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                horizontal = -1f;

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                horizontal = 1f;

            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                vertical = 1f;

            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                vertical = -1f;

            keyboardInput = new Vector2(horizontal, vertical).normalized;

            if (keyboard.spaceKey.wasPressedThisFrame)
                RequestJump();
        }

        // Movimiento con joystick virtual
        Vector2 joystickInput = Vector2.zero;

        if (joystick != null)
            joystickInput = joystick.Direction;

        // Priorizar el joystick cuando esté en uso
        moveInput = joystickInput.sqrMagnitude > 0.001f
            ? joystickInput
            : keyboardInput;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(
            moveInput.x * moveSpeed,
            rb.linearVelocity.y,
            moveInput.y * moveSpeed
        );

        rb.linearVelocity = movement;

        if (jumpRequested && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        jumpRequested = false;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );
    }

    // Esta función también servirá para el botón táctil de salto
    public void RequestJump()
    {
        jumpRequested = true;
    }

    public void ActivateSpeedBoost()
{
    if (speedBoostCoroutine != null)
    {
        StopCoroutine(speedBoostCoroutine);
    }

    speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
}

private System.Collections.IEnumerator SpeedBoostRoutine()
{
    // Aumenta temporalmente la velocidad.
    moveSpeed = boostedSpeed;

    // Activa la estela visual.
    if (speedTrail != null)
    {
        speedTrail.Clear();
        speedTrail.emitting = true;
    }

    Debug.Log("¡Speed Boost activado! Velocidad: " + moveSpeed);

    // Mantiene el efecto durante un máximo de 2.5 segundos.
    yield return new WaitForSeconds(
        Mathf.Clamp(boostDuration, 0.1f, 3f)
    );

    // Restaura la velocidad normal.
    moveSpeed = normalSpeed;

    // Detiene la emisión de la estela.
    if (speedTrail != null)
    {
        speedTrail.emitting = false;
    }

    speedBoostCoroutine = null;

    Debug.Log("Speed Boost finalizado. Velocidad: " + moveSpeed);
}

}