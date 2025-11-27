using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraPosition;
    public MobileInputManager InputManager;
    [Header("Настройки управления")] public float mouseSensitivity = 5.0f;
    public float playerSpeed = 5.0f;
    public float jumpHeight = 2.0f;

    private float verticalAngle = 0.0f;
    private float horizontalAngle = 0.0f;

    private float verticalSpeed = 0.0f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
         Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }


    void HandleMovement()
    {
        float speed = playerSpeed;

        float moveHorizontal = SimpleInput.GetAxis("Horizontal");
        float moveVertical = SimpleInput.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (moveDirection.magnitude > 1.0f)
            moveDirection.Normalize();

        moveDirection = transform.TransformDirection(moveDirection);


        moveDirection *= speed * Time.deltaTime;

        if (characterController.isGrounded)
        {
            if (SimpleInput.GetButtonDown("Jump"))
            {
                verticalSpeed = Mathf.Sqrt(2.0f * jumpHeight * Mathf.Abs(Physics.gravity.y));
            }
        }

        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        moveDirection.y = verticalSpeed * Time.deltaTime;

        characterController.Move(moveDirection);
    }

    void HandleMouseLook()
    {
        float mouseX = 0;
        float mouseY = 0;
        if (InputManager.IsMobileDevice())
        {
             mouseX = SimpleInput.GetAxis("Mouse X") * mouseSensitivity;
            mouseY = -SimpleInput.GetAxis("Mouse Y") * mouseSensitivity;
        }
        else
        {
             mouseX = Input.GetAxis("LookX") * mouseSensitivity;
             mouseY = -Input.GetAxis("LookY") * mouseSensitivity;
        }

        horizontalAngle += mouseX;
        verticalAngle = Mathf.Clamp(verticalAngle + mouseY, -89.0f, 89.0f);

        transform.localRotation = Quaternion.Euler(0.0f, horizontalAngle, 0.0f);
        cameraPosition.localRotation = Quaternion.Euler(verticalAngle, 0.0f, 0.0f);
    }
}