using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //==== FIELDS====
    [SerializeField] Slider energyBar;
    [SerializeField] TMP_Text energyBarText;
    [SerializeField] Slider hullBar;
    [SerializeField] TMP_Text hullBarText;

    N0MAD n0mad;
    private float energy;
    private float maxEnergy;
    private float hull;
    private float maxHull;
    
    //==== START ====
    void Start()
    {
        //Set N0M-AD related values
        n0mad = GameObject.FindGameObjectWithTag("Player").GetComponent<N0MAD>();
        energy = n0mad.Energy;
        maxEnergy = n0mad.MaxEnergy;
        hull = n0mad.Hull; 
        maxHull = n0mad.MaxHull;
    }

    //==== UPDATE ====
    void Update()
    {
        //Get current values
        energy = n0mad.Energy;
        maxEnergy = n0mad.MaxEnergy;
        hull = n0mad.Hull;
        maxHull = n0mad.MaxHull;
        
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
    }
}
