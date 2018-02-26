using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class selection : MonoBehaviour 
{
    private GameObject _selectedobject;

	// Update is called once per frame
	void Update () 
    {
        if (_selectedobject == null || !_selectedobject.activeInHierarchy)
        {
            if (_selectedobject != null && !_selectedobject.activeInHierarchy)
            {
                deselect();
            }
        }

        if (Input.GetMouseButton(0) && buildsystem.instance._buildTower==false)
        {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, (1 << LayerMask.NameToLayer("selectable")));

            if (hit.collider != null)
            {
                GameObject hitGameObject = hit.collider.gameObject;

                if(hitGameObject.tag == "selectable")
                {
                    select(hitGameObject);
                }
            }
        }
        else if (Input.GetMouseButton(1) && buildsystem.instance._buildTower == false)
        {
            deselect();
        }
	}

    private void select(GameObject toSelect)
    {
        if (toSelect.GetComponentInParent<invader>())
        {
            deselect();
            _selectedobject = toSelect.transform.parent.gameObject;
        }
        else if (toSelect.GetComponentInParent<tower>())
        {
            buildsystem.instance._buildTower = false;
            deselect();
            _selectedobject = toSelect.transform.parent.gameObject;
        }

        if (_selectedobject)
        {
            Color newcolor = _selectedobject.GetComponent<SpriteRenderer>().color;
            newcolor.a = 0.25f;
            _selectedobject.GetComponent<SpriteRenderer>().color = newcolor;

            GameObject range = _selectedobject.transform.Find("range").gameObject; 

            if(range)
            {
                range.SetActive(true);
            }
        }
    }

    private void deselect()
    {
        if (_selectedobject)
        {
            Color newcolor = _selectedobject.GetComponent<SpriteRenderer>().color;
            newcolor.a = 1.0f;
            _selectedobject.GetComponent<SpriteRenderer>().color = newcolor;

            GameObject range = _selectedobject.transform.Find("range").gameObject;

            if (range)
            {
                range.SetActive(false);
            }
         }

        _selectedobject = null;
    }
    public void swichtobuildmode()
    {
        deselect();
        _selectedobject = null;
    }
}
