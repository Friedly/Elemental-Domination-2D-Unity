using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class buildsystem : singleton<buildsystem>
{
    public bool _buildTower = false;
    private GameObject _mouseOverObject = null;
    private string _towerTag;

    private Dictionary<string, GameObject> _towers = null;

    public GameObject panel;
    public GameObject buttonToInstantiate;

    void Start()
    {
        _towers = new Dictionary<string, GameObject>();

        Object[] objects = Resources.LoadAll("PrefabObjects/Elemental Towers");

        foreach (Object mapObject in objects)
        {
            _towers.Add(mapObject.name, (GameObject)mapObject);

            GameObject gameObject = (GameObject)mapObject;

            createTowerButton(gameObject.GetComponent<tower>().icon, gameObject.GetComponent<tower>().appearance.appearanceStates[0].look, gameObject.tag);
        }

        _mouseOverObject = new GameObject("buildIcon");
        _mouseOverObject.AddComponent<SpriteRenderer>();
        _mouseOverObject.SetActive(false);
    }

	void Update () 
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < -0.5f || mousePosition.y < -0.5f)
        {
            mousePosition.x = -2;
            mousePosition.y = -2;
        }

        int mousePositionX = (int)(mousePosition.x + 0.5f);
        int mousePositionY = (int)(mousePosition.y + 0.5f);

        mapData mapData = gamemanager.instance.mapData;
        mapGfx mapGfx = gamemanager.instance.mapGfx;

        if (mapData == null || mapGfx == null)
        {
            return;
        }

        int tileOnMousePosition = mapData.getTileOnLayer(mousePositionX, mousePositionY, "tower");

        if (_buildTower == true && tileOnMousePosition != -1 && tileOnMousePosition != 1)
        {
            _mouseOverObject.SetActive(true);
            _mouseOverObject.transform.position = new Vector2(mousePositionX, mousePositionY);

            if (Input.GetMouseButtonDown(0) && _towers.ContainsKey(_towerTag))
            {
                mapData.setTileOnLayer(mousePositionX, mousePositionY, "tower", 1);
                GameObject newTower = Instantiate(_towers[_towerTag], new Vector2(mousePositionX, mousePositionY), Quaternion.identity) as GameObject;
                mapGfx.addGameObject(newTower, "tower");

                switch (_towerTag)
                {
                    case "waterTower":
                        elementaryAffection.instance.waterLevel++;
                        break;
                    case "fireTower":
                        elementaryAffection.instance.fireLevel++;
                        break;
                    case "airTower":
                        elementaryAffection.instance.airLevel++;
                        break;
                    case "earthTower":
                        elementaryAffection.instance.earthLevel++;
                        break;
                }

                elementaryAffection.instance.updateOnChange();
            }
        }
        else
        {
            _mouseOverObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _buildTower = false;
        }
	}

    public void createTowerButton(Sprite towerIcon, Sprite  towerAppearance, string towerTag)
    {
        GameObject newButton = Instantiate(buttonToInstantiate) as GameObject;
        newButton.transform.SetParent(panel.transform, false);

        Image image = newButton.GetComponent<Image>();
        image.sprite = towerIcon;

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => clickedTowerButton(towerAppearance, towerTag));
    }

    public void clickedTowerButton(Sprite towerAppearance, string towerTag)
    {
        setMouseOverObject(towerAppearance);
        _towerTag = towerTag;
        _buildTower = true;
    }

    public void setMouseOverObject(Sprite towerAppearance)
    {
        SpriteRenderer spriteRenderer = _mouseOverObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = towerAppearance;
        spriteRenderer.sortingLayerName = "tower";

        Color color = spriteRenderer.color;
        color.a = 0.50f;
        spriteRenderer.color = color;
    }
}
