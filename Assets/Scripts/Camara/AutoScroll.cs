using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + (speed * 0.1f), transform.position.y, transform.position.z);
    }
}
