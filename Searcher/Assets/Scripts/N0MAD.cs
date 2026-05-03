using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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

    private MonoBehaviour activeSlot;
    private List<MonoBehaviour> slotList;
    private MonoBehaviour slot1;
    private MonoBehaviour slot2;

    private bool rmbThisFrame;
    private bool rmbLastFrame;
    private bool lmbThisFrame;
    private bool lmbLastFrame;

    private bool key1ThisFrame;
    private bool key1LastFrame;
    private bool key2ThisFrame;
    private bool key2LastFrame;

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
    public List<MonoBehaviour> SlotList { get { return slotList; } }
    public MonoBehaviour ActiveSlot { get { return activeSlot; } }
    public MonoBehaviour Slot1 { get { return slot1; } set { slot1 = value; } }
    public MonoBehaviour Slot2 { get { return slot2; } set { slot2 = value; } }
    
    //==== START ====
    void Start()
    {
        //Set timer to 0
        timer = 0f;

        //Set Mouse States
        rmbThisFrame = false;
        lmbThisFrame = false;
        rmbLastFrame = false;
        lmbLastFrame = false;

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
        cargo = gameObject.AddComponent<Cargo>();
        cargo.Hold = new Dictionary<string, int>(); //Key = name, Value = number (of item)
        cargo.MaxCargo = 20;

        //Set Harvester
        harvester = gameObject.AddComponent<Harvester>();
        harvester.Rate = 5f; //5 seconds per item harvested from target
        harvester.Range = 5f; //Only Harvest targets 5 units away

        //Set Equipment Slots (NOTE: THIS WILL NOT BE HOW THIS IS FILLED IN IN THE FINAL PRODUCT OBVIOUSLY)
        slotList = new List<MonoBehaviour>();
        slot1 = harvester;
        slot2 = null;
        slotList.Add(slot1);
        slotList.Add(slot2);
        activeSlot = slot1;
    }

    //==== UPDATE ====
    void Update()
    {
        //Get current mouse & keyboard states
        SetThisFrame();

        //Set current active slot based on 
        if (key1ThisFrame && !key1LastFrame) { activeSlot = slot1; Debug.Log("1 Pressed - Slot 1 Active"); }
        else if (key2ThisFrame && !key2LastFrame) { activeSlot = slot2; Debug.Log("2 Pressed - Slot 2 Active"); }
        
        //Set Target
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

        //Use Equipment (RMB) (this will eventually first loop through to find which Slot is selected, but for this first test is just for the Harvester)
        if (rmbThisFrame && !rmbLastFrame) { UseEquipment(activeSlot); }

        //Update all mouse & keyboard bool states
        SetLastFrame();
    }

    //==== METHODS ====
    void Consciousness() { energy -= 1; } //Constant Energy drain representing N0M-AD's brain at work

    //When N0M-AD collides with an outside collider for the first time
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Reduce Hull by an amount relative to the collision velocity magnitude and collider mass
        hull -= (collision.relativeVelocity.magnitude + ((collision.gameObject.GetComponent<Rigidbody2D>().mass / 200) * collision.relativeVelocity.magnitude)) * (1 - hullResist);
        if (hull <= 0) SceneManager.LoadScene("Title Screen"); //Current "Death Condition" Result
    }

    //Activate or Deactivate the selected Equipment being used
    private void UseEquipment(MonoBehaviour equipment)
    {
        switch (equipment)
        {
            case Harvester h:
                if (!h.Active && target != null) { h.SetActive(target); } //If the Harvester isn't active when clicked, activate it towards a target within range
                else //If the Harvester is active when clicked...
                {
                    if (target != null) { h.SetActive(target); Debug.Log("Target Changed"); } //If selecting a new target, switch to Active towards it
                    else { h.SetInactive(); } //If there's no target, deactivate Harvester
                }
                break;
            default:
                Debug.Log("NOTHING HAPPENED.");
                break;
        }
    }
    private void SetThisFrame() //Set 'this-frame' mouse & keyboard values
    {
        //MOUSE
        lmbThisFrame = Mouse.current.leftButton.IsPressed();
        rmbThisFrame = Mouse.current.rightButton.IsPressed();

        //KEYBOARD
        key1ThisFrame = Keyboard.current.digit1Key.IsPressed();
        key2ThisFrame = Keyboard.current.digit2Key.IsPressed();
    }
    private void SetLastFrame() //Set 'last-frame' mouse & keyboard values
    {
        //MOUSE
        lmbLastFrame = lmbThisFrame;
        rmbLastFrame = rmbThisFrame;

        //KEYBOARD
        key1LastFrame = key1ThisFrame;
        key2LastFrame = key2ThisFrame;
    }
}
