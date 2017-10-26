using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IDamager
{
    public float WalkSpeed = 1;
    public float RunSpeed = 2;

    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 position;
    private Rigidbody rb;
    private Animator anim;

    public StatSciptable stats;

    //public Player player;
    // Use this for initialization
    void Start()
    {
        // player = ScriptableObject.CreateInstance<Player>();
        // player.health = 10;
        //player.damage = 10;
        stats._Alive = true;
        stats.GetStat("PlayerDamage").Value = 10;
        stats.GetStat("PlayerHealth").Value = 100;
        stats.GetStat("PlayerStamina").Value = 50;
        rb = GetComponent<Rigidbody>();
        position = this.transform.position;
        anim = GetComponent<Animator>();
        acceleration = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

    }

    // Update is called once per frame
    void Update()
    {


        position = this.transform.position;
        acceleration = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        acceleration *= 20;


        if (Input.GetKeyDown(KeyCode.None))
            Debug.Log("none");
        float Speed = (Input.GetKey(InputMap.KeyBinds["Sprint"])) ? RunSpeed : WalkSpeed;
        if (Input.GetKey(InputMap.KeyBinds["Forward"]) && Input.GetKey(InputMap.KeyBinds["Left"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = -45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Forward"]) && Input.GetKey(InputMap.KeyBinds["Left"]))
        {
            float mag = acceleration.magnitude;
            float angle = -45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }

        else if (Input.GetKey(InputMap.KeyBinds["Forward"]) && Input.GetKey(InputMap.KeyBinds["Right"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = 45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Forward"]) && Input.GetKey(InputMap.KeyBinds["Right"]))
        {
            float mag = acceleration.magnitude;
            float angle = 45 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Backward"]) && Input.GetKey(InputMap.KeyBinds["Left"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = -135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Backward"]) && Input.GetKey(InputMap.KeyBinds["Left"]))
        {
            float mag = acceleration.magnitude;
            float angle = -135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Backward"]) && Input.GetKey(InputMap.KeyBinds["Right"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = 135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Backward"]) && Input.GetKey(InputMap.KeyBinds["Right"]))
        {
            float mag = acceleration.magnitude;
            float angle = 135 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }


        else if (Input.GetKey(InputMap.KeyBinds["Forward"]) && velocity.magnitude < Speed)
        {

            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Forward"]))
        {

            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Right"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = 90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
            

        }
        else if (Input.GetKey(InputMap.KeyBinds["Right"]))
        {
            float mag = acceleration.magnitude;
            float angle = 90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
            


        }
        else if (Input.GetKey(InputMap.KeyBinds["Backward"]) && velocity.magnitude < Speed)
        {
            acceleration = -acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Backward"]))
        {
            acceleration = -acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
        }
        else if (Input.GetKey(InputMap.KeyBinds["Left"]) && velocity.magnitude < Speed)
        {
            float mag = acceleration.magnitude;
            float angle = -90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity = acceleration.normalized * velocity.magnitude;
            velocity += acceleration;
            if (velocity.magnitude > Speed)
                velocity = velocity.normalized * Speed;
           

        }
        else if (Input.GetKey(InputMap.KeyBinds["Left"]))
        {
            float mag = acceleration.magnitude;
            float angle = -90 * Mathf.Deg2Rad;
            acceleration = new Quaternion(0, Mathf.Sin(angle / 2f), 0, Mathf.Cos(angle / 2f)) * acceleration;
            velocity += acceleration;
            velocity = velocity.normalized * Speed;
            
        }
        else
        {
            Vector3 v = new Vector3(velocity.x, 0, velocity.z);
            velocity += -velocity * ((v.magnitude * 25f) / WalkSpeed) * Time.deltaTime;
        }

        position += velocity * Time.deltaTime;
        //anim.SetFloat("velocity", velocity.magnitude);
        this.transform.position = position;
        Quaternion q = this.transform.rotation;

        this.transform.LookAt(this.transform.position + velocity);
        this.transform.rotation = Quaternion.Slerp(q, this.transform.rotation, .2f);

        if (Input.GetKeyDown(InputMap.KeyBinds["Attack"]))
            Attack();
    }

    void Attack()
    {
        //anim.SetTrigger("attack");

    }

    public void TryToHit()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(this.transform.position + new Vector3(0, 1, 0), this.transform.forward), out hit, 10);

        if (hit.transform == null)
            return;
        //EnemyController ec = hit.transform.gameObject.GetComponent<EnemyController>();
        //if (ec != null)
        //{

        //    player.DoDamage(ec.enemy);
        //    ec.GetHit();
        //}
    }


    public void TakeDamage(float damage)
    {
        stats.GetStat("PlayerHealth").Value -= damage;
    }

    public void DoDamage(IDamageable defender)
    {
        throw new System.NotImplementedException();
    }
}
//+ (this.transform.localScale.y / 2f - 1f) + position.y