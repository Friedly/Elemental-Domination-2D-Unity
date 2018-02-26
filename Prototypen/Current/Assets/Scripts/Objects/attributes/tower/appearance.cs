using UnityEngine;
using System.Collections;

[System.Serializable]
public class appearanceState
{
    public Sprite look;
    public int onLevel;
}

[System.Serializable]
public class appearance 
{
    [readonlyAttribute]
    public Sprite currentAppearance;
    public appearanceState[] appearanceStates;

    private SpriteRenderer _spriterenderer;

    public void initiate(SpriteRenderer spriterenderer)
    {
        _spriterenderer = spriterenderer;
    }

    public void updateAppearance(int level)
    {
        if(!_spriterenderer)
        {
            return;
        }

        foreach(appearanceState state in appearanceStates)
        {
            if (state.onLevel == level)
            {
                currentAppearance = state.look;
                _spriterenderer.sprite = currentAppearance;

                return;
            }
        }
    }
}
