using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Food
{
    public Rabbit() : base(energy: 25f) { }

    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);
        print("rabbit eaten");
    }
}
