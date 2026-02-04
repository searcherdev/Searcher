using UnityEngine;

public class Engine : MonoBehaviour
{
    //==== FIELDS ====
    private float accel;
    private float maxSpeed;
    private float rotSpeed;

    //==== PROPERTIES ====
    public float Accel { get { return accel; } set { accel = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float RotSpeed { get {return rotSpeed; } set { rotSpeed = value; } }
}
