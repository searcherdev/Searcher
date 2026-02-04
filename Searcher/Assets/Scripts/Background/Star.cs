using UnityEngine;

public class Star : MonoBehaviour
{
    //==== FIELDS ====
    private Vector3 scale = Vector3.zero;
    private const float min = 0.01f;
    private const float max = 0.015f;
    private float elapsedTime;
    private float randomTime;
    private int randomStart;
    
    //==== START ====
    void Start()
    {
        //Set Initial Randoms
        randomStart = Random.Range(1, 3);
        randomTime = Random.Range(0, 9);

        //Set Initial Scale
        if (randomStart == 1) scale = new Vector3(min, min);
        else scale = new Vector3(max, max);
        transform.localScale = scale;

        //Set Elapsed Time
        elapsedTime = 0;
    }

    //==== UPDATE ====
    void Update()
    {
        //Keep track of time
        elapsedTime += Time.deltaTime;

        //If enough time has passed...
        if (elapsedTime >= randomTime)
        {
            //Reset timer
            elapsedTime = 0;
            randomTime = Random.Range(0, 9);

            //Change star scale based on current state
            if (scale.x == min) scale = new Vector3(max, max);
            else scale = new Vector3(min, min);
            transform.localScale = scale;
        }
    }
}
