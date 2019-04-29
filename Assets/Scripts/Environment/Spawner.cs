using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] sprites;
    public Transform environment;
    public int noInstances;
    public float minParallax = 0;
    public float maxParallax = 0;
    public bool randomFlip = false;
    public float yJitter = 0f;

    private Camera cam;
    private GameObject[] instances;
    private float tolerance = 5;
    private float screenEdge;

    void Start()
    {
        cam = Camera.main;
        screenEdge = Mathf.Abs(cam.transform.position.x);
        InitSpawn();
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2((cam.transform.position.x - cam.orthographicSize * 0.5f) - tolerance, 0f), new Vector2(.1f, .1f));
    }
    */
    private void Update()
    {
        screenEdge = Mathf.Abs(cam.transform.position.x + cam.orthographicSize * Screen.width / Screen.height);
        for (int i = 0; i < instances.Length; i++)
        {
            Debug.Log(instances[i].transform.position.x);
            if (instances[i].transform.position.x < (cam.transform.position.x - cam.orthographicSize * Screen.width / Screen.height) - tolerance)
            {
                Destroy(instances[i]);
                GameObject newSpawn = Instantiate(sprites[Random.Range(0, sprites.Length)]);
                newSpawn.transform.SetParent(environment);
                newSpawn.transform.localPosition = new Vector3(tolerance + screenEdge + Random.Range(0, screenEdge), Random.Range(0, yJitter), 0);
                if (maxParallax > 0) newSpawn.GetComponent<Parallax>().parallaxEffect = Random.Range(minParallax, maxParallax);
                if (randomFlip) newSpawn.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = (Random.value < 0.5);
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
            newSpawn.transform.SetParent(environment);
            newSpawn.transform.localPosition = new Vector3(Random.Range(-screenEdge, screenEdge * 4), Random.Range(0, yJitter), 0);
            if (maxParallax > 0) newSpawn.GetComponent<Parallax>().parallaxEffect = Random.Range(minParallax, maxParallax);
            instances[i] = newSpawn;
        }
    }
}
