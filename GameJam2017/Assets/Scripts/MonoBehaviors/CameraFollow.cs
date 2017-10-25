using UnityEngine;

// ReSharper disable InconsistentNaming
public class CameraFollow : MonoBehaviour
{
    public Vector3 m_Offset;
    public FloatVariable m_SmoothSpeed;
    public Transform m_Target;
    public BoolVariable LookAt;

    private void LateUpdate()
    {
        if (!m_Target) return;
        var desiredPos = m_Target.position + m_Offset;
        var smoothPos = Vector3.Lerp(transform.position, desiredPos, m_SmoothSpeed.Value);
        transform.position = smoothPos;
        if (LookAt.Value)
            transform.LookAt(m_Target);

    }
}