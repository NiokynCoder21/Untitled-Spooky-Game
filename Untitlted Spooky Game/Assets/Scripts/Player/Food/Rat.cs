using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Food
{
    public Rat() : base(energy: 10f) { }

    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);
        print("rat eaten");
    }
}
