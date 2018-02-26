using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class triggerDestroyOnCollision : MonoBehaviour 
{
    public string tagOfObjectToDestroy;

    public void Start()
    {
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == tagOfObjectToDestroy && other != null)
        {
            Destroy(other);
            other = null;
        }
    }
}
