using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAbility : Ability
{
    public GhostAbility() : base(energyCost: 0.7f) { } // Set specific cost for ghost

    public override void Activate(GameObject player)
    {
        base.Activate(player);
        player.GetComponent<MeshRenderer>().enabled = false; //disables mesh renderer

        Physics.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("Wall"), true); //this means that object on this layer do not have collsion detection
    }

    public override void Disable(GameObject player)
    {
        base.Disable(player);
        player.GetComponent<MeshRenderer>().enabled = true; //enables mesh renderer

        Physics.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("Wall"), false); //this means that object on this layer do have collsion detection
    }
}
