using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;

public class Cursor : MonoBehaviour
{
    //==== FIELDS ====
    [SerializeField] GameObject cursorPrefab;
    private GameObject cursor;
    private Vector2 mousePos = Vector2.zero;

    private Collisions collisions;
    [SerializeField] GameObject manager;

    //==== START ====
    void Start()
    {
        cursor = Instantiate(cursorPrefab);
        cursor.transform.position = mousePos;

        collisions = new Collisions();
    }

    //==== UPDATE ====
    void Update()
    {
        //Get Mouse Position & Update Cursor Transform
        mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //Gravitate prefab towards any overlapping objects
        bool mouseColliding = false;
        foreach (GameObject a in manager.GetComponent<AsteroidManager>().Asteroids)
        {
            if (collisions.CheckMouseOverlap(mousePos, a))
            {
                cursor.transform.position = Vector3.MoveTowards(cursor.transform.position, a.transform.position, 50f * Time.deltaTime);
                mouseColliding = true;
                Debug.Log(a.GetComponent<Asteroid>().Ore);
            }
        }
        if (!mouseColliding) //If the mouse isn't over an interactable, return cursor back to mousePos
        {
            cursor.transform.position = Vector3.MoveTowards(cursor.transform.position, mousePos, 50f * Time.deltaTime);
        }
    }
}
