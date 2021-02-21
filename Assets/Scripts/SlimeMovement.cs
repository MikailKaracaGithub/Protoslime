using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeMovement : MonoBehaviour
{
    private Vector3 rawInputMovement;
    //private CharacterController _characterController;

    [SerializeField]
    private float _moveSpeed = 5;

    private Rigidbody _playerBody;

    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _health = 10f;
    void Start()
    {
        //   _characterController = GetComponent<CharacterController>();
        _playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Death();
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
        //_characterController.Move((rawInputMovement) * _moveSpeed * Time.deltaTime);
        transform.LookAt(transform.position + new Vector3(rawInputMovement.x, 0, rawInputMovement.z));
        _playerBody.velocity = rawInputMovement * _moveSpeed;
        
    }
    
    public void Shrink(InputAction.CallbackContext value)
    {
        transform.localScale = transform.localScale * 0.95f;
    }
    public void Grow(InputAction.CallbackContext value)
    {
        transform.localScale = transform.localScale * 1.05f;
    }
    public void Grow()
    {
        transform.localScale = transform.localScale * 1.05f;
        _health++;
    }

    public void Death()
    {

        if (transform.localScale.x < 0.1f)
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }
    public void IncreaseSpeed()
    {
        _moveSpeed++;
    }
    public void DoDmg()
    {
       
    }
}
