using UnityEngine;

public class Equipment : MonoBehaviour
{
    //==== FIELDS ====
    protected float rate;
    protected float timer;
    protected float range;
    protected bool active;
    protected GameObject target;
    protected GameObject manager;
    protected N0MAD n0mad;

    //==== PROPERTIES ====
    public float Rate { get { return rate; } set { rate = value; } }
    public float Timer { get { return timer; } set { timer = value; } }
    public float Range { get { return range; } set { range = value; } }
    public bool Active {  get { return active; } set { active = value; } }
    public GameObject Target { get { return target; } set { target = value; } }
    
    //==== Awake ====
    void Awake()
    {
        n0mad = GameObject.FindGameObjectWithTag("Player").GetComponent<N0MAD>();
        manager = GameObject.FindGameObjectWithTag("Manager");

        active = false;
        timer = 0f;
    }

    //==== UPDATE ====
    void Update()
    {
        
    }
}
