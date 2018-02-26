using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class invader : MonoBehaviour 
{
    public static int invaderCount = 0;

    public Sprite icon;
    public Sprite elementIcon;
    public string element;

    public health health = new health();
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
    }

    public void Update()
    {
        if (health.isDead())
        {
            gameObject.SetActive(false);

            return;
        }

        movement.update(Time.deltaTime, transform);

        _spriterenderer.sortingOrder = -(int)(transform.position.y * 10.0f);
    }
}
