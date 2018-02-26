using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class singleTargetDamageEffect : MonoBehaviour, IEffect
{
    //public GameObject singleTargetProjectile;

    public void fire(List<invader> invader, attack attack, Transform origin, GameObject appearance)
    {
        if (invader.Count >= 1)
        {
            if (invader[0].gameObject.activeInHierarchy)
            {
                GameObject newProjectile = Instantiate(appearance, new Vector2(origin.position.x, origin.position.y), Quaternion.identity) as GameObject;

                projectiles projectile = newProjectile.GetComponent<projectiles>();
                projectile.target = invader[0].transform;
                projectile.attack = attack; 
            }
        }
    }
}
