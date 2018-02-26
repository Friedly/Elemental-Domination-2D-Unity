using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class singleTargetDamageEffect : MonoBehaviour, IEffect
{
    public void fire(List<invader> invader, attack attack)
    {
        if (invader.Count >= 1)
        {
            if (invader[0].gameObject.activeInHierarchy)
                invader[0].health.takeDamage(attack);

            if (invader[0].health.isDead())
            {
                invader.RemoveAt(0);
            }
        }
    }
}
