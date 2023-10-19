using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAIScript : MonoBehaviour
{
    [SerializeField] private float minFollowDistance, maxFollowDistance, maxAttackDistance;
    [SerializeField] public bool possessed, possessable;
    [SerializeField] private Transform firePoint;

    bool playerInSight, playerInRange;

    EntityScript es;
    GameObject player;

    float distance;

    void Start()
    {
        es = GetComponent<EntityScript>();
        player = (GameObject.FindWithTag("Player")).GetComponent<PlayerController>().Body;
        playerInSight = playerInRange = false;
    }

    void Update()
    {
        if (!possessed)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (CanSeePlayer(distance))
            {
                if (distance > minFollowDistance && distance < maxFollowDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, es.Spd * Time.deltaTime);
                }
                if (distance < maxAttackDistance)
                {
                    Vector2 dir = player.transform.position - transform.position;
                    dir.Normalize();
                    es.PlaceFirepoint(dir.x, dir.y);
                    es.Attack();
                }
            }
        }
    }
    
    // FIXME !!!!!!!!!!!!!!!!!
    bool CanSeePlayer(float distance)
    {
        float castDistance = distance;

        Vector2 endPos = firePoint.position + Vector3.right * distance;

        RaycastHit2D hit = Physics2D.Linecast(firePoint.position, endPos);
        if (hit.collider != null)
        {
            GameObject go = hit.collider.gameObject;
            if (go.GetComponent<EntityAIScript>() != null)
            {
                if (go.GetComponent<EntityAIScript>().possessed)
                {
                    Debug.Log("Can see player!");
                    return true;
                }
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                return false;
            }
        }

        return false;
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
