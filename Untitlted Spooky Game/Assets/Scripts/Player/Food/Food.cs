using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    protected float energyCost; // this is the energy amount for each kind of food

    public Food(float energy)
    {
        this.energyCost = energy;
    }

    public virtual void ApplyEffect(GameObject player)
    {
        player.GetComponent<PlayerEnegy>().GainEnergy(energyCost);
    }

}
