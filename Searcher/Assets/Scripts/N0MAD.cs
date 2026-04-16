using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class N0MAD : MonoBehaviour
{
    //NOTE: Though the character's name is N0M-AD, the dash in the name caused problems in the code, so to prevent that the class name removes the dash

    //==== FIELDS ====
    private float energy;
    private float maxEnergy;

    private float hull;
    private float maxHull;
    private float hullResist;

    private Engine engine;
    private Cargo cargo;
    private Harvester harvester;

    private float timer;

    private GameObject target;
    private GameObject manager;

    //==== PROPERTIES ====
    public float Energy { get { return energy; } set { energy = value; } }
    public float MaxEnergy { get { return maxEnergy; } set { maxEnergy = value; } }   
    public float Hull { get { return hull; } set { hull = value; } }
    public float MaxHull { get { return maxHull; } set { hull = value; } }
    public float HullResist { get { return hullResist; } set { hullResist = value; } }
    public Engine Engine { get { return engine; } set { engine = value; } }
    public Cargo Cargo { get { return cargo; } set { cargo = value; } }
    public Harvester Harvester { get { return harvester; } set { harvester = value; } }
    public GameObject Target { get { return target; } set { target = value; } }
    
    //==== START ====
    void Start()
    {
        //Set timer to 0
        timer = 0f;

        //Set Manager
        manager = GameObject.FindGameObjectWithTag("Manager");

        //Set Energy & MaxEnergy
        maxEnergy = 10000;
        energy = maxEnergy;

        //Set Engine stats
        engine = gameObject.AddComponent<Engine>();
        engine.Accel = 1f;
        engine.MaxSpeed = 5f;
        engine.RotSpeed = 50f;

        //Set Hull
        maxHull = 100;
        hull = maxHull;
        hullResist = 0.01f;

        //Set Cargo
        cargo.Hold = new Dictionary<string, int>(); //Key = name, Value = number (of item)
        cargo.MaxCargo = 20;
        cargo.BaseCargoValues();

        //Set Harvester
        harvester.Rate = 5f; //5 seconds per item harvested from target
    }

    //==== UPDATE ====
    void Update()
    {
        target = manager.GetComponent<Cursor>().Target;
        
        //Consciousness Update Timer
        timer += Time.deltaTime;
        if (timer >= 1.0f) //If a second passes, activate consciousness energy decrease and decrement timer
        {
            Consciousness();
            timer -= 1.0f;
        }

        //Engine Energy Reduction (only when Engine is in use)
        if (engine.IsInUse) energy -= engine.Accel * Time.deltaTime;
    }

    //==== METHODS ====
    void Consciousness() //Constant Energy drain representing N0M-AD's brain at work
    {
        energy -= 1;
    }

    //When N0M-AD collides with an outside collider for the first time
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Reduce Hull by an amount relative to the collision velocity magnitude and collider mass
        hull -= (collision.relativeVelocity.magnitude + ((collision.gameObject.GetComponent<Rigidbody2D>().mass / 200) * collision.relativeVelocity.magnitude)) * (1 - hullResist);
        if (hull <= 0) SceneManager.LoadScene("Title Screen");
    }
}
