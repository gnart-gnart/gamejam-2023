using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Player p; // object to be followed
    public float Speed, MaxSpeed, SmoothTime;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (p != null)
        {
            Transform t = p.host.GetComponent<Transform>();
            targetPos = new Vector3(t.position.x, t.position.y, transform.position.z);
            Vector3 velocity = (targetPos - transform.position) * Speed;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, SmoothTime, MaxSpeed, Time.deltaTime);
        }
    }
}
