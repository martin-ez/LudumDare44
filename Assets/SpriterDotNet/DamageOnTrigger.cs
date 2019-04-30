using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    Damager damager;
    private float count = 3;

    // Start is called before the first frame update
    void Start()
    {
        damager = GetComponent<Damager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

        transform.localPosition += Vector3.right * count;
        count *= 1.5f;
    }
}
