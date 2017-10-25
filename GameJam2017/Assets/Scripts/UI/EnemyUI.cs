using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private EnemyBehaviour Enemy;
    public Slider EnemyHealthSlider;
	// Use this for initialization
	void Start ()
	{
	    Enemy = GetComponent<EnemyBehaviour>();
	    EnemyHealthSlider.maxValue = Enemy.m_EnemyStats.GetStat("EnemyHealth").Value;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    EnemyHealthSlider.value = Enemy.m_EnemyStats.GetStat("EnemyHealth").Value;
        if(Input.GetKeyDown(KeyCode.Space))
            Enemy.TakeDamage(10);
	}
}
