using System.Collections.Generic;
using UnityEngine;

public class NebulaManager : MonoBehaviour
{
    //==== FIELDS ====
    private List<GameObject> nebulae = new List<GameObject>();
    [SerializeField] private GameObject nebula;
    private int numNebulae = 5;
    private const float min = -30;
    private const float max = 30;

    //Camera
    private Camera cam;
    private float camHeight;
    private float camWidth;

    private Collisions collisions;

    //==== PROPERTIES ====
    public List<GameObject> Nebulae { get { return nebulae; } }

    //==== START ====
    void Start()
    {
        //Set Collisions
        collisions = new Collisions();

        //Set Camera
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2.0f;
        camWidth = cam.orthographicSize * cam.aspect;

        //Spawn Nebulae
        bool canSpawn = true; //Create canSpawn variable
        for (int i = 0; i < numNebulae; i++)
        {
            canSpawn = true; //Reset canSpawn bool at top of every loop
            Vector3 nPos = new Vector3(Random.Range(min, max), Random.Range(min, max)); //Set potential position

            /*//Determine if nebula is spawning over N0M-AD
            GameObject n0mad = GameObject.FindGameObjectWithTag("Player");
            float n0madIncrementX = n0mad.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            float n0madIncrementY = n0mad.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            if (astPos.x > n0mad.transform.position.x - n0madIncrementX && astPos.x < n0mad.transform.position.x + n0madIncrementX && astPos.y > n0mad.transform.position.y - n0madIncrementY && astPos.y < n0mad.transform.position.y + n0madIncrementY)
            {
                canSpawn = false;
            }*/

            foreach (GameObject n in nebulae) //Loop through each nebula already spawned
            {
                float increment = n.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2; //Get increment from center needed to check collisions

                //If the potential position is within a nebula, mark canSpawn as false
                if (nPos.x > n.transform.position.x - increment && nPos.x < n.transform.position.x + increment && nPos.y > n.transform.position.y - increment && nPos.y < n.transform.position.y + increment)
                {
                    canSpawn = false;
                }
            }
            if (canSpawn) { nebulae.Add(Instantiate(nebula, nPos, Quaternion.identity, transform)); } //If a nebula can spawn, spawn it & add to the nebulae list
            else { i -= 1; } //If it can't, redo this nebula spawn process
        }
    }

    //==== UPDATE ====
    void Update()
    {

    }
}
