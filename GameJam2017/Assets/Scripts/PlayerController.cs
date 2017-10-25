using UnityEngine;

// ReSharper disable InconsistentNaming
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{ 
    public FloatVariable m_Speed, m_JumpForce, m_Gravity;
    private Vector3 m_Direction = Vector3.zero;
    private CharacterController m_PlayerController;

    private void Start()
    {
        m_PlayerController = gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (m_PlayerController.isGrounded)
        {
            m_Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            m_Direction = transform.TransformDirection(m_Direction);
            m_Direction *= m_Speed.Value;

            if (Input.GetButtonDown("Jump"))
                m_Direction.y = m_JumpForce.Value;
        }

        m_Direction.y -= m_Gravity.Value * Time.deltaTime;
        m_PlayerController.Move(m_Direction * Time.deltaTime);
    }
}

