using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("PLAYER")]
    public float acceleration;
    public float maxWalkVelocity;
    public float maxRunVelocity;
    public float jumpBoost;
    public float gravity;
    public float friction;

    [Header("CAMERA")]
    public Transform cameraRotation;
    public float sensitivity;
    public float lookXLimit = 90.0f;

    public GameObject weaponPrimary;
    public GameObject weaponSecondary;
    private int currentWeapon;

    private CharacterController controller;
    private PlayerInput playerInput;
    
    [Header("ANIMATION")]
    public Animator animator;
    
    private Vector3 velocityXZ;
    private float velocityY;
    private float rotationX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        currentWeapon = 2;
        SwitchWeapon();
    }

    private void Update()
    {
        // CURSOR
        if (playerInput.Pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // FRICTION
        if (velocityXZ.sqrMagnitude <= friction * friction) velocityXZ = Vector2.zero;
        else velocityXZ -= velocityXZ.normalized * friction;

        // GRAVITY
        if (controller.isGrounded) velocityY = 0;
        else velocityY -= gravity;

        var inputDirection = new Vector3(playerInput.Movement.x, 0, playerInput.Movement.y);
        var directionXY = transform.TransformDirection(inputDirection).normalized * acceleration;
        velocityXZ += directionXY;
        var maxVelocity = playerInput.Walk ? maxWalkVelocity : maxRunVelocity;
        if (velocityXZ.sqrMagnitude >= maxVelocity) velocityXZ = velocityXZ.normalized * Mathf.Sqrt(maxVelocity);

        // JUMP
        if (playerInput.Jump && controller.isGrounded)
        {
            velocityY = jumpBoost;
        }

        // SWITCH WEAPONS
        if (playerInput.Weapon1 && currentWeapon != 1)
        {
            animator.SetTrigger("TakeOff");
        }
        if (playerInput.Weapon2 && currentWeapon != 2)
        {
            animator.SetTrigger("TakeOff");
        }

        // MOVE THE CONTROLLER
        controller.Move((velocityXZ + Vector3.up * velocityY) * Time.deltaTime);

        // PLAYER AND CAMERA ROTATION
        rotationX -= playerInput.Look.y * sensitivity / 10;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        cameraRotation.localRotation = Quaternion.Euler(new Vector3(rotationX, 0, 0));
        transform.rotation *= Quaternion.Euler(0, playerInput.Look.x * sensitivity / 10, 0);
        
        // ANIMATION
        animator.SetFloat("Speed", inputDirection.magnitude);
    }

    public void WeaponTakeOffFinished()
    {
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        if (weaponPrimary == null || weaponSecondary == null) return;
        if (currentWeapon == 1)
        {
            weaponPrimary.SetActive(false);
            weaponSecondary.SetActive(true);
            animator = weaponSecondary.GetComponentInChildren<Animator>();
            currentWeapon = 2;
        }
        else if (currentWeapon == 2)
        {
            weaponPrimary.SetActive(true);
            weaponSecondary.SetActive(false);
            animator = weaponPrimary.GetComponentInChildren<Animator>();
            currentWeapon = 1;
        }
    }
}
