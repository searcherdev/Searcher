using UnityEngine;

public class Engine : Equipment
{
    //==== FIELDS ====
    private float accel;
    private float maxSpeed;
    private float rotSpeed;
    private float jumpDistance;

    //==== PROPERTIES ====
    public float Accel { get { return accel; } set { accel = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float RotSpeed { get {return rotSpeed; } set { rotSpeed = value; } }
    public float JumpDistance { get { return jumpDistance; } set { jumpDistance = value; } }
}
