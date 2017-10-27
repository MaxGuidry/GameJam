using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.transform.root.gameObject)
            return;
        IDamager attacker = this.transform.root.gameObject.GetComponent<IDamager>();
        IDamageable defender = other.gameObject.GetComponent<IDamageable>();
        if (attacker == null || defender == null)
            return;
        attacker.DoDamage(defender);

    }

  

}
