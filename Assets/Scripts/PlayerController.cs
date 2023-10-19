using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject Body;

    Vector2 direction, mouseDir;
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

        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        mouseDir = (worldMousePosition - rb.position).normalized;

        es.PlaceFirepoint(mouseDir.x, mouseDir.y);

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            es.Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * es.Spd, direction.y * es.Spd);
    }
}
