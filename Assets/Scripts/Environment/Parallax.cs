using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect;
    public bool tiling = true;

    private Transform cam;

    private float length, startPos;

    void Start() {
        startPos = transform.position.x;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr) length = sr.sprite.bounds.size.x;
        else length = 33;
        cam = Camera.main.transform;
    }

    private void FixedUpdate() {
        float dist = (cam.position.x * parallaxEffect);
        float inverse = cam.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (inverse > startPos + length && tiling) startPos += length;
        else if (inverse < startPos - length && tiling) startPos -= length;
    }
}
