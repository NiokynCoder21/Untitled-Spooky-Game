using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Food
{
    public Rat() : base(energy: 25f) { } //this sets the energy for this item

    public override void ApplyEffect(GameObject player) //gets the base function from the food script
    {
        base.ApplyEffect(player);
    }
}
