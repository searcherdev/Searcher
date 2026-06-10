using UnityEngine;

public class Weapon : Equipment
{
    //==== FIELDS ====
    private GameObject ammoPrefab;

    private GameObject cursorPrefab;
    private GameObject cursorInstance;
    private GameObject ammoInstance;

    //==== PROPERTIES ====

    //==== START ====
    void Start()
    {
        cursorPrefab = manager.GetComponent<Cursor>().CursorPrefab;
        cursorInstance = null;
        ammoPrefab = manager.GetComponent<UIManager>().AmmoPrefab; //NOT FINAL - this system will change IMMINENTLY
    }

    //==== UPDATE ====
    void Update()
    {
        //If weapon is active, target exists, and target is within range, target the target and fire at rate
        if (active && target != null && Vector3.Distance(this.transform.position, target.transform.position) < range)
        {
            WeaponCursor();
            //Update Weapon Timer
            timer += Time.deltaTime;
            if (timer >= rate)
            {
                timer -= rate;
                Fire();
            }
        }
        else //Otherwise, turn off the weapon
        {
            SetInactive();
        }
    }

    //==== METHODS ====
    public void Fire() //Spawn a projectile & reduce N0M-AD's Energy
    {
        n0mad.Energy -= ammoPrefab.GetComponent<Ammo>().Damage * rate;

        ammoInstance = null;
        ammoInstance = Instantiate(ammoPrefab, n0mad.transform.position, Quaternion.identity);
        ammoInstance.GetComponent<Ammo>().SetTarget(target.transform.position);
        Physics2D.IgnoreCollision(n0mad.GetComponent<PolygonCollider2D>(), ammoInstance.GetComponent<PolygonCollider2D>());
        ammoInstance = null;
    }

    public void WeaponCursor() //Spawn separate cursor for when the Weapon is active
    {
        if (cursorInstance != null)
        {
            cursorInstance.transform.position = target.transform.position;
            if (active && n0mad.ActiveSlot == this) cursorInstance.GetComponent<SpriteRenderer>().color = Color.green;
            else if (active && n0mad.ActiveSlot != this) cursorInstance.GetComponent<SpriteRenderer>().color = Color.red;
            else cursorInstance.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }

    public void SetActive(GameObject target) //Turn on the weapon & set values
    {
        active = true;
        this.target = target;
        timer = 0f;

        if (cursorInstance != null) Destroy(cursorInstance);
        cursorInstance = Instantiate(cursorPrefab);
        cursorInstance.GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    public void SetInactive() //Turn off the weapon & reset values
    {
        Destroy(cursorInstance);
        cursorInstance = null;

        active = false;
        target = null;
        timer = 0f;
    }
}
