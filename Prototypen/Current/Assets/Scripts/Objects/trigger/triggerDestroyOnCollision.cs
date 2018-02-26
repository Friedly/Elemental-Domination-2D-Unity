using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class triggerDestroyOnCollision : MonoBehaviour 
{
    public string tagOfObjectToDestroy;
    public Vector2 triggerSize;

    public void Start()
    {
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
            boxCollider.size = triggerSize;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == tagOfObjectToDestroy && other != null)
        {
            if (transform.position == other.transform.position)
            {
                /*dirty-code*/
                invader invader = other.gameObject.GetComponent<invader>();
                invader.killedBy = this.tag;
                /**/

                other.gameObject.SetActive(false);
            }
        }
    }
}
