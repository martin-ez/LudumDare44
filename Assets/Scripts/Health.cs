using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    public bool canBeDamaged = true;
    private int lastDamageTaken;

    public float physicalResistance = 1.0f;
    public int physicalIgnore;
    public float fireResistance = 1.0f;
    public int fireIgnore;
    

    public int Damage(int damage, DamageType[] damageTypes)
    {
        float bestResistance = 1.0f;
        int bestIgnore = 0;
        for (int i = 0; i < damageTypes.Length; i++)
        {
            switch (damageTypes[i])
            {
                case DamageType.Physical:
                    bestResistance = bestResistance < physicalResistance ? bestResistance : physicalResistance;
                    bestIgnore = bestIgnore > physicalIgnore ? bestIgnore : physicalIgnore;
                    break;
                case DamageType.Fire:
                    bestResistance = bestResistance < fireResistance ? bestResistance : fireResistance;
                    bestIgnore = bestIgnore > fireIgnore ? bestIgnore : fireIgnore;
                    break;
            }
        }

        damage -= bestIgnore;
        damage = (int)Mathf.Round(damage * bestResistance);

        health -= damage;
        lastDamageTaken = damage;
        if (health <= 0)
        {
            Die();
        }

        return lastDamageTaken;
    }
    
    public bool Die()
    {
        gameObject.SetActive(false);
        return false;
    }
}
