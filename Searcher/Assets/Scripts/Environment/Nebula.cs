using UnityEngine;

public class Nebula : MonoBehaviour
{
    //==== FIELDS ====
    private float zRot;
    private float scale;
    private float density;

    private Vector3 position = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private float gas;
    private Collisions collisions;

    //==== PROPERTIES ====
    public float Scale { get { return scale; } }
    public Vector3 Position { get { return position; } set { position = value; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Velocity { get { return velocity; } }
    public float Gas { get { return gas; } set { gas = value; } }
    public float Density { get { return density; } set { density = value; } }

    //==== START ====
    void Start()
    {
        //Set random starting rotation & scale
        zRot = Random.Range(-360f, 360f);
        scale = Random.Range(5f, 15f);

        //Set initial rotation
        transform.rotation = Quaternion.Euler(0, 0, zRot);

        //Set initial scale
        transform.localScale = new Vector3(scale, scale, 1);

        //Set initial velocity
        position = transform.position;
        velocity = new Vector3(Random.Range(-.1f, .1f) * (1 / scale), Random.Range(-.1f, .1f) * (1 / scale), 0);

        //Set Gas Count
        gas = Mathf.Round(Random.Range(5f, 20f) * (scale / 2));

        //Set Density
        density = (gas / scale) / 7.5f;

        //Set Collisions
        collisions = new Collisions();
    }

    //==== UPDATE ====
    void Update()
    {
        //Change position by velocity
        position.x += velocity.x * Time.deltaTime;
        position.y += velocity.y * Time.deltaTime;
        transform.position = position;

        //Affect N0M-AD Velocity (and eventually other object velocities BUT ONE THING AT A TIME)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (collisions.CheckSpriteCollision(this.gameObject, player))
        {
            Vector3 playerVelocity = player.GetComponent<Player>().Velocity;
            Vector3.MoveTowards(playerVelocity, Vector3.zero, density * Time.deltaTime);
            player.GetComponent<Player>().Velocity = playerVelocity;
        }

    }
}
