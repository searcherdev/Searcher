using UnityEngine;

public class Ammo : MonoBehaviour
{
    //==== FIELDS ====
    private Vector3 target;
    private float speed;
    private Vector3 velocity;
    private float damage;

    //==== PROPERTIES ====
    public Vector3 Target { get { return target; } }
    public float Damage { get { return damage; } }
    
    //==== START ====
    void Start()
    {
        
    }

    //==== UPDATE ====
    void Update()
    {
        
    }

    //==== METHODS ====
    public void SetTarget(Vector3 target) { this.target = target; }
}
