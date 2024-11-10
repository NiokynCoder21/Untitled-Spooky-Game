using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected float energyCost; // Energy cost unique to each ability

    public Ability(float energyCost)
    {
        this.energyCost = energyCost;
    }

    public virtual void Activate(GameObject player)
    {
        player.GetComponent<PlayerEnegy>().LossEnergy(energyCost);
    }

    public virtual void Disable(GameObject player)
    {
        //
    }
}
