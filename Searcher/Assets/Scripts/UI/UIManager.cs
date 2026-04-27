using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    //==== FIELDS====
    [SerializeField] Slider energyBar;
    [SerializeField] TMP_Text energyBarText;
    [SerializeField] Slider hullBar;
    [SerializeField] TMP_Text hullBarText;

    [SerializeField] GameObject equipmentSlots;

    N0MAD n0mad;
    private float energy;
    private float maxEnergy;
    private float hull;
    private float maxHull;
    private List<MonoBehaviour> slotList;
    private MonoBehaviour activeSlot;
    
    //==== START ====
    void Start()
    {
        //Set N0M-AD related values
        n0mad = GameObject.FindGameObjectWithTag("Player").GetComponent<N0MAD>();
        energy = n0mad.Energy;
        maxEnergy = n0mad.MaxEnergy;
        hull = n0mad.Hull; 
        maxHull = n0mad.MaxHull;
        slotList = n0mad.SlotList;
        activeSlot = n0mad.ActiveSlot;
    }

    //==== UPDATE ====
    void Update()
    {
        //Get current N0M-AD values
        energy = n0mad.Energy;
        maxEnergy = n0mad.MaxEnergy;
        hull = n0mad.Hull;
        maxHull = n0mad.MaxHull;
        slotList = n0mad.SlotList;
        activeSlot = n0mad.ActiveSlot;

        //Update Energy values
        energyBar.maxValue = maxEnergy;
        energyBar.value = energy;
        if (energyBar.value < 0) energyBar.value = 0;
        energyBarText.text = $"ENERGY [{energy} / {maxEnergy}]";
        
        //Update Hull values
        hullBar.maxValue = maxHull;
        hullBar.value = hull;
        if (hullBar.value < 0) hullBar.value = 0;
        hullBarText.text = $"HULL [{hull} / {maxHull}]";

        //Update Equipment Slot UI
        foreach (MonoBehaviour slot in slotList) //Loop through all equipment slots
        {
            int index = slotList.IndexOf(slot); //Find the slot's index in slotList
            if (slot == activeSlot) {  equipmentSlots.transform.GetChild(index).GetComponent<Image>().color = Color.darkGreen; } //If a slot is active, highlight it
            else { equipmentSlots.transform.GetChild(index).GetComponent<Image>().color = Color.clear; } //If it's not active, clear any highlight
        }
    }
}
