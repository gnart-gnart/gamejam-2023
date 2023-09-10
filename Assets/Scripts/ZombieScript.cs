using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : EntityScript
{
    private void Update()
    {
        if (possessed)
        {
            controlUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (possessed)
        {
            controlFixedUpdate();
        }
    }
}
