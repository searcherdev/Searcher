using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Harvester : Equipment
{
    //==== FIELDS ====
    private Collisions collisions;

    private GameObject cursorPrefab;
    private GameObject cursorInstance;

    LineRenderer line;

    //==== PROPERTIES ====
    public GameObject CursorInstance { get { return cursorInstance; } }
    
    //==== START ====
    void Start()
    {
        cursorPrefab = manager.GetComponent<Cursor>().CursorPrefab;
        cursorInstance = null;

        collisions = new Collisions();
        line = GetComponent<LineRenderer>();
    }

    //==== UPDATE ====
    void Update()
    {
        if (active && target != null)
        {
            //If the Harvester should be Harvesting, then Harvest
            if ((target.GetComponent<Asteroid>() && Vector3.Distance(this.transform.position, target.transform.position) <= range) || (target.GetComponent<Nebula>() && collisions.CheckSpriteCollision(n0mad.gameObject, target)))
            {
                HarvesterBeam(); //Render UI Harvester Beam
                HarvesterCursor(); //Render UI Harvester Targeting Cursor

                //Harvester Update Timer
                timer += Time.deltaTime;
                if (timer >= rate) //If the timer goes past the rate, harvest and decrement timer
                {
                    Harvest();
                    timer -= rate;
                }

                n0mad.Energy -= (1 / rate) * 10 * Time.deltaTime; //Decrement energy based on usage
            }
            else //Otherwise, turn off the Harvester
            {
                SetInactive();
            }
        }
    }

    //==== METHODS ====
    public void Harvest() //Decrement a Resource from the target and add it to N0M-AD's Cargo Hold
    {
        MonoBehaviour targetClass = target.GetComponent<MonoBehaviour>();
        switch (targetClass)
        {
            case Asteroid a: //Take an Ore or Gas from an Asteroid
                int randNum = Random.Range(1, 3);
                if (randNum == 1 && a.Gas > 0) //Take a Gas if there's a Gas to take and that's what was rolled
                {
                    a.Gas--;
                    if (n0mad.Cargo.Hold.ContainsKey("Gas")) { n0mad.Cargo.Hold["Gas"]++; }
                    else { n0mad.Cargo.Hold.Add("Gas", 1); }
                    Debug.Log("Took 1 Gas from Asteroid. N0M-AD Gas: " + n0mad.Cargo.Hold["Gas"] + ". Asteroid Gas: " + a.Gas);
                }
                else //Take an Ore otherwise
                {
                    a.Ore--;
                    if (n0mad.Cargo.Hold.ContainsKey("Ore")) { n0mad.Cargo.Hold["Ore"]++; }
                    else { n0mad.Cargo.Hold.Add("Ore", 1); }
                    Debug.Log("Took 1 Ore from Asteroid. N0M-AD Ore: " + n0mad.Cargo.Hold["Ore"] + ". Asteroid Ore: " + a.Ore);
                }
                if (a.Ore == 0 && a.Gas == 0) { SetInactive(); manager.GetComponent<AsteroidManager>().Asteroids.Remove(a.gameObject); Destroy(a.gameObject); }
                break;

            case Nebula n: //Take Gas from a Nebula
                n.Gas--;
                if (n0mad.Cargo.Hold.ContainsKey("Gas")) { n0mad.Cargo.Hold["Gas"]++; }
                else { n0mad.Cargo.Hold.Add("Gas", 1); }
                Debug.Log("Took 1 Gas from Nebula. N0M-AD Gas: " + n0mad.Cargo.Hold["Gas"] + ". Nebula Gas: " + n.Gas);
                if (n.Gas == 0) { SetInactive(); manager.GetComponent<NebulaManager>().Nebulae.Remove(n.gameObject); Destroy(n.gameObject); }
                break;

            default:
                Debug.Log("RESOURCE COLLECTION FAILED");
                break;
            
        }
    }
    public void HarvesterBeam() //Render a Harvester Beam between N0M-AD and the target Asteroid
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.material = new Material(Shader.Find("Sprites/Default"));

        line.startColor = Color.blue;
        line.endColor = Color.white;

        line.startWidth = 0.025f;
        line.endWidth = 0.15f;

        line.SetPosition(0, n0mad.transform.position);
        line.SetPosition(1, target.transform.position);
    }

    public void HarvesterCursor()
    {
        if (cursorInstance != null)
        {
            cursorInstance.transform.position = target.transform.position;
            if (active && n0mad.ActiveSlot == this) cursorInstance.GetComponent<SpriteRenderer>().color = Color.green;
            else if (active && n0mad.ActiveSlot != this) cursorInstance.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }

    public void SetActive(GameObject target) 
    { 
        active = true; 
        this.target = target; 
        timer = 0f;

        if (cursorInstance != null) Destroy(cursorInstance);
        cursorInstance = Instantiate(cursorPrefab);
        cursorInstance.GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    public void SetInactive() 
    {
        Destroy(cursorInstance);
        cursorInstance = null;

        active = false; 
        target = null; 
        line.enabled = false;
        timer = 0f;
    }
}
