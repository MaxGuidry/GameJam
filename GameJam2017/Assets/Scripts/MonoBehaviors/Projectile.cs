using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public FloatVariable m_Speed;

    public Vector3 Direction;
	void Update ()
	{
        Debug.Log(m_Speed);
	    this.transform.position += Direction.normalized * m_Speed.Value * Time.deltaTime;
	}
}
