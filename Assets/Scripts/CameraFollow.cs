using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float _cameraMoveSpeed = 360f;
    [SerializeField]
    private GameObject _cameraFollowObj;
    private float _clampAngle = 80f;
    private float _inputSensitivity = 150f;
    [SerializeField]
    private float _mouseX;
    private float _mouseY;
    private float _finalInputX;
    private float _finalInputZ;
    private float _rotY = 0f;
    private float _rotX = 0f;



    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        _rotY = rot.y;
        _rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        float inputX = Input.GetAxis("JoystickHorizontal");
        float inputZ = Input.GetAxis("JoystickVertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _finalInputX = inputX + _mouseX;
        _finalInputZ = inputZ + _mouseY;

        _rotY += _finalInputX * _inputSensitivity * Time.deltaTime;
        _rotX += _finalInputZ * _inputSensitivity * Time.deltaTime;

        _rotX = Mathf.Clamp(_rotX, -_clampAngle, _clampAngle);
        Quaternion localRotation = Quaternion.Euler(_rotX, _rotY, 0f);
        transform.rotation = localRotation;

        CameraUpdater();
    }

    private void LateUpdate()
    {
        
    }

    private void CameraUpdater()
    {
        Transform target = _cameraFollowObj.transform;

        float step = _cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
