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
    public Material mat;
    public bool hueShift = false;
    public Color nextColor;
    public float hueShiftRate = .0003f;
    
     

    // Start is called before the first frame update
    void Awake()
    {
        hueShift = false;

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if(club == null)
            club = FindAnyObjectByType<Club>();

        if(mat == null)
            mat = GetComponent<Material>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (hueShift)
        {
            if (mat == null)
                mat = gameObject.GetComponent<MeshRenderer>().material;

            nextColor = mat.color;
            Color.RGBToHSV(nextColor, out float Hue, out float Sat, out float Val);
            Hue += hueShiftRate;
           
            nextColor = Color.HSVToRGB(Hue, Sat, Val);
            mat.color = nextColor;
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {
            GameManager.Instance.NextLevel();
        }
        if (other.CompareTag("LastHole"))
        {
            Debug.Log("HOLE");
            GameManager.Instance.LastLevel();
        }
        if (other.CompareTag("Dead"))
        {
            GameManager.Instance.ReloadScene();
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

public class ChangeBallHue : Ball
{
    public void Start()
    {
        hueShift = true;
    }
}
