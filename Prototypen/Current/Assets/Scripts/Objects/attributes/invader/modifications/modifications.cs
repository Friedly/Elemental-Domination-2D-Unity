using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class modifications
{
    //enum e_modificatins { Geist, Flug, Schneller, Leben, Reinkarnation, Unverwundbar, Verlangsamung};

    private List<modification> _currentModifikationlist;
    private invader _ownerInvader;
    //public List<IModification> Modifikationlist;
    //private Dictionary<int, IModification> Modifikationdicta;

    public modifications(invader ownerInvader)
    {
        _ownerInvader = ownerInvader;
        /*
        _listOfNotUsedModificatins = new List<int>();
        _listOfNotUsedModificatins.Add((int)e_modificatins.Geist);
        _listOfNotUsedModificatins.Add((int)e_modificatins.Flug);
        _listOfNotUsedModificatins.Add((int)e_modificatins.Schneller);
        _listOfNotUsedModificatins.Add((int)e_modificatins.Leben);
        _listOfNotUsedModificatins.Add((int)e_modificatins.Reinkarnation);
        _listOfNotUsedModificatins.Add((int)e_modificatins.Unverwundbar);
        _listOfNotUsedModificatins.Add((int)e_modificatins.Verlangsamung);*/
    }

    public void createmodifications(List<modification> currenlist)
    {
        _currentModifikationlist = currenlist;

        foreach (modification mod in _currentModifikationlist)
        {
            mod.aktivatemodification(_ownerInvader);
        }

    }
    public void resetmodifications()
    {
        foreach (modification mod in _currentModifikationlist)
        {
            mod.disablemodification(_ownerInvader);
        }
    }

}
