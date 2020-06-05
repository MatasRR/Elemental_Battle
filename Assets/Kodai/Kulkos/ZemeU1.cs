using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZemeU1 : Kulka
{
    public override void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (c.GetComponent<Judejimas>().AntZemes())
            {
                base.OnTriggerEnter(c);
            }
        }
        else
        {
            base.OnTriggerEnter(c);
        }
    }
}
