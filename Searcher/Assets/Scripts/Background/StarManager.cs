using UnityEngine;
using System.Collections.Generic;

public class StarManager : MonoBehaviour
{
    //==== FIELDS ====
    private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private GameObject star;
    private int numStars = 100;
    private const float min = -20;
    private const float max = 20;

    //Camera
    private Camera cam;
    private float camHeight;
    private float camWidth;

    //==== START ====
    void Start()
    {
        //Set Camera
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2.0f;
        camWidth = cam.orthographicSize * cam.aspect;

        //Spawn Stars
        for (int i = 0; i < numStars; i++)
        {
            Vector3 starPos = new Vector3(Random.Range(min, max), Random.Range(min, max));
            stars.Add(Instantiate(star, starPos, Quaternion.identity, transform));
        }
    }

    //==== UPDATE ====
    void Update()
    {
        foreach (GameObject s in stars)
        {
            //Get position of star relative to camera
            Vector3 starInViewport = cam.WorldToViewportPoint(s.transform.position);
            
            //If a star is inside the camera, set it to active
            if (starInViewport.x >= 0 && starInViewport.x <= 1 && starInViewport.y >= 0 && starInViewport.y <= 1) s.SetActive(true);
            else s.SetActive(false); //Else set it to inactive
        }
    }
}

/* NOTE FOR FUTURE SAM:
 * You'll probably have to eventually store Star positions within each Sector object to make sure Stars appear in the same place in every Sector every time
 */