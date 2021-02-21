using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _movementControl;

    [SerializeField]
    private InputActionReference _jumpControl;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private Transform _cameraMainTransform;
    [SerializeField] 
    private float _rotationSpeed = 5f;

    private void OnEnable()
    {
        _movementControl.action.Enable();
        _jumpControl.action.Enable();
    }
    private void OnDisable()
    {
        _movementControl.action.Disable();
        _jumpControl.action.Disable();
    }
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = _movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        move = _cameraMainTransform.forward * move.z + _cameraMainTransform.right * move.x;
        move.y = 0;

        controller.Move(move * Time.deltaTime * playerSpeed);

       //if (move != Vector3.zero)
       //{
       //    gameObject.transform.forward = move;
       //}

        // Changes the height position of the player..
        if (_jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + _cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f,targetAngle ,0f );

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }

    }
}
