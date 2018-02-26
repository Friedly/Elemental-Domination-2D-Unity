using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class invader : MonoBehaviour 
{
    public static int invaderCount = 0;

    public Sprite icon;
    public Sprite elementIcon;
    public string element;

    [readonlyAttribute]
    public string killedBy = "unknown";

    public health health = new health();
    public modifications modifications;
    public resistance resistance = new resistance();
    public lifedamage lifedamage = new lifedamage();
    public movement movement = new movement();
    public elementalenergy elementalenergy = new elementalenergy();

    private SpriteRenderer _spriterenderer;

    //Gefällt mir nicht!
    private Slider _invadercountslider;
    //

    //Wird immer bei SetActive(true) aufgerufen
    public void Start()
    {
        //Gefällt mir nicht!
         GameObject slider = GameObject.Find("invadercountslider");
         _invadercountslider = slider.GetComponent<Slider>();
        //
         _spriterenderer = gameObject.GetComponent<SpriteRenderer>();

         health.resistance = resistance;

         modifications modifications = new modifications(this);
    }

    private void OnEnable()
    {
        invaderCount++;

        health.reset();
    }

    private void OnDisable()
    {
        invaderCount--;

        if (_invadercountslider != null)
        {
            _invadercountslider.value--;
        }

        if(killedBy == "tower")
        {
            switch(element)
            {
                case "fire":
                    gamemanager.instance.playermanager.resources.fireResources += elementalenergy.energy;
                    break;
                case "water":
                    gamemanager.instance.playermanager.resources.waterResources += elementalenergy.energy;
                    break;
                case "earth":
                    gamemanager.instance.playermanager.resources.earthResources += elementalenergy.energy;
                    break;
                case "air":
                    gamemanager.instance.playermanager.resources.airResources += elementalenergy.energy;
                    break;
            }

            gamemanager.instance.playermanager.updatePlayerUI();
        }
        else if(killedBy == "basis")
        {
            gamemanager.instance.playermanager.lives -= lifedamage.damage;
            gamemanager.instance.playermanager.updatePlayerUI();
        }

        killedBy = "unknown";
    }

    public void Update()
    {
        if (health.isDead())
        {
            killedBy = "tower";

            gameObject.SetActive(false);

            return;
        }

        movement.update(Time.deltaTime, transform);

        _spriterenderer.sortingOrder = -(int)(transform.position.y * 10.0f);
    }
}
