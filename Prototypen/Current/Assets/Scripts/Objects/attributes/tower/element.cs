using UnityEngine;
using System.Collections;

[System.Serializable]
public class element
{
    public elementalGem primaryGem;
    public elementalGem secondaryGem;

    [readonlyAttribute]
    public elementalCombination combination;

    public void updateCombination()
    {
		string combinationName = "";

		if(primaryGem != null)
		{
			combinationName = primaryGem.gemName;
		}

        if (secondaryGem != null)
		{
            combinationName = primaryGem.gemName + secondaryGem.gemName;
		}

		combination = Resources.Load("ScriptableObjects/Elemental Combinations/" + combinationName + "Combination") as elementalCombination;
    }
}
