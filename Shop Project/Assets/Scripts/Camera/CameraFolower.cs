using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolower : MonoBehaviour
{
    public Transform m_target; // The target the camera will follow
    public float m_smoothSpeed = 5f; // Adjust the smoothness of the follow

    private void LateUpdate()
    {
        if (m_target != null)
        {
            Vector3 desiredPosition = new Vector3(m_target.position.x, m_target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
