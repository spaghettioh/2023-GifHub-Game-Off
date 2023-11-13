using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ClumpPropCollection",
    menuName = "Game Off/Clump prop collection"
)]
public class ClumpPropCollectionSO : ScriptableObject
{
    [SerializeField]
    private VoidEventSO _winEvent;
    [SerializeField]
    private List<PropHandler> _currentScenePropsCollected;
    [SerializeField]
    private List<PropData> _propsForWinScreen;

    private void OnEnable()
    {
        _winEvent.OnEventRaised += BuildWonPropData;
    }

    private void OnDisable()
    {
        _winEvent.OnEventRaised -= BuildWonPropData;
    }

    public void Reset()
    {
        _currentScenePropsCollected = new();
        _propsForWinScreen = new();
    }

    public void AddProp(PropHandler prop)
    {
        _currentScenePropsCollected.Add(prop);

        DisableOlderProps();
    }

    private void DisableOlderProps()
    {
        var count = _currentScenePropsCollected.Count;
        if (count > 100)
        {
            _currentScenePropsCollected[count - 100].gameObject
                .SetActive(false);
        }
    }

    public void RemoveProp(PropHandler prop)
    {
        _currentScenePropsCollected.Remove(prop);
    }

    // TODO - All 3 of these are only used to know
    // if PropManager can find an attaching
    // prop, so just move this and that check into that method
    public int Count => _currentScenePropsCollected.Count;
    public PropHandler Last => _currentScenePropsCollected.Last();
    public List<PropHandler> AttachingProps =>
        _currentScenePropsCollected.FindAll(p => p.IsAttaching);

    // Waits until the scene is won before trying to build out prop data
    // to avoid having to manage the list in each method
    private void BuildWonPropData()
    {
        // _currentScenePropsCollected.ForEach(
        //     prop => _propsForWinScreen.Add(
        //         new(prop.Sprite, prop.transform.localScale.x, prop.ScorePoints)
        //     )
        // );
    }

    public List<PropData> PropsWon => _propsForWinScreen;
}
