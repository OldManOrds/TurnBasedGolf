using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    public ClubTrigger trigger;
    public Ball hitBall;
    public float str = 10f;
    float maxAngle = 60f;
    float minAngle = -20f;
    public float rotationSpeed = 10f;
    float desiredAngle = 0f;
    Vector3 vertAxis = new Vector3(0, 1, 0);
    public float smoothing = .1f;
    Vector3 defaultClubPos;
    bool acceptingInputs = true;
    float highestAngleThisSwing = 0f;




    // Start is called before the first frame update
    void Start()
    {
        if (trigger == null)
            trigger = GetComponentInChildren<ClubTrigger>();

        if (hitBall == null)
            hitBall = FindObjectOfType<Ball>();

        defaultClubPos = transform.position - hitBall.transform.position;

    }

    public void ballStruck()
    {
        Debug.Log("hit");


        acceptingInputs = false;
        desiredAngle = 0f;
        transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        

        Vector3 hitDirection = hitBall.transform.position - trigger.transform.position;
        hitDirection.Normalize();
        hitBall.SetDirection(hitDirection);

        float hitMagnitude = str;
        hitBall.SetMagnitude(hitMagnitude);

        trigger.gameObject.SetActive(false);
        hitBall.Hit();



    }

    public void EnableTrigger()
    {
        trigger.gameObject.SetActive(true);
    }

    public void ResetClub()
    {
        transform.position = hitBall.transform.position + defaultClubPos;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        highestAngleThisSwing = 0f;
        acceptingInputs = true;
        Invoke("EnableTrigger", smoothing);
    }



    // Update is called once per frame
    void Update()
    {
        
        SetStrength();
        GetInput();
    }

   

    void SetStrength()
    {
        str = (highestAngleThisSwing + 10)/2.5f;
    }

    void GetInput()
    {
        if (acceptingInputs)
        {
            if (Input.GetMouseButton(0))
            {
                float Yinput = Input.GetAxis("Mouse Y");
                desiredAngle -= Yinput * rotationSpeed * Time.deltaTime;

                desiredAngle = Mathf.Clamp(desiredAngle, minAngle, maxAngle);

                if(desiredAngle > highestAngleThisSwing) 
                { 
                    highestAngleThisSwing = desiredAngle;
                }

                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(desiredAngle, transform.rotation.eulerAngles.y, 0f), smoothing);
            }
            else
            {
                highestAngleThisSwing = 0f;
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), smoothing);
                desiredAngle = 0f;

                if (Input.GetKey(KeyCode.A))
                {
                    transform.RotateAround(hitBall.transform.position, vertAxis, rotationSpeed/4 * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.RotateAround(hitBall.transform.position, vertAxis, -rotationSpeed/4 * Time.deltaTime);
                }
            }

        }

    }

}

public class Putter : Club
{

}

public class Iron : Club
{

}

public class Driver : Club
{

}