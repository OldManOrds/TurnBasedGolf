using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 Direction;
    float Magnitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Hit()
    {
        rb.velocity = Direction * Magnitude;
    }

    public void SetDirection(Vector3 desiredDirection)
    {
        Direction = desiredDirection;
    }

    public void SetMagnitude(float desiredMagnitude)
    {
        Magnitude = desiredMagnitude;
    }

}

public class GolfBall : Ball
{

}

public class BaseballBall : Ball
{

}

public class TennisBall : Ball
{

}