using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    // camera set to startpoint variables

    private Vector3 _cameraTarget;
    public GameObject player;

    [SerializeField]
    private float _cameraTargetOffset = 1.46f;

    [SerializeField]
    private float _distanceZ = 6.0f;

    // camera rotation variables

    private float _rotationX, _rotationY;

    //x rotation limit
    private const float MIN_X = -12;

    private const float MAX_X = 20;

    //zoom limit
    private const float MIN_Z = -2;

    private const float MAX_Z = 8;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _cameraTarget = player.transform.position;
        _cameraTarget.y += _cameraTargetOffset;

       // _rotationY += Input.GetAxis("Mouse X");
       // _rotationX -= Input.GetAxis("Mouse Y");
       //
        _rotationX = Mathf.Clamp(_rotationX, MIN_X, MAX_X);

        //zooming in and out
        _distanceZ -= Input.GetAxis("Mouse ScrollWheel");
        _distanceZ = Mathf.Clamp(_distanceZ, MIN_Z, MAX_Z);
    }

    private void LateUpdate()
    {
        Quaternion cameraRotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        // camera set to startpoint
        Vector3 cameraOffset = new Vector3(0, 0, -_distanceZ);
        transform.position = _cameraTarget + cameraRotation * cameraOffset;
        transform.LookAt(_cameraTarget);
    }

    public Vector3 GetForwardVector()
    {
        Quaternion rotation = Quaternion.Euler(0, _rotationY, 0);
        return rotation * Vector3.forward;
    }

}