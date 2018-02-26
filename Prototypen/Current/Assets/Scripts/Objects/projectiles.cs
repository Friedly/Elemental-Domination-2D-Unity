using UnityEngine;
using System.Collections;

public class projectiles : MonoBehaviour 
{
    private attack _attack;
    private Transform _target;
    public float travelSpeed;

    public void Update()
    {
        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            Destroy(this.gameObject);

            return;
        }

        Vector3 dir = _target.transform.position - transform.position; 
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, travelSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "invader" && _target.gameObject == other.gameObject) 
        {
            invader invader = other.gameObject.GetComponent<invader>();

            invader.health.takeDamage(_attack);

            Destroy(this.gameObject);
        }
    }

    public attack attack
    {
        set
        {
            _attack = value;
        }
    }

    public Transform target
    {
        set
        {
            _target = value;
        }
    }
}
