using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 3f;
    public float smoothSpeed = 10f;

    float yaw;

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        yaw += mouseX;

        Quaternion targetRotation = Quaternion.Euler(0, yaw, 0);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }
}
