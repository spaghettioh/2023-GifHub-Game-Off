using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    [SerializeField]
    private ClumpRuntimeDataSO _clumpData;

    [SerializeField]
    private ClumpPropCollectionSO _propCollection;

    [SerializeField]
    private SoundEffectAudioDataSO _defaultCollectSound;

    [Header("Crashing")]
    [SerializeField]
    private float _crashShakeDuration;

    [SerializeField]
    private SoundEffectAudioDataSO _crashSoundSmall;

    [SerializeField]
    private SoundEffectAudioDataSO _crashSoundLarge;

    [Header("Listening to...")]
    [SerializeField]
    private PropCollectEventSO _propCollectEvent;

    [Header("Broadcasting to...")]
    [SerializeField]
    private AudioEventSO _audioEvent;

    [SerializeField]
    private VoidEventSO _crashEvent;

    [Header("Editor")]
    [SerializeField]
    private bool _forceSpriteOrientation;

    [Header("DEBUG ==========")]
    [SerializeField]
    private float _clumpRadiusIncrement;
    [SerializeField]
    private float _clumpForceIncrement;
    [SerializeField]
    private List<PropHandler> _props;
    [SerializeField]
    private List<PropHandler> _collectableProps;

    private void OnEnable()
    {
        _clumpData.OnPropCountChanged += AdjustPropsCollectable;
        _propCollectEvent.OnEventRaised += CollectProp;
        // PropCollisionSystem.OnCollision += ProcessCollision;
    }

    private void OnDisable()
    {
        _clumpData.OnPropCountChanged -= AdjustPropsCollectable;
        _propCollectEvent.OnEventRaised -= CollectProp;
        // PropCollisionSystem.OnCollision -= ProcessCollision;
    }

    private void Start()
    {
        _propCollection.Reset();
        BuildPropsList();
        SetClumpIncrements();
        AdjustPropsCollectable();
    }

    private void ProcessCollision(PropHandler prop)
    {
        if (prop.IsCollectable)
        {
            CollectProp(prop);
        }
        else
        {
            CrashIntoProp(prop);
        }
    }

    private void BuildPropsList()
    {
        _props = new(GetComponentsInChildren<PropHandler>());
    }

    private void SetClumpIncrements()
    {
        var minCollider = _clumpData.MinColliderRadius;
        var maxCollider = _clumpData.MaxColliderRadius;
        _clumpRadiusIncrement = (maxCollider - minCollider) / _props.Count();

        var minForce = _clumpData.MinMoveForce;
        var maxForce = _clumpData.MaxMoveForce;
        _clumpForceIncrement = (maxForce - minForce) / _props.Count();
    }

    private void CollectProp(PropHandler p)
    {
        _audioEvent.RaisePlayback(_defaultCollectSound);
        _propCollection.AddProp(p);
        _collectableProps.Remove(p);
        p.SetCollected(_audioEvent);
        _clumpData.IncreaseSize(_clumpRadiusIncrement, _clumpForceIncrement);
    }

    private void CrashIntoProp(PropHandler p)
    {
        if (_clumpData.CurrentSpeed >= _clumpData.MaxSpeed / 2)
        {
            _audioEvent.RaisePlayback(_crashSoundLarge);
            _crashEvent.Raise(name);
            // p.ShakeGraphic(_crashShakeDuration, true);

            // TODO make this query return all attaching or just the last one
            if (_propCollection.Count > 0)
            {
                List<PropHandler> attaching = _propCollection.AttachingProps;
                if (attaching.Count > 0)
                {
                    attaching.ForEach(p => UncollectProp(p));
                }
                else
                {
                    UncollectProp(_propCollection.Last);
                }
            }
        }
        else
        {
            _audioEvent.RaisePlayback(_crashSoundSmall);
            // p.ShakeGraphic(_crashShakeDuration, false);
        }
    }

    private void UncollectProp(PropHandler p)
    {
        _propCollection.RemoveProp(p);
        _collectableProps.Add(p);
        _clumpData.DecreaseSize(_clumpRadiusIncrement, _clumpForceIncrement);
        p.DetachFromClump();
    }

    private void AdjustPropsCollectable(int count = 0)
    {
        // Set props collectable as count increases
        // _props.FindAll(p => p.SizeToCollect <= _clumpData.CollectedCount)
        //     .ForEach(
        //         p => {
        //             _props.Remove(p);
        //             _collectableProps.Add(p);
        //             p.ToggleCollectable(true);
        //         }
        //     );
        // // Set props not collectable as count decreases
        // _collectableProps.FindAll(
        //     p => p.SizeToCollect > _clumpData.CollectedCount
        // ).ForEach(
        //     p => {
        //         _collectableProps.Remove(p);
        //         _props.Add(p);
        //         p.ToggleCollectable(false);
        //     }
        // );
    }

    private void OnValidate()
    {
        BuildPropsList();

        if (_forceSpriteOrientation)
        {
            _props.ForEach(
                p => {
                    p.GetComponentInChildren<Billboard>().OrientNow();
                    _forceSpriteOrientation = false;
                }
            );
        }
    }
}
