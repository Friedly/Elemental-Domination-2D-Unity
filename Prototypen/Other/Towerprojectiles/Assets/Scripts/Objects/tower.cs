using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class towerState
{
	public Sprite state;
	public int onLevel;
}

public class tower : MonoBehaviour 
{
    //public string towerName;
    public Sprite icon;
    public appearance appearance = new appearance();

    public level level = new level();
    public element element = new element();
    public attack attack = new attack();
    public attackspeed attackspeed = new attackspeed();
    public range range = new range();
    public costs costs = new costs();

    private BoxCollider2D _rangeBox;
    private List<invader> _objectsInRange = new List<invader>();

    [readonlyAttribute]
    public invader target;

    public void Start()
    {
        appearance.initiate(gameObject.GetComponent<SpriteRenderer>());
        appearance.updateAppearance(level.rank);

        element.updateCombination();

        attack.damage = element.combination.damage;
        attack.element = element.combination.element;

        range.tileRange = element.combination.range;
        range.correctTileRange();

        _rangeBox = GetComponent<BoxCollider2D>();

        if (_rangeBox)
        {
            _rangeBox.size = new Vector2((float)range.correctedTileRange - 0.1f, (float)range.correctedTileRange - 0.1f);
        }

        attackspeed.speed = element.combination.attackspeed;
    }

	public void Update()
	{
        if (attackspeed.isReadyToAttack(Time.deltaTime))
        {

            if (_objectsInRange.Count >= 1)
            {

                IEffect effect = element.combination.effect.GetComponent<IEffect>();

                if (effect != null)
                    effect.fire(_objectsInRange, attack);

                attackspeed.reset();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        invader invader = other.gameObject.GetComponent<invader>();

        if (invader)
        {
            _objectsInRange.Add(invader);

            if (!target)
            {
                target = invader;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        invader invader = other.gameObject.GetComponent<invader>();

        if (invader)
        {
            for(int index = 0; index < _objectsInRange.Count; ++index)
            {
                if (_objectsInRange[index] == invader)
                {
                    _objectsInRange.RemoveAt(index);

                    if (target == invader && _objectsInRange.Count >= 1)
                    {
                        target = _objectsInRange[0];
                    }
                    else
                    {
                        target = null;
                    }
                }
            }
        }
    }
}
