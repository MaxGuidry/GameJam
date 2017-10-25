using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 1;
    private Vector3 deltaMouse;
    private Vector3 mouse;
    //private float y_offset;
    //private Vector3 offsetVec;
    private Vector3 prevPlayerPos;

    private float NormalDist;

    private bool idkHowElsetoDothis = false;
    // Use this for initialization
    void Start()
    {
        mouse = Input.mousePosition;
        prevPlayerPos = player.transform.position;
        //y_offset = this.transform.position.y - player.transform.position.y;
        //offsetVec = this.transform.position - player.transform.position;
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        deltaMouse = (Input.mousePosition - mouse);

        transform.RotateAround(player.transform.position, new Vector3(0, 1f, 0), deltaMouse.x * (sensitivity / 180f));
        Vector3 deltaPlayer = player.transform.position - prevPlayerPos;
        this.transform.position += deltaPlayer; //new Vector3(deltaPlayer.x,0,deltaPlayer.z);
        Vector3 pos = this.transform.position;
        transform.RotateAround(player.transform.position, this.transform.right, -deltaMouse.y * (sensitivity / 180f));
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
            this.transform.position -= .005f * -deltaMouse.y *  (player.transform.position - this.transform.position);
        }
        ////this.transform.position = new Vector3(this.transform.position.x , y_offset + player.transform.position.y, this.transform.position.z );
        ////if ((this.transform.position - player.transform.position).magnitude != offsetVec.magnitude)
        ////{
        ////    this.transform.position = ((this.transform.position - player.transform.position).normalized * offsetVec.magnitude) + player.transform.position;

        ////}
        transform.LookAt(player.transform.position + new Vector3(0, 2f, 0));
        mouse = Input.mousePosition;
        prevPlayerPos = player.transform.position;
    }
}