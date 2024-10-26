using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Club : MonoBehaviour
{
    public ClubTrigger trigger;
    public GameObject ball;
    //public Ball hitBall;
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
    public Slider forceUI;





    // Start is called before the first frame update
    void Start()
    {
        if (trigger == null)
            trigger = GetComponentInChildren<ClubTrigger>();

        if (ball == null)
        {
            ball = FindObjectOfType<Ball>().gameObject;
            
        }

        defaultClubPos = transform.position - ball.transform.position;

    }

    public void ballStruck()
    {
        Debug.Log("hit");


        acceptingInputs = false;
        desiredAngle = 0f;
        transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        

        Vector3 hitDirection = ball.transform.position - trigger.transform.position;
        hitDirection.Normalize();
        
        if (ball.GetComponent<Ball>())
            ball.GetComponent<Ball>().SetDirection(hitDirection);

        if (ball.GetComponent<ChangeBallHue>())
            ball.GetComponent<ChangeBallHue>().SetDirection(hitDirection);

        float hitMagnitude = str;

        if (ball.GetComponent<Ball>())
            ball.GetComponent<Ball>().SetMagnitude(hitMagnitude);

        if (ball.GetComponent<ChangeBallHue>())
            ball.GetComponent<ChangeBallHue>().SetMagnitude(hitMagnitude);

        trigger.gameObject.SetActive(false);

        if (ball.GetComponent<Ball>())
            ball.GetComponent<Ball>().Hit();

        if (ball.GetComponent<ChangeBallHue>())
            ball.GetComponent<ChangeBallHue>().Hit();

        GameManager.Instance.AddScore(1);



    }

    public void EnableTrigger()
    {
        trigger.gameObject.SetActive(true);
    }

    public void ResetClub()
    {
        transform.position = ball.transform.position + defaultClubPos;
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
                    transform.RotateAround(ball.transform.position, vertAxis, rotationSpeed/4 * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.RotateAround(ball.transform.position, vertAxis, -rotationSpeed/4 * Time.deltaTime);
                }
            }

            //hue shifter input
            if(Input.GetKeyDown(KeyCode.B))
            {
                if (ball.GetComponent<Ball>().enabled)
                {
                    if (ball.GetComponent<ChangeBallHue>() == null) 
                    { 
                        ball.AddComponent<ChangeBallHue>();
                    }
                    else 
                    { 
                        ball.GetComponent<ChangeBallHue>().enabled = true; 
                    }
                    
                    ball.GetComponent<Ball>().enabled = false;
                }
                else
                {
                    ball.GetComponent<Ball>().enabled = true;
                    ball.GetComponent<ChangeBallHue>().enabled = false;
                }
            }

        }

    }
    //public void Slider()
    //{
    //    forceUI.value = str;
    //}
    public void ResetGauge()
    {
        str = 0f;
        forceUI.value = 0f;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        ResetGauge();
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