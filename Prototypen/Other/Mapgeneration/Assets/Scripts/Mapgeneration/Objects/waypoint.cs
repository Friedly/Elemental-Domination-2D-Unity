using UnityEngine;
using System.Collections;

public class waypoint : MonoBehaviour 
{
    public waypoint nextwaypoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        invader invader = other.GetComponent<invader>();
        //if (other.tag == "Invader" && invader!= null)
        if (invader != null)
        {
            invader.nextWaypoint = nextwaypoint;
        }
    }

}
