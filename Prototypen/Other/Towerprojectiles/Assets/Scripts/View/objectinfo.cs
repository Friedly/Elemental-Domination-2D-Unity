using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class objectinfo : MonoBehaviour {

    public GameObject towerpanel;
    public GameObject invaderpanel;
    public GameObject towerbuidpanel;
    private GameObject _selectedobject;
    private int _selectedtyp;
	// Update is called once per frame
	void Update () 
    {
        if (_selectedobject == null || !_selectedobject.activeInHierarchy)
        {
            if (_selectedobject != null && !_selectedobject.activeInHierarchy)
            {
                deselect();
            }

            invaderpanel.SetActive(false);
            towerpanel.SetActive(false);
            _selectedtyp = -1;
        }

        if (Input.GetMouseButton(0) && buildsystem.instance._buildTower==false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                GameObject hitGameObject = hit.collider.gameObject;

                if(hitGameObject.tag == "selectable")
                {
                    select(hitGameObject);

                    /*
                    if (hitGameObject.GetComponentInParent<invader>())
                    {
                        deselect();
                        _selectedobject = hitGameObject.transform.parent.gameObject;
                        invaderpanel.SetActive(true);
                        _selectedtyp = 0;
                    }
                    else if (hitGameObject.GetComponentInParent<tower>())
                    {
                        buildsystem.instance._buildTower = false;
                        deselect();
                        _selectedobject = hitGameObject.transform.parent.gameObject;
                        towerpanel.SetActive(true);
                        _selectedtyp = 1;
                    }
                     * */
                }

                //select();

                /*
                if (_selectedobject)
                {
                    Color newcolor = _selectedobject.GetComponent<SpriteRenderer>().color;
                    newcolor.a = 0.25f;
                    _selectedobject.GetComponent<SpriteRenderer>().color = newcolor;
                }
                 * */
            }
        }
        else if (Input.GetMouseButton(1) && buildsystem.instance._buildTower == false)
        {
            deselect();
            
            /*if (_selectedobject)
            {
                Color newcolor = _selectedobject.GetComponent<SpriteRenderer>().color;
                newcolor.a = 1.0f;
                _selectedobject.GetComponent<SpriteRenderer>().color = newcolor;
            }
            _selectedobject = null;
            _selectedtyp = -1;*/
        }

        switch (_selectedtyp)
        {
            case 0:
                invader invader = _selectedobject.GetComponent<invader>();

                Transform iconPanel = invaderpanel.transform.Find("iconPanel");

                Image icon = iconPanel.transform.Find("icon").gameObject.GetComponent<Image>();
                Slider healthSlider = iconPanel.transform.Find("healthslider").gameObject.GetComponent<Slider>();

                icon.sprite = invader.icon;
                healthSlider.GetComponent<healthbar>().healthObject = invader.health;
                healthSlider.GetComponent<healthbar>().reset();
                break;

            case 1:
                tower tower = _selectedobject.GetComponent<tower>();

                Text elementtext = towerpanel.transform.Find("elementtext").gameObject.GetComponent<Text>();
                Text damagetext = towerpanel.transform.Find("damagetext").gameObject.GetComponent<Text>();
                Text rangetext = towerpanel.transform.Find("rangetext").gameObject.GetComponent<Text>();
                Text attackspeedtext = towerpanel.transform.Find("attackspeedtext").gameObject.GetComponent<Text>();

                elementtext.text = tower.tag;
                damagetext.text = "Schaden: 0";
                rangetext.text = "Reichweite: 0";
                attackspeedtext.text = "Angriffsgeschwindigkeit: 0";
                break;

            default:
                break;
        }
	}

    private void select(GameObject toSelect)
    {
        if (toSelect.GetComponentInParent<invader>())
        {
            deselect();
            _selectedobject = toSelect.transform.parent.gameObject;
            invaderpanel.SetActive(true);
            _selectedtyp = 0;
        }
        else if (toSelect.GetComponentInParent<tower>())
        {
            buildsystem.instance._buildTower = false;
            deselect();
            _selectedobject = toSelect.transform.parent.gameObject;
            towerpanel.SetActive(true);
            _selectedtyp = 1;
        }

        if (_selectedobject)
        {
            Color newcolor = _selectedobject.GetComponent<SpriteRenderer>().color;
            newcolor.a = 0.25f;
            _selectedobject.GetComponent<SpriteRenderer>().color = newcolor;
        }
    }

    private void deselect()
    {
        if (_selectedobject)
        {
            Color newcolor = _selectedobject.GetComponent<SpriteRenderer>().color;
            newcolor.a = 1.0f;
            _selectedobject.GetComponent<SpriteRenderer>().color = newcolor;
         }

        switch (_selectedtyp)
        {
            case 0:
                invaderpanel.SetActive(false);
                break;
            case 1:
                towerpanel.SetActive(false);
                break;
            case 2:
                towerbuidpanel.SetActive(false);
                break;
        }

        ///////////////////////////////////////
        _selectedobject = null;
        _selectedtyp = -1;
    }
    public void swichtobuildmode()
    {
        deselect();
        _selectedobject = null;
        _selectedtyp = 2;

        towerbuidpanel.SetActive(true);
    }
}
