using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Movement Data for when possessed
    public float dynamicFriction = 0.96f;
    public float staticFriction = 0.90f;
    public float mSpeed = 0.35f;

    float x_dir, y_dir, speed;
    Vector2 dir;
    bool moving;

    public Entity host;

    void Start()
    {
        host = GameObject.Find("Entity").GetComponent<Entity>();
    }

    void Update()
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

    private void FixedUpdate()
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
        host.GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
}
