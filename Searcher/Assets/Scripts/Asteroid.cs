using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //==== FIELDS ====
    private float rotSpeed;
    private float zRot;
    
    //==== START ====
    void Start()
    {
        //Set random starting rotation & rotation speed
        rotSpeed = Random.Range(-10, 10);
        zRot = Random.Range(-360, 360);

        //Set initial rotation
        Quaternion tempRot = transform.rotation;
        tempRot.z = zRot;
        transform.rotation = tempRot;
    }

    //==== UPDATE ====
    void Update()
    {
        //Add rotation
        zRot += rotSpeed;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, zRot);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime);
    }
}
