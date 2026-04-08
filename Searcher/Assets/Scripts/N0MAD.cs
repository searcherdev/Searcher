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

    private float timer;

    private Dictionary<string, int> cargo = new Dictionary<string, int>(); //Key = name, Value = number (of item)
    private float maxCargo;

    //==== PROPERTIES ====
    public float Energy { get { return energy; } set { energy = value; } }
    public float MaxEnergy { get { return maxEnergy; } set { maxEnergy = value; } }   
    public float Hull { get { return hull; } set { hull = value; } }
    public float MaxHull { get { return maxHull; } set { hull = value; } }
    public float HullResist { get { return hullResist; } set { hullResist = value; } }
    public Engine Engine { get { return engine; } set { engine = value; } }
    public Dictionary<string, int> Cargo { get { return cargo; } set { cargo = value; } }
    public float MaxCargo { get {return maxCargo; } set {maxCargo = value; } }
    
    //==== START ====
    void Start()
    {
        //Set timer to 0
        timer = 0f;

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
        maxCargo = 20;
        BaseCargoValues();
    }

    //==== UPDATE ====
    void Update()
    {
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
    void BaseCargoValues()
    {
        cargo.Add("Ore", 0);
        cargo.Add("Gas", 0);
        cargo.Add("Metals", 0);
        cargo.Add("Fuel", 0);
        cargo.Add("Circuits", 0);
        cargo.Add("Heavy Metals", 0);
        cargo.Add("Jump Fuel", 0);
        cargo.Add("Computer Circuits", 0);
    }

    //When N0M-AD collides with an outside collider for the first time
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Reduce Hull by an amount relative to the collision velocity magnitude and collider mass
        hull -= (collision.relativeVelocity.magnitude + ((collision.gameObject.GetComponent<Rigidbody2D>().mass / 200) * collision.relativeVelocity.magnitude)) * (1 - hullResist);
        if (hull <= 0) SceneManager.LoadScene("Title Screen");
    }
}
