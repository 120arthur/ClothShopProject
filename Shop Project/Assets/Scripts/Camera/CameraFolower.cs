using UnityEngine;

public class CameraFolower : MonoBehaviour
{
    [SerializeField]
    private Transform m_target;
    [SerializeField]
    private float m_smoothSpeed = 5f;

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
