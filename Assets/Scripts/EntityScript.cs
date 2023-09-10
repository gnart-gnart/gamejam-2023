using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityScript : MonoBehaviour
{
    // Movement Data for when possessed
    public float dynamicFriction = 0.96f;
    public float staticFriction = 0.90f;
    public float mSpeed = 0.35f;

    public PlayerCameraScript pcs;

    float x_dir, y_dir, speed;
    Vector2 dir;
    bool moving;
    public bool infected;
    bool infectable;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pcs = GameObject.Find("Player Camera").GetComponent<PlayerCameraScript>();
        infectable = true;
    }

    void Update()
    {
        if (infected) { PUpdate(); }
    }

    private void FixedUpdate()
    {
        if (infected) { PFixedUpdate(); }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (infected)
        {
            GameObject go = c.gameObject;
            if (go.tag == "Entity" && go.GetComponent<EntityScript>().infectable)
            {
                Debug.Log("Collided!");
                go.GetComponent<EntityScript>().infected = true;
                pcs.target = go;
                infectable = false;
                infected = false;
            }
        }
    }

    void PUpdate()
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

    void PFixedUpdate()
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
}