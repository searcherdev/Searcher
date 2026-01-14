using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //==== FIELDS ====
    //Vectors
    private Vector3 position = Vector3.zero;
    [SerializeField] private Vector3 direction = Vector3.zero;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    //Values
    private float accel; //Will be taken from N0M-AD's Engine
    private const float decel = -0.01f; //Constant deceleration in space
    private float maxSpeed;

    //Camera
    private Camera cam;
    private float camHeight;
    private float camWidth;

    //==== PROPERTIES ====
    //Vectors
    public Vector3 Position { get { return position; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Velocity { get { return velocity; } }

    //Values
    public float Acceleration { get { return accel; } }
    public float Decel { get { return decel; } }
    public float MaxSpeed { get { return maxSpeed; } }
    
    //==== START ====
    void Start()
    {
        //Set Camera
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2.0f;
        camWidth = cam.orthographicSize * cam.aspect;

        //Set position
        position = transform.position;

        //Set movement values (NOTE: WILL BE REPLACED WITH EVENTUAL N0M-AD CLASS VALUES)
        accel = 0.05f;
        maxSpeed = 0.5f;
    }

    //==== UPDATE ====
    void Update()
    {
        //Arrow Key Movement
        if (Keyboard.current.leftArrowKey.isPressed) velocity.x -= (accel * Time.deltaTime); //Left
        if (Keyboard.current.rightArrowKey.isPressed) velocity.x += (accel * Time.deltaTime); //Right
        if (Keyboard.current.downArrowKey.isPressed) velocity.y -= (accel * Time.deltaTime); //Down
        if (Keyboard.current.upArrowKey.isPressed) velocity.y += (accel * Time.deltaTime); //Up

        //Clamp Movement Speed
        if (velocity.x <= -maxSpeed) velocity.x = -maxSpeed;
        if (velocity.x >= maxSpeed) velocity.x = maxSpeed;
        if (velocity.y <= -maxSpeed) velocity.y = -maxSpeed;
        if (velocity.y >= maxSpeed) velocity.y = maxSpeed;

        //Apply Universal Decel
        if (velocity.x < -0) velocity.x -= (decel * Time.deltaTime);
        else if (velocity.x > 0) velocity.x += (decel * Time.deltaTime);
        if (velocity.y < -0) velocity.y -= (decel * Time.deltaTime);
        else if (velocity.y > 0) velocity.y += (decel * Time.deltaTime);

        //Set Player Position
        position.x += (velocity.x * Time.deltaTime);
        position.y += (velocity.y * Time.deltaTime);
        transform.position = position;
    }

    //==== FUNCTIONS ====
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}
