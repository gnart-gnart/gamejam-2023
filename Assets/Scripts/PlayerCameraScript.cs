using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{

    public PlayerController player;
    Transform target, cam;
    void Start()
    {
        target = player.Body.GetComponent<Transform>();
        cam = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cam.position = Vector3.MoveTowards(cam.position, target.position, 1.0f);
        cam.position = new Vector3(cam.position.x, cam.position.y, -10f);
    }
}
