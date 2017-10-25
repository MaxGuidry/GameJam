using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public FloatVariable m_Speed;
    public Vector3 Direction;
    [HideInInspector]public float Damage;

    void Update()
    {
        this.transform.position += Direction.normalized * m_Speed.Value * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            var damageableObject = GetComponent<IDamageable>();
            if (damageableObject != null)
                damageableObject.TakeDamage(Damage);
            Destroy(gameObject);
        }
        else 
            Destroy(gameObject, 2);
    }
}
