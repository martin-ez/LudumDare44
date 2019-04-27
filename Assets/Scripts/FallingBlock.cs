using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class FallingBlock : MonoBehaviour
{
    Damager damager;

    // Start is called before the first frame update
    void Start()
    {
        damager = GetComponent<Damager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health targetHealth = collision.transform.GetComponent<Health>();
        if (targetHealth == null)
        {
            Debug.Log(collision.transform.name);
            targetHealth = GetComponentInParent<Health>();
            if (targetHealth == null)
            {
                Debug.Log(collision.transform.name);
                targetHealth = GetComponentInChildren<Health>();
                if (targetHealth == null)
                {
                    Debug.Log(collision.transform.name);
                    return;
                }
            }
        }
        
        damager.DoDamage(targetHealth);
    }
}
