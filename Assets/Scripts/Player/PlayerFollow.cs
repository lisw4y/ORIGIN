using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    [Range(0.01f, 1.0f)] public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationsSpeed = 5.0f;
    public float CameraPitchMin = 1.5f;
    public float CameraPitchMax = 6.5f;
    public float yAxisAmend = 1.2f;

    Vector3 _cameraOffset;

    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    private bool IsRotateActive
    {
        get
        {
            if (!RotateAroundPlayer)
                return false;

            if (Input.GetMouseButton(0))
                return true;

            return false;
        }
    }

    void LateUpdate()
    {
        if (IsRotateActive)
        {

            float h = Input.GetAxis("Mouse X") * RotationsSpeed;

            Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);

            Vector3 newCameraOffset = camTurnAngle * _cameraOffset;

            // Limit camera pitch
            if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
            {
                newCameraOffset = camTurnAngle * _cameraOffset;
            }

            _cameraOffset = newCameraOffset;

        }

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + yAxisAmend, PlayerTransform.position.z));
    }
}