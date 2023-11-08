using Nerdscape.Events;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Event_FX_NAME", menuName = "Events/FX/Visual effect event"
)]
public class VFXEventSO : EventBase<VFXType, Transform> { }
