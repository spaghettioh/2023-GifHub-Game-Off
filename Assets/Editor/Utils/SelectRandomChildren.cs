using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectRandomChildren : MonoBehaviour
{
    [SerializeField]
    private bool _selectRandomChildren;
    private void OnValidate()
    {
        if (!_selectRandomChildren)
        {
            return;
        }
        _selectRandomChildren = false;
        var kids = GetComponentsInChildren<Transform>().ToList();
        List<Transform> randomKids = new();
        kids.Randomize().ForEach(
            kid => {
                if (Util.CoinFlip)
                {
                    randomKids.Add(kid);
                }
            }
        );
        randomKids.SelectInHierarchy();
    }
}
