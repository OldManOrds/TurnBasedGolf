using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubTrigger : MonoBehaviour
{
    Club club;


    // Start is called before the first frame update
    void Start()
    {
        if (club == null)
            club = GetComponentInParent<Club>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        club.ballStruck();
    }
}
