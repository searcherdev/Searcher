using UnityEngine;

public class Weapon : MonoBehaviour
{
    //==== FIELDS ====
    private GameObject ammoPrefab;
    private float firingRate;
    private float range;
    private float timer;
    private bool active;
    private float energyReduction;

    private N0MAD n0mad;
    private GameObject target;
    private GameObject manager;

    private GameObject cursorPrefab;
    private GameObject cursorInstance;
    private GameObject ammoInstance;

    //==== PROPERTIES ====
    public GameObject Target { get { return target; } }
    public float FiringRate { get { return firingRate; } set { firingRate = value; } }
    public float Range { get { return range; } set { range = value; } }
    public bool Active { get { return active; } }
    public float Timer { get { return timer; } }

    //==== START ====
    void Start()
    {
        n0mad = GameObject.FindGameObjectWithTag("Player").GetComponent<N0MAD>();
        active = false;
        timer = 0f;

        manager = GameObject.FindGameObjectWithTag("Manager");
        cursorPrefab = manager.GetComponent<Cursor>().CursorPrefab;
        cursorInstance = null;
    }

    //==== UPDATE ====
    void Update()
    {
        if (active && target != null)
        {
            //Update Weapon Timer
            timer += Time.deltaTime;
            if (timer >= firingRate)
            {
                timer -= firingRate;
                Fire();
            }
        }
    }

    //==== METHODS ====
    public void Fire()
    {
        n0mad.Energy -= energyReduction;
        ammoInstance = Instantiate(ammoPrefab, n0mad.transform.position, Quaternion.identity);
    }

    public void WeaponCursor()
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
        timer = 0f;
    }
}
