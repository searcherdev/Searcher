using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //==== FIELDS ====
    private float zRot;
    private float scale;

    private Vector3 position = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private Rigidbody2D rigidBody;

    private float ore;
    private float gas;

    //==== PROPERTIES ====
    public float Scale { get { return scale; } }
    public Vector3 Position { get { return position; } set { position = value; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
    public float Ore { get { return ore; } set { ore = value; } }
    public float Gas { get { return gas; } set { gas = value; } }

    //==== START ====
    void Start()
    {
        //Set random starting rotation & rotation speed
        zRot = Random.Range(-360, 360);
        scale = Random.Range(0.1f, 0.5f);

        //Set initial rotation
        transform.rotation = Quaternion.Euler(0, 0, zRot);

        //Set initial scale
        transform.localScale = new Vector3(scale, scale, 1);

        //Set RigidBody
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.mass = 1000 * scale;

        //Set initial velocity
        position = transform.position;
        rigidBody.linearVelocity = new Vector3(Random.Range(-.1f, .1f) * (1/scale), Random.Range(-.1f, .1f) * (1/scale), 0);

        //Set Ore & Gas Count
        ore = Mathf.Round(Random.Range(1f, 5f) * (scale * 20));
        gas = Mathf.Round(Random.Range(0f, 3f) * (scale * 5));
    }

    //==== UPDATE ====
    void Update()
    {
        //Change position by velocity
        position.x += rigidBody.linearVelocity.x * Time.deltaTime;
        position.y += rigidBody.linearVelocity.y * Time.deltaTime;
        transform.position = position;
    }
}
