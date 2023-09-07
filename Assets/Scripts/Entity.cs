using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    Transform t;
    public Player p;

    void Start()
    {
        t = GetComponent<Transform>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Clicked!");
        p.host = GetComponent<Entity>();
    }
}
