using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class areaOfDamageEffect : MonoBehaviour, IEffect 
{
    public void fire(List<invader> invader, attack attack)
    {
        for (int index = 0; index < invader.Count; ++index)
        {
            if (invader[index].gameObject.activeInHierarchy)
                invader[index].health.takeDamage(attack);
            else
            {
                invader.RemoveAt(index);
                continue;
            }

            if (invader[index].health.isDead())
                invader.RemoveAt(index);
        }
    }
}
