using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class triggerWaypoint : MonoBehaviour
{
    public triggerWaypoint nextWaypoint;

    public void Start()
    {
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector2(0.32f, 0.32f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        invader invader = other.GetComponent<invader>();
        //if (other.tag == "Invader" && invader!= null)
        if (invader != null)
        {
            invader.nextWaypoint = nextWaypoint;
        }
    }
}

