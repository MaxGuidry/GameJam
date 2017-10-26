using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSoundEffect : MonoBehaviour
{
    private AudioSource source;
	void Start ()
	{
	    source = this.gameObject.AddComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	    GameObject g = EventSystem.current.currentSelectedGameObject;
        if(g!= null)
	        if (g.GetComponent<Button>() != null)
	        {
	            source.clip = Resources.Load<AudioClip>("Click");
                source.Play();
	        }
	}
}
