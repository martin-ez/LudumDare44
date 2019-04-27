using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damageDealt;
    public DamageType[] damageTypes = new DamageType[] { DamageType.Physical };

    public int DoDamage(Health target)
    {
        return target.Damage(damageDealt, damageTypes);
    }
}

public enum DamageType
{
    Physical,
    Fire
}
