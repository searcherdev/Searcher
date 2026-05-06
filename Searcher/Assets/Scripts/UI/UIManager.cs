using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIManager : MonoBehaviour
{
    //==== FIELDS====
    [SerializeField] Slider energyBar;
    [SerializeField] TMP_Text energyBarText;
    [SerializeField] Slider hullBar;
    [SerializeField] TMP_Text hullBarText;

    [SerializeField] GameObject equipmentSlots;
    //[SerializeField] GameObject equipmentSlotPrefab;

    [SerializeField] Sprite s_Ore;
    [SerializeField] Sprite s_Gas;

    private bool menuActive;
    [SerializeField] GameObject m_CargoPrefab;
    private GameObject menuInstance;

    N0MAD n0mad;
    private float energy;
    private float maxEnergy;
    private float hull;
    private float maxHull;
    private List<MonoBehaviour> slotList;
    private MonoBehaviour activeSlot;
    private Cargo cargo;

    private bool escKeyThisFrame;
    private bool escKeyLastFrame;
    private bool cKeyThisFrame;
    private bool cKeyLastFrame;
    
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
        cargo = n0mad.Cargo;

        menuActive = false;
    }

    //==== UPDATE ====
    void Update()
    {
        //Set States
        SetKeyboardStates();
        
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
            switch (slot)
            {
                case Harvester h:
                    if (slot == activeSlot) { equipmentSlots.transform.GetChild(index).GetChild(0).GetComponent<Image>().color = Color.darkGreen; } //If the slot is the active selected slot, highlight it green
                    else if (slot != activeSlot && h.Active) { equipmentSlots.transform.GetChild(index).GetChild(0).GetComponent<Image>().color = Color.cyan; } //If the slot isn't selected but its equipment is active, highlight it yellow
                    else { equipmentSlots.transform.GetChild(index).GetChild(0).GetComponent<Image>().color = Color.clear; } //If it's not active or selected, clear any highlight

                    if (h.Active) //Update equipment slot color scaling when active
                    {
                        Vector3 tempScale = equipmentSlots.transform.GetChild(index).GetChild(0).localScale;
                        tempScale.x = h.Timer / h.Rate;
                        equipmentSlots.transform.GetChild(index).GetChild(0).localScale = tempScale;
                    }
                    else //Set equipment slot color scaling to full when inactive
                    {
                        Vector3 tempScale = equipmentSlots.transform.GetChild(index).GetChild(0).localScale;
                        tempScale.x = 1;
                        equipmentSlots.transform.GetChild(index).GetChild(0).localScale = tempScale;
                    }

                    break;
                default: //If there's nothing in the Equipment slot, set it green or nuthin'
                    if (slot == activeSlot) { equipmentSlots.transform.GetChild(index).GetChild(0).GetComponent<Image>().color = Color.darkGreen; }
                    else { equipmentSlots.transform.GetChild(index).GetChild(0).GetComponent<Image>().color = Color.clear; }
                    break;
            }
        }

        //Activate/Deactivate Menu
        if (!menuActive && menuInstance == null && ((escKeyThisFrame && !escKeyLastFrame) || (cKeyThisFrame && !cKeyLastFrame))) //Activate
        { 
            menuInstance = Instantiate(m_CargoPrefab); 
            menuActive = true; 
        }
        else if (menuActive && menuInstance != null && (escKeyThisFrame && !escKeyLastFrame)) //Deactivate
        { 
            Destroy(menuInstance.gameObject); 
            menuInstance = null; 
            menuActive = false; 
        }

        //If the menu is active, populate its inventory UI [WILL NEED TO BE UPDATED TO FUNCTION DEPENDING ON WHICH MENU IS OPEN]
        if (menuActive && menuInstance != null)
        {
            Transform cargoSlotContainer = menuInstance.transform.GetChild(0).GetChild(0).GetChild(2);
            for (int i = 0; i < cargoSlotContainer.childCount; i++) //Get each cargo slot
            {
                Transform slot = cargoSlotContainer.GetChild(i); //Get the current cargo slot
                if (i < cargo.Hold.Count) //If there's something to put in this slot, put it there
                {
                    slot.GetChild(2).GetComponent<TMP_Text>().text = cargo.Hold.ElementAt(i).Key; //Set key label
                    slot.GetChild(3).GetComponent<TMP_Text>().text = cargo.Hold.ElementAt(i).Value.ToString(); //Set value label

                    slot.GetChild(1).GetComponent<Image>().color = Color.white; //Populate image of cargo slot [SHUNT THIS EVENTUALLY INTO A METHOD]
                    if (cargo.Hold.ElementAt(i).Key == "Ore") { slot.GetChild(1).GetComponent<Image>().sprite = s_Ore; }
                    else if (cargo.Hold.ElementAt(i).Key == "Gas") { slot.GetChild(1).GetComponent<Image>().sprite = s_Gas; }
                }
                else //Otherwise, empty the slot
                {
                    slot.GetChild(1).GetComponent<Image>().sprite = null; //Nullify image
                    slot.GetChild(1).GetComponent<Image>().color = Color.clear;
                    slot.GetChild(2).GetComponent<TMP_Text>().text = ""; //Nullify label
                    slot.GetChild(3).GetComponent<TMP_Text>().text = ""; //Nullify count
                }
            }
        }

        //Set Last Frame States
        SetLastFrameStates();
    }

    //==== METHODS ====
    private void SetKeyboardStates()
    {
        escKeyThisFrame = Keyboard.current.escapeKey.isPressed;
        cKeyThisFrame = Keyboard.current.cKey.isPressed;
    }
    private void SetLastFrameStates()
    {
        escKeyLastFrame = escKeyThisFrame;
        cKeyLastFrame = cKeyThisFrame;
    }
}
