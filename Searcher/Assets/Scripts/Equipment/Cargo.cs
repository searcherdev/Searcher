using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Cargo : MonoBehaviour
{
    //==== FIELDS ====
    private Dictionary<string, int> hold; //Inventory: key = name, value = amount
    private float maxCargo;

    //==== PROPERTIES ====
    public Dictionary<string, int> Hold { get { return hold; } set { hold = value; } }
    public float MaxCargo { get { return maxCargo; } set { maxCargo = value; } }

    //==== UPDATE ====
    private void Update()
    {
        //Keep Resource Values From Dropping Into Negative Values
        if (hold["Ore"] < 0) hold["Ore"] = 0;
        if (hold["Gas"] < 0) hold["Gas"] = 0;
        if (hold["Metals"] < 0) hold["Metals"] = 0;
        if (hold["Fuel"] < 0) hold["Fuel"] = 0;
        if (hold["Circuits"] < 0) hold["Circuits"] = 0;
        if (hold["Heavy Metals"] < 0) hold["Heavy Metals"] = 0;
        if (hold["Jump Fuel"] < 0) hold["Jump Fuel"] = 0;
        if (hold["Computer Circuits"] < 0) hold["Computer Circuits"] = 0;

        //Keep Cargo Hold at at or below max inventory
        if (hold.Count > maxCargo) hold.Remove(hold.Last().Key);
    }

    //==== METHODS ====
    public void BaseCargoValues()
    {
        hold.Add("Ore", 0);
        hold.Add("Gas", 0);
        hold.Add("Metals", 0);
        hold.Add("Fuel", 0);
        hold.Add("Circuits", 0);
        hold.Add("Heavy Metals", 0);
        hold.Add("Jump Fuel", 0);
        hold.Add("Computer Circuits", 0);
    }

    /*
     I will also be keeping information on every single Resource or Equipment in the game in this class to be able to access quickly
     */
}
