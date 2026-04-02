using UnityEngine;

public class N0MAD : MonoBehaviour
{
    //NOTE: Though the character's name is N0M-AD, the dash in the name caused problems in the code, so to prevent that the class name removes the dash

    //==== FIELDS ====
    private float energy;
    private float maxEnergy;

    private float hull;
    private float maxHull;

    private Engine engine;

    private float timer;

    //==== PROPERTIES ====
    public float Energy { get { return energy; } set { energy = value; } }
    public float MaxEnergy { get { return maxEnergy; } set { maxEnergy = value; } }   
    public float Hull { get { return hull; } set { hull = value; } }
    public float MaxHull { get { return maxHull; } set { hull = value; } }
    public Engine Engine { get { return engine; } set { engine = value; } }
    
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
    }

    //==== UPDATE ====
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f) //If a second passes, activate consciousness energy decrease and decrement timer
        {
            Consciousness();
            timer -= 1.0f;
        }
    }

    //==== METHODS ====
    void Consciousness() //Constant Energy drain representing N0M-AD's brain at work
    {
        energy -= 1;
    }
}
