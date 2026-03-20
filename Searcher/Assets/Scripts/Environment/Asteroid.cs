using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //==== FIELDS ====
    private float rotSpeed;
    private float zRot;
    private float scale;

    private Vector3 position = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private Rigidbody2D rigidBody;

    private bool isColliding;

    //==== PROPERTIES ====
    public float Scale { get { return scale; } }
    public Vector3 Position { get { return position; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Velocity { get { return velocity; } }
    public bool IsColliding { get { return isColliding; } set { isColliding = value; } }

    //==== START ====
    void Start()
    {
        //Set random starting rotation & rotation speed
        rotSpeed = Random.Range(-10, 10);
        zRot = Random.Range(-360, 360);
        scale = Random.Range(0.1f, 0.5f);

        //Set initial rotation
        Quaternion tempRot = transform.rotation;
        tempRot.z = zRot;
        transform.rotation = tempRot;

        //Set initial scale
        transform.localScale = new Vector3(scale, scale, 1);

        //Set RigidBody
        rigidBody = GetComponent<Rigidbody2D>();

        //Set initial velocity
        position = transform.position;
        rigidBody.linearVelocity = new Vector3(Random.Range(-.1f, .1f) * (1/scale), Random.Range(-.1f, .1f) * (1/scale), 0);
    }

    //==== UPDATE ====
    void Update()
    {
        //Add rotation
        //transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);

        //Change position by velocity
        position.x += rigidBody.linearVelocity.x * Time.deltaTime;
        position.y += rigidBody.linearVelocity.y * Time.deltaTime;
        transform.position = position;
    }
}
