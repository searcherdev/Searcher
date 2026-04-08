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

    //RigidBody2D
    private Rigidbody2D rigidBody;

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

        //Set movement values
        accel = n0mad.Engine.Accel;
        maxSpeed = n0mad.Engine.MaxSpeed;
        rotSpeed = n0mad.Engine.RotSpeed;

        //Set RigidBody
        rigidBody = GetComponent<Rigidbody2D>();
    }

    //==== UPDATE ====
    void Update()
    {
        //Set isInUse to false by default
        n0mad.Engine.IsInUse = false;
        
        //Arrow Key Movement (Engine is In Use & using Energy if any of these keys are pressed)
        if (Keyboard.current.leftArrowKey.isPressed) { rigidBody.linearVelocityX -= (accel * Time.deltaTime); n0mad.Engine.IsInUse = true; } //Left
        if (Keyboard.current.rightArrowKey.isPressed) { rigidBody.linearVelocityX += (accel * Time.deltaTime); n0mad.Engine.IsInUse = true; } //Right
        if (Keyboard.current.downArrowKey.isPressed) { rigidBody.linearVelocityY -= (accel * Time.deltaTime); n0mad.Engine.IsInUse = true; } //Down
        if (Keyboard.current.upArrowKey.isPressed) { rigidBody.linearVelocityY += (accel * Time.deltaTime); n0mad.Engine.IsInUse = true; } //Up

        //Clamp Movement Speed
        if (rigidBody.linearVelocityX <= -maxSpeed) rigidBody.linearVelocityX = -maxSpeed; //Left
        if (rigidBody.linearVelocityX >= maxSpeed) rigidBody.linearVelocityX = maxSpeed; //Right
        if (rigidBody.linearVelocityY <= -maxSpeed) rigidBody.linearVelocityY = -maxSpeed; //Down
        if (rigidBody.linearVelocityY >= maxSpeed) rigidBody.linearVelocityY = maxSpeed; //Up

        //Apply Universal Decel
        float speed = rigidBody.linearVelocity.magnitude;
        speed = Mathf.MoveTowards(speed, 0f, decel * Time.deltaTime);
        if (speed > 0f) rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * speed;
        else rigidBody.linearVelocity = Vector3.zero;

        //Set Player Position
        position.x += (rigidBody.linearVelocityX * Time.deltaTime);
        position.y += (rigidBody.linearVelocityY * Time.deltaTime);
        transform.position = position;

        //Move Camera to Player Position
        cam.transform.position = new Vector3(position.x, position.y, -10f);

        //Rotate Sprite Towards Velocity Over Time If Moving
        direction = rigidBody.linearVelocity.normalized;
        if (direction != Vector3.zero)
        {
            float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }

        //Decelerate angular velocity on the RigidBody
        rigidBody.angularVelocity -= 5 * decel * Time.deltaTime;
        if (rigidBody.angularVelocity <= 0) rigidBody.angularVelocity = 0;
    }

    //==== FUNCTIONS ====
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}
