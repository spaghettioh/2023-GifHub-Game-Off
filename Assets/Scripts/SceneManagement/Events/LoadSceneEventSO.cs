using Nerdscape.Events;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Event_LoadScene", menuName = "Events/Load scene event"
)]
public class LoadSceneEventSO : EventBase<SceneReferenceSO, bool> { }
