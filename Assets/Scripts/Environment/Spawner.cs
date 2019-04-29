using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] sprites;
    public int noInstances;
    public float minParallax = 0;
    public float maxParallax = 0;
    public float minScale = 1;
    public float maxScale = 1;

    private Transform cam;
    private GameObject[] instances;
    private float tolerance = 5;
    private float screenEdge;

    void Start()
    {
        cam = Camera.main.transform;
        screenEdge = Mathf.Abs(transform.localPosition.x);
        InitSpawn();
    }

    private void Update()
    {
        for (int i = 0; i < instances.Length; i++)
        {
            if (instances[i].transform.position.x < transform.position.x - tolerance)
            {
                Destroy(instances[i]);
                GameObject newSpawn = Instantiate(sprites[Random.Range(0, sprites.Length)]);
                newSpawn.transform.SetParent(cam);
                newSpawn.transform.localPosition = new Vector3(tolerance + screenEdge + Random.Range(0, screenEdge), 0, 0);
                if (maxParallax > 0) newSpawn.GetComponent<Parallax>().parallaxEffect = Random.Range(minParallax, maxParallax);
                newSpawn.transform.GetChild(0).localScale = Vector3.one * Random.Range(1f, maxScale);
                instances[i] = newSpawn;
            }
        }
    }

    private void InitSpawn()
    {
        instances = new GameObject[noInstances];
        for (int i = 0; i < instances.Length; i++)
        {
            GameObject newSpawn = Instantiate(sprites[Random.Range(0, sprites.Length)]);
            newSpawn.transform.SetParent(cam);
            newSpawn.transform.localPosition = new Vector3(Random.Range(-screenEdge, screenEdge * 2), 0, 0);
            if (maxParallax > 0) newSpawn.GetComponent<Parallax>().parallaxEffect = Random.Range(minParallax, maxParallax);
            newSpawn.transform.GetChild(0).localScale = Vector3.one * Random.Range(1f, maxScale);
            instances[i] = newSpawn;
        }
    }
}
