using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Club club;
    public Rigidbody rb;
    Vector3 Direction;
    float Magnitude;
    public float stoppingVel = .05f;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if(club == null)
            club = FindAnyObjectByType<Club>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < stoppingVel && rb.isKinematic == false)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            
            transform.rotation = Quaternion.identity;

            club.ResetClub();
            
            Debug.Log("stopped ball");
        }

    }

    public void Hit()
    {
        rb.isKinematic = false;
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