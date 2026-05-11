using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Pelaajan transform
    public float smoothSpeed = 0.125f; // Kameran pehmeys (0-1)
    public Vector3 offset;         // Etäisyys pelaajaan (esim. Z = -10)

    void LateUpdate()
    {
        // Kameran tavoitesijainti
        Vector3 desiredPosition = target.position + offset;

        // Pehmeä liike kohti tavoitetta
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
