using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Food
{
    public Rabbit() : base(energy: 50f) { } //this sets the energy for this item

    public override void ApplyEffect(GameObject player) //gets the base function from the food script
    {
        base.ApplyEffect(player);
    }
}
