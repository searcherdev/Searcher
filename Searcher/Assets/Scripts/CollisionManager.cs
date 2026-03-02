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
        bool playerColliding = false;
        
        //For each asteroid...
        foreach(GameObject a in asteroids)
        {
            a.GetComponent<SpriteRenderer>().color = Color.white;   

            //Check Player/Asteroid Collisions
            if (collisions.CheckSpriteCollision(player, a))
            {
                //Turn red to indicate a collision (TEST CODE DUH)
                player.GetComponent<SpriteRenderer>().color = Color.red;
                a.GetComponent<SpriteRenderer>().color = Color.red;
                playerColliding = true;
            }

            //Check Asteroid/Asteroid Collisions
            foreach (GameObject b in asteroids)
            {
                if (a == b) continue;
                if (collisions.CheckSpriteCollision(a, b))
                {
                    a.GetComponent<SpriteRenderer>().color = Color.green;
                    a.GetComponent<Asteroid>().IsColliding = true;

                    b.GetComponent<SpriteRenderer>().color = Color.green;
                    a.GetComponent<Asteroid>().IsColliding = true;
                }
            }
        }

        //Reset coloration if no collisions happening
        if (!playerColliding)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
