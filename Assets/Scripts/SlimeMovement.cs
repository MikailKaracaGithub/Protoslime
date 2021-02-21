using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeMovement : MonoBehaviour
{
    private Vector3 rawInputMovement;
    private CharacterController _characterController;

    [SerializeField]
    private float _moveSpeed = 5;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Attacking");
        }
    }
    private void Movement()
    {
        _characterController.Move((rawInputMovement) * _moveSpeed * Time.deltaTime);
    }
    
}
