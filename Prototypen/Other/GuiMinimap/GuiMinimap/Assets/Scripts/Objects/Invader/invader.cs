using UnityEngine;
using System.Collections;

[System.Serializable]
public class resistance
{
    public int fire=100;
    public int water=100;
    public int air=100;
    public int earth=100;
}
public class invader : MonoBehaviour 
{
    public float live=100;
    public resistance resistance = new resistance();
    public float speed=1;
    public int damageOnLive=1;
    public int elementalenergy=10;
    public triggerWaypoint nextWaypoint;
    public triggerWaypoint currentWaypoint;
    public static int invaderCount = 0;

    public void OnEnable()
    {
        invaderCount++;
    }
    public void Update()
    {
        if (currentWaypoint != null)
        {
            Vector3 dir = currentWaypoint.transform.position - transform.position;
            dir.Normalize();
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, speed * Time.deltaTime);
            if (currentWaypoint.transform.position == transform.position)
            {
                currentWaypoint = nextWaypoint;
            }
        }
        else
        {
            currentWaypoint = nextWaypoint;
        }
    }
    private void OnDisable()
    {
        invaderCount--;
    }
}
