using System;
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
    private const float decel = 0.05f; //Constant deceleration in space
    private float maxSpeed;
    private float rotSpeed;

    //Camera
    private Camera cam;
    private float camHeight;
    private float camWidth;

    //N0M-AD
    private N0MAD n0mad;

    //==== PROPERTIES ====
    //Vectors
    public Vector3 Position { get { return position; } }
    public Vector3 Direction { get { return direction; } }
    public Vector3 Velocity { get { return velocity; } }

    //Values
    public float Acceleration { get { return accel; } }
    public float Decel { get { return decel; } }
    public float MaxSpeed { get { return maxSpeed; } }
    public float RotSpeed { get { return rotSpeed; } }
    
    //==== START ====
    void Start()
    {
        //Set Camera
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2.0f;
        camWidth = cam.orthographicSize * cam.aspect;

        //Set position
        position = transform.position;

        //Set N0M-AD
        n0mad = GetComponent<N0MAD>();

        //Set movement values (NOTE: WILL BE REPLACED WITH EVENTUAL N0M-AD CLASS VALUES)
        accel = n0mad.Engine.Accel;
        maxSpeed = n0mad.Engine.MaxSpeed;
        rotSpeed = n0mad.Engine.RotSpeed;

        /*accel = 1f;
        maxSpeed = 5f;
        rotSpeed = 50f;*/
    }

    //==== UPDATE ====
    void Update()
    {
        //Arrow Key Movement
        if (Keyboard.current.leftArrowKey.isPressed) { velocity.x -= (accel * Time.deltaTime); } //Left
        if (Keyboard.current.rightArrowKey.isPressed) { velocity.x += (accel * Time.deltaTime); } //Right
        if (Keyboard.current.downArrowKey.isPressed) { velocity.y -= (accel * Time.deltaTime); } //Down
        if (Keyboard.current.upArrowKey.isPressed) { velocity.y += (accel * Time.deltaTime); }//Up

        //Clamp Movement Speed
        if (velocity.x <= -maxSpeed) velocity.x = -maxSpeed;
        if (velocity.x >= maxSpeed) velocity.x = maxSpeed;
        if (velocity.y <= -maxSpeed) velocity.y = -maxSpeed;
        if (velocity.y >= maxSpeed) velocity.y = maxSpeed;

        //Apply Universal Decel
        float speed = velocity.magnitude;
        speed = Mathf.MoveTowards(speed, 0f, decel * Time.deltaTime);
        if (speed > 0f) velocity = velocity.normalized * speed;
        else velocity = Vector3.zero;

        //Set Player Position
        position.x += (velocity.x * Time.deltaTime);
        position.y += (velocity.y * Time.deltaTime);
        transform.position = position;

        //Move Camera to Player Position
        cam.transform.position = new Vector3(position.x, position.y, -10f);

        //Rotate Sprite Towards Velocity Over Time If Moving
        direction = velocity.normalized;
        if (direction != Vector3.zero)
        {
            float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    //==== FUNCTIONS ====
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}
