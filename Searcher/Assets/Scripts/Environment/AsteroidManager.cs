using UnityEngine;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
    //==== FIELDS ====
    private List<GameObject> asteroids = new List<GameObject>();
    [SerializeField] private GameObject asteroid;
    private int numAsteroids = 10;
    private const float min = -20;
    private const float max = 20;

    //Camera
    private Camera cam;
    private float camHeight;
    private float camWidth;

    //==== PROPERTIES ====
    public List<GameObject> Asteroids { get { return asteroids; } }

    //==== START ====
    void Start()
    {
        //Set Camera
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2.0f;
        camWidth = cam.orthographicSize * cam.aspect;

        //Spawn Stars
        for (int i = 0; i < numAsteroids; i++)
        {
            Vector3 astPos = new Vector3(Random.Range(min, max), Random.Range(min, max));
            asteroids.Add(Instantiate(asteroid, astPos, Quaternion.identity, transform));
        }
    }

    //==== UPDATE ====
    void Update()
    {
        /*foreach (GameObject a in asteroids)
        {
            //Get position of star relative to camera
            Vector3 astInViewport = cam.WorldToViewportPoint(a.transform.position);

            //If a star is inside the camera, set it to active
            if (astInViewport.x >= 0 && astInViewport.x <= 1 && astInViewport.y >= 0 && astInViewport.y <= 1) a.SetActive(true);
            else a.SetActive(false); //Else set it to inactive
        }*/
    }
}
