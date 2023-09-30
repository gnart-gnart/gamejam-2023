using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Body;

    Vector2 direction;
    Rigidbody2D rb;
    EntityScript es;
    EntityAIScript ai;

    void Start()
    {
        rb = Body.GetComponent<Rigidbody2D>();
        es = Body.GetComponent<EntityScript>();
        ai = Body.GetComponent<EntityAIScript>();

        ai.TryPossess();
        es.PlaceFirepoint(1, 0);
    }

    void Update()
    {
        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(move_x, move_y).normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            es.Attack();
        }

        if (move_x != 0 || move_y != 0)
        {
            es.PlaceFirepoint(move_x, move_y);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * es.Spd, direction.y * es.Spd);
    }
}
