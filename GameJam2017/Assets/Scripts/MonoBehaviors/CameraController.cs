using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class CameraController : MonoBehaviour
{
    public GameObject player;


    private Vector3 deltaMouse;
    //private float y_offset;
    //private Vector3 offsetVec;
    private Vector3 prevPlayerPos;

    private float NormalDist;
    private float distance;
    private bool idkHowElsetoDothis = false;
    // Use this for initialization
    void Start()
    {

        Camera.main.gameObject.transform.position = player.transform.position + new Vector3(0f, 3f, -7f);
        prevPlayerPos = player.transform.position;
        distance = (this.transform.position - player.transform.position).magnitude;
    }


    // Update is called once per frame
    void Update()
    {
        

    }
    private void LateUpdate()
    {
        deltaMouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 10f;



        if (deltaMouse.y > 50f)
            deltaMouse.y = 50f;
        transform.RotateAround(player.transform.position, new Vector3(0, 1f, 0), deltaMouse.x * (InputMap.Sensitivity / 180f));
        Vector3 deltaPlayer = player.transform.position - prevPlayerPos;
        this.transform.position += deltaPlayer; //new Vector3(deltaPlayer.x,0,deltaPlayer.z);
        Vector3 pos = this.transform.position;

        transform.RotateAround(player.transform.position, this.transform.right, -deltaMouse.y * (InputMap.Sensitivity / 180f));
        if ((this.transform.position - player.transform.position).magnitude < .3f)
            this.transform.position = pos;
        if (this.transform.position.y > player.transform.position.y + 5.5f)
            this.transform.position = pos;
        float dist = (this.transform.position - player.transform.position).magnitude;
        if (this.transform.position.y < player.transform.position.y + .1f)
        {
            if (!idkHowElsetoDothis)
            {
                NormalDist = (this.transform.position - player.transform.position).magnitude;
                idkHowElsetoDothis = true;
            }
            this.transform.position = pos;
            if ((this.transform.position - player.transform.position).magnitude > 2)
            {
                Vector3 move = .005f * deltaMouse.y * (player.transform.position - this.transform.position);
                this.transform.position += new Vector3(move.x, 0, move.z);

            }

        }
        else if ((this.transform.position - player.transform.position).magnitude < NormalDist && deltaMouse.y < 0)
        {
            this.transform.position = pos;
            this.transform.position -= .005f * -deltaMouse.y * (player.transform.position - this.transform.position);

        }

        else if (dist < distance && this.transform.position.y >= player.transform.position.y + .2f)
        {
            Vector3 playerpos = player.transform.position + new Vector3(0, 2f, 0);
            Vector3 camerapos = this.transform.position;
            Vector3 newpos = ((camerapos - playerpos).normalized * distance) + playerpos;
            this.transform.position = newpos;
            Quaternion origin = this.transform.rotation;
            transform.LookAt(player.transform.position + new Vector3(0, 2f, 0));
            dist = (this.transform.position - player.transform.position).magnitude;
            RaycastHit rhit;
            Vector3 right = this.transform.right * Mathf.Sin(50 / dist * Mathf.Deg2Rad * .5f);
            Vector3 dir = new Quaternion(right.x, right.y, right.z, Mathf.Cos(50 / dist * Mathf.Deg2Rad * .5f)) * this.transform.forward;
            if (Physics.Raycast(new Ray(this.transform.position, dir), out rhit, 15))
                if (rhit.transform.gameObject != player.gameObject)
                {
                    this.transform.position = camerapos;
                    this.transform.rotation = origin;
                }
        }
        else if (dist > distance && this.transform.position.y >= player.transform.position.y + .2f)
        {
            Vector3 playerpos = player.transform.position + new Vector3(0, 2f, 0);
            Vector3 camerapos = this.transform.position;
            Vector3 newpos = ((camerapos - playerpos).normalized * distance) + playerpos;
            this.transform.position = newpos;
            Quaternion origin = this.transform.rotation;
            transform.LookAt(player.transform.position + new Vector3(0, 2f, 0));
            dist = (this.transform.position - player.transform.position).magnitude;
            RaycastHit rhit;
            Vector3 right = this.transform.right * Mathf.Sin(50 / dist * Mathf.Deg2Rad * .5f);
            Vector3 dir = new Quaternion(right.x, right.y, right.z, Mathf.Cos(50 / dist * Mathf.Deg2Rad * .5f)) * this.transform.forward;
            if (Physics.Raycast(new Ray(this.transform.position, dir), out rhit, 15))
                if (rhit.transform.gameObject != player.gameObject)
                {
                    this.transform.position = camerapos;
                    this.transform.rotation = origin;
                }


        }
        if ((this.transform.position - player.transform.position).y > 6f)
        {
            this.transform.position = player.transform.position + new Vector3(0, 2f, 0);
        }
        
        transform.LookAt(player.transform.position + new Vector3(0, 2f, 0));
        RaycastHit hit;
        int i = 0;
        
        Vector3 v = this.transform.right * Mathf.Sin(50 / dist * Mathf.Deg2Rad * .5f);
        Vector3 ray = new Quaternion(v.x, v.y, v.z, Mathf.Cos(50 / dist * Mathf.Deg2Rad * .5f)) * this.transform.forward;
        //Debug.DrawRay(this.transform.position, ray.normalized * 15f, Color.blue);

        if (Physics.Raycast(new Ray(this.transform.position, ray), out hit, 15f))
            if (hit.transform != null)
            {
                while (hit.transform.gameObject.GetComponent<PlayerController>() == null)
                {
                    Physics.Raycast(new Ray(this.transform.position, ray), out hit, 15f);
                    this.transform.position += this.transform.forward * .1f;
                    transform.LookAt(player.transform.position + new Vector3(0, 2f, 0));
                    i++;
                    if (i > 700)
                    {
                        break;
                    }
                    if (hit.transform.gameObject == null)
                        break;
                }

                if (i != 0)
                {
                    this.transform.position += this.transform.forward * .3f;
                }
            }

        prevPlayerPos = player.transform.position;
    }
}