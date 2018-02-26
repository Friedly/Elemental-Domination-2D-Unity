using UnityEngine;
using System.Collections;

[System.Serializable]
public class resources
{
    public int earthResources;
    public int airResources;
    public int waterResources;
    public int fireResources;

    public bool possibleToBuy(resources availableResources)
    {
        if (   availableResources.airResources < airResources
            || availableResources.earthResources < earthResources
            || availableResources.waterResources < waterResources
            || availableResources.fireResources < fireResources)
        {
            return false;
        }

        return true;
    }

    public void reduceResources(resources availableResources)
    {
        availableResources.airResources -= airResources;
        availableResources.earthResources -= earthResources;
        availableResources.fireResources -= fireResources;
        availableResources.waterResources -= waterResources;
    }
}
