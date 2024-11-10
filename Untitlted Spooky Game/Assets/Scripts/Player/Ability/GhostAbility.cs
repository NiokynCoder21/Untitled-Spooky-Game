using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAbility : Ability
{
    public GhostAbility() : base(energyCost: 15f) { }

    public override void Activate(GameObject player)
    {
        base.Activate(player);
        player.GetComponent<Collider>().enabled = false; // Example: Disable player collider to make them pass through objects
        player.GetComponent<MeshRenderer>().enabled = false;
    }

    public override void Disable(GameObject player)
    {
        base.Disable(player);
        player.GetComponent<Collider>().enabled = true; // Example: Disable player collider to make them pass through objects
        player.GetComponent<MeshRenderer>().enabled = true;
    }
}
