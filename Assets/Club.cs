using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    public ClubTrigger trigger;
    public Ball hitBall;
    public float str = 10f;
    float maxAngle = 60f;
    float minAngle = -15f;
    public float rotationSpeed = 10f;
    float desiredAngle = 0f;
    Vector3 vertAxis = new Vector3(0, 1, 0);




    // Start is called before the first frame update
    void Start()
    {
        if (trigger == null)
            trigger = GetComponentInChildren<ClubTrigger>();

        if (hitBall == null)
            hitBall = FindObjectOfType<Ball>();

    }

    public void ballStruck()
    {
        Debug.Log("hit");

        


        Vector3 hitDirection = hitBall.transform.position - trigger.transform.position;
        hitBall.SetDirection(hitDirection);

        float hitMagnitude = str;
        hitBall.SetMagnitude(hitMagnitude);

        trigger.gameObject.SetActive(false);
        hitBall.Hit();



    }

    


    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            float Yinput = Input.GetAxis("Mouse Y");
            desiredAngle -= Yinput * rotationSpeed * Time.deltaTime;

            desiredAngle = Mathf.Clamp(desiredAngle, minAngle, maxAngle);
            Debug.Log(desiredAngle);

            transform.localRotation = Quaternion.Euler(desiredAngle, transform.rotation.eulerAngles.y, 0f);
        }

        if(Input.GetMouseButtonUp(0))
        {
            transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            desiredAngle = 0f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(hitBall.transform.position, vertAxis, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(hitBall.transform.position, vertAxis, -rotationSpeed * Time.deltaTime);
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