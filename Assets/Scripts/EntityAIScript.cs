using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAIScript : MonoBehaviour
{
    public float AIMinFollowDistance, AIMaxFollowDistance;
    public bool possessed, possessable;

    EntityScript es;
    GameObject player;

    float distance;

    void Start()
    {
        es = GetComponent<EntityScript>();
        player = (GameObject.FindWithTag("Player")).GetComponent<PlayerController>().Body;
    }

    void Update()
    {
        if (!possessed)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance > AIMinFollowDistance && distance < AIMaxFollowDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, es.Spd * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {

    }

    public bool TryPossess()
    {
        if (possessable)
        {
            possessed = true;
            return true;
        }
        return false;
    }
}
