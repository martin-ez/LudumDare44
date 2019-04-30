using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    private int maxHealth = 10;
    public bool canBeDamaged = true;
    private int lastDamageTaken;

    public float physicalResistance = 1.0f;
    public int physicalIgnore;
    public float fireResistance = 1.0f;
    public int fireIgnore;

    public Transform respawnPoint;

    public Image bar;



    public void Start()
    {
        maxHealth = health;
    }

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
        Debug.Log(bar.fillAmount = (float)health / (float)maxHealth);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Respawn();
        }

        return lastDamageTaken;
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    
    public bool Die()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
        return false;
    }
}
