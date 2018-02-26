using UnityEngine;
using System.Collections;

[System.Serializable]
public class health
{
    [readonlyAttribute]
    public float maximumHealth;
    [readonlyAttribute]
    public float currentHealth;

    private resistance _resistance;

    public bool isDead()
    {
        if(currentHealth <= 0.0f)
        {
            return true;
        }

        return false;
    }

    public void reset()
    {
        currentHealth = maximumHealth;
    }

    public void takeDamage(attack attack)
    {
        float damage = attack.damage;

        switch (attack.element)
        {
            case "fire":
                damage = damage - ((damage * ((float)_resistance.againstFire / 100.0f)) - damage);
                break;
            case "air":
                damage = damage - ((damage * ((float)_resistance.againstAir / 100.0f)) - damage);
                break;
            case "earth":
                damage = damage - ((damage * ((float)_resistance.againstEarth / 100.0f)) - damage);
                break;
            case "water":
                damage = damage - ((damage * ((float)_resistance.againstWater / 100.0f)) - damage);
                break;
        }

        currentHealth -= damage;
    }

    public resistance resistance
    {
        set
        {
            _resistance = value;
        }
    }
}
