using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    protected Transform t;
    protected Rigidbody2D rb;
    public PlayerCameraScript pc; //player camera

    //player movement stats
    public float dynamicFriction = 0.96f;
    public float staticFriction = 0.90f;
    public float mSpeed = 0.35f;

    //player movement variables
    protected float x_dir, y_dir, speed;
    protected Vector2 dir;
    protected bool moving;

    //the all-important boolean
    public bool possessed;
    protected bool possessable;

    protected void Start()
    {
        possessable = true;
        moving = false;
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        pc = FindAnyObjectByType<PlayerCameraScript>();
    }

    private void Update()
    {
        if (possessed)
        {
            controlUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (possessed)
        {
            controlFixedUpdate();
        }
    }

    protected void controlUpdate()
    {
        x_dir = 0;
        y_dir = 0;

        if (Input.GetKey(KeyCode.W))
        {
            y_dir += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x_dir += -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y_dir += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x_dir += 1;
        }
    }

    protected void controlFixedUpdate()
    {
        if (x_dir == 0 && y_dir == 0)
        { moving = false; }
        else { moving = true; }

        if (moving)
        {
            speed += mSpeed;
            speed *= dynamicFriction;
        }
        else
        {
            speed *= staticFriction;
        }

        dir = new Vector2(x_dir, y_dir);
        dir.Normalize();
        rb.velocity = speed * dir;
    }

    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (possessed && coll.gameObject.GetComponent<EntityScript>().possessable)
        {
            possessed = false;
            coll.gameObject.GetComponent<EntityScript>().possessed = true;
            possessable = false;

            pc.e = coll.gameObject.GetComponent<EntityScript>();

            Debug.Log("Possessed!");
        }
    }
}
