using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public EntityScript e; // object to be followed
    public float Speed, MaxSpeed, SmoothTime;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (e != null)
        {
            Transform t = e.gameObject.GetComponent<Transform>();
            targetPos = new Vector3(t.position.x, t.position.y, transform.position.z);
            Vector3 velocity = (targetPos - transform.position) * Speed;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, SmoothTime, MaxSpeed, Time.deltaTime);
        }
    }
}
