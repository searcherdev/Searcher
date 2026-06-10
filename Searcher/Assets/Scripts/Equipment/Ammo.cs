using UnityEngine;

public class Ammo : MonoBehaviour
{
    //==== FIELDS ====
    private Vector3 target;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Vector3 position;
    private Vector3 velocity;
    
    //==== PROPERTIES ====
    public float Damage { get { return damage; } }

    //==== START ====
    void Start()
    {
        position = transform.position;

        //Calculate velocity
        velocity = new Vector3(((target.x - position.x) * speed * Time.deltaTime), ((target.y - position.y) * speed * Time.deltaTime));
    }

    //==== UPDATE ====
    void Update()
    {
        position += velocity;
        transform.position = position;
    }

    //==== METHODS ====
    public void SetTarget(Vector3 target) { this.target = target; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonoBehaviour targetClass = collision.gameObject.GetComponent<MonoBehaviour>();
        switch (targetClass)
        {
            case N0MAD n:
                n.Hull -= damage;
                break;

            case Asteroid a:
                a.Health -= damage;
                break;

            default:
                break;
        }
        Destroy(this.gameObject);
    }
}
