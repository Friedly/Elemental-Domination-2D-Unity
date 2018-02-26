using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour 
{
    public Waypoint nextwaypoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Invader invader = other.GetComponent<Invader>();
        if (other.tag == "Invader" && invader!= null)
        {
            invader.nextWaypoint = nextwaypoint;
        }
    }

}
