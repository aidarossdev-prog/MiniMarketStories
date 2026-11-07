using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Personaje")]
    public Transform target;

    [Header("Configuración de cámara")]
    public Vector3 offset = new Vector3(0f, 6f, -7f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            offset.y,
            target.position.z + offset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
    }