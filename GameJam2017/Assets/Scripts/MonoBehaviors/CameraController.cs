﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 1;
    private Vector3 deltaMouse;
    //private float y_offset;
    //private Vector3 offsetVec;
    private Vector3 prevPlayerPos;

    private float NormalDist;

    private bool idkHowElsetoDothis = false;
    // Use this for initialization
    void Start()
    {

        Camera.main.gameObject.transform.position = player.transform.position + new Vector3(0f, 3f, -7f);
        prevPlayerPos = player.transform.position;
        //y_offset = this.transform.position.y - player.transform.position.y;
        //offsetVec = this.transform.position - player.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.mousePosition.x > Screen.width)
        //{
        //    PropertyInfo prop = typeof(Input).GetProperty("mousePosition");
        //    prop.SetValue(prop.GetValue(null, null), new Vector3(0, Input.mousePosition.y, 0), null);
        //}
    }
    private void LateUpdate()
    {

        deltaMouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 10f; //(Input.mousePosition - mouse);
          
       
        if (deltaMouse.y > 50f)
            deltaMouse.y = 50f;
        transform.RotateAround(player.transform.position, new Vector3(0, 1f, 0), deltaMouse.x * (sensitivity / 180f));
        Vector3 deltaPlayer = player.transform.position - prevPlayerPos;
        this.transform.position += deltaPlayer; //new Vector3(deltaPlayer.x,0,deltaPlayer.z);
        Vector3 pos = this.transform.position;

        transform.RotateAround(player.transform.position, this.transform.right, -deltaMouse.y * (sensitivity / 180f));
        if ((this.transform.position - player.transform.position).magnitude < .3f)
            this.transform.position = pos;
        if (this.transform.position.y > player.transform.position.y + 5.5f)
            this.transform.position = pos;
        if (this.transform.position.y < player.transform.position.y + .1f)
        {
            if (!idkHowElsetoDothis)
            {
                NormalDist = (this.transform.position - player.transform.position).magnitude;
                idkHowElsetoDothis = true;
            }
            this.transform.position = pos;
            if ((this.transform.position - player.transform.position).magnitude > 2)
                this.transform.position += .005f * deltaMouse.y * (player.transform.position - this.transform.position);


        }
        else if ((this.transform.position - player.transform.position).magnitude < NormalDist && deltaMouse.y < 0)
        {
            this.transform.position = pos;
            this.transform.position -= .005f * -deltaMouse.y * (player.transform.position - this.transform.position);
        }
        ////this.transform.position = new Vector3(this.transform.position.x , y_offset + player.transform.position.y, this.transform.position.z );
        ////if ((this.transform.position - player.transform.position).magnitude != offsetVec.magnitude)
        ////{
        ////    this.transform.position = ((this.transform.position - player.transform.position).normalized * offsetVec.magnitude) + player.transform.position;

        ////}
        transform.LookAt(player.transform.position + new Vector3(0, 2f, 0));
       

        prevPlayerPos = player.transform.position;
    }
}