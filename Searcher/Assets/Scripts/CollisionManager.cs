using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour
{
    //==== FIELDS ====
    private Collisions collisions;
    private List<GameObject> asteroids;
    private GameObject player;

    
    //==== START ====
    void Start()
    {
        //Initiate fields
        collisions = new Collisions();
        asteroids = transform.GetComponent<AsteroidManager>().Asteroids;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //==== UPDATE ====
    void Update()
    {
        
    }
}
