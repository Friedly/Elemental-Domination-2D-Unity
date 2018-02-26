using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEffect
{
    void fire(List<invader> invader, attack attack);
}
