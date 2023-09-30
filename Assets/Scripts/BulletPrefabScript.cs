using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPrefabScript : MonoBehaviour
{
    //public stats
    public Vector3 Scale;
    public float Spd, MaxTravelTime, Dmg, XDir, YDir;

    //private stats
    float timeSinceSpawn;
    bool destroyNextFrame;

    //public componenets
    public Rigidbody2D parent;

    //private componenets
    Rigidbody2D rb;
    Transform t;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();

        t.localScale = Scale;
        timeSinceSpawn = 0;
        destroyNextFrame = false;

        if (parent.velocity.x == 0 && parent.velocity.y == 0)
        {
            rb.velocity = new Vector2(XDir, YDir).normalized * Spd;
        }
        else
        {
            rb.velocity = parent.velocity.normalized * Spd;
        }
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > MaxTravelTime || destroyNextFrame)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Entity" && other.GetComponent<Rigidbody2D>() != this.parent)
        {
            EntityScript e = other.GetComponent<EntityScript>();
            e.Hurt(Dmg);
            destroyNextFrame = true;
        }
    }
}
