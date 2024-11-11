using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAbility : Ability
{
    public GhostAbility() : base(energyCost: 0.7f) { }

    public override void Activate(GameObject player)
    {
        base.Activate(player);
        player.GetComponent<MeshRenderer>().enabled = false;

        Physics.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("Wall"), true);
    }

    public override void Disable(GameObject player)
    {
        base.Disable(player);
        player.GetComponent<MeshRenderer>().enabled = true;

        Physics.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("Wall"), false);
    }
}
