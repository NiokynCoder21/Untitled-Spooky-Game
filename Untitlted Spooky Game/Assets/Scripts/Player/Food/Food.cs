using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    protected float energyCost; // Energy cost unique to each ability

    // The method that will be called when the player collects this item

    public Food(float energy)
    {
        this.energyCost = energy;
    }

    public virtual void ApplyEffect(GameObject player)
    {
        player.GetComponent<PlayerEnegy>().GainEnergy(energyCost);
    }

}
