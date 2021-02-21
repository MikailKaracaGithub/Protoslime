using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;

    private GameObject _camera;
    private CameraLogic _cameraLogic;

    [SerializeField]
    private float _moveSpeed = 5;

    private Vector3 rawInputMovement;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        _camera = Camera.main.gameObject;
        if (_camera)
        {
            _cameraLogic = _camera.GetComponent<CameraLogic>();
        }
    }

    private void Update()
    {
        PlayerMovement();
        FaceForward();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }


 private void FaceForward()
 {
         if (_cameraLogic && (Mathf.Abs(rawInputMovement.x) > 0.1f || Mathf.Abs(rawInputMovement.z) > 0.1f))
         {
             transform.forward = _cameraLogic.GetForwardVector();
         }
 }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    private void PlayerMovement()
    {
        _characterController.Move((rawInputMovement) * _moveSpeed * Time.deltaTime);
        
    }
  // private void PlayerCombatInput()
  // {
  //     if (Input.GetButtonDown("X"))
  //     {
  //         _animationControl.ComboStarter(false);
  //     }
  //    
  // }
}