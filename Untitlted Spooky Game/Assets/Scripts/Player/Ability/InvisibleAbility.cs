using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleAbility : Ability //make the player not visible
{
    public InvisibleAbility() : base(energyCost: 0.5f) { } // Set specific cost for invisibility

    public override void Activate(GameObject player)
    {
        base.Activate(player);
    }

    public override void Disable(GameObject player)
    {
        base.Disable(player);
    }
}
