// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.VFX;
//
// public class VFXHandler : MonoBehaviour
// {
//     public event UnityAction<VFXHandler> OnHandlerFinished;
//
//     [Header("Broadcasting to...")]
//     [SerializeField]
//     private AudioEventSO _audioEvent;
//
//     private Dictionary<VFXType, VFXComponentBase> _fxObjects = new();
//     private VaultKey _statusEffectAudioKey = VaultKey.Invalid;
//     private Transform _t;
//
//     private void Awake()
//     {
//         TryGetComponent(out _t);
//         VFXComponentBase[] effects =
//             GetComponentsInChildren<VFXComponentBase>(true);
//         _fxObjects = effects.ToDictionary(vfx => vfx.Type, vfx => vfx);
//     }
//
//     public void DoVFX(VFXType vfxType, Transform followTarget)
//     {
//         var component = _fxObjects[vfxType];
//         name = followTarget.name + vfxType.ToString().GetNameTag();
//         component.gameObject.SetActive(true);
//
//         _audioEvent.RaisePlayback(component.SoundEffect, _t.position, name);
//         component.OnVFXFinished += HandleVFXFinished;
//         StartCoroutine(component.VisualEffectRoutine(followTarget));
//     }
//
//     private void HandleVFXFinished(VFXComponentBase vfx)
//     {
//         vfx.OnVFXFinished -= HandleVFXFinished;
//         StartCoroutine(WrapVFXRoutine(vfx.VisualEffect));
//     }
//     
//     private IEnumerator WrapVFXRoutine(VisualEffect vfx)
//     {
//         vfx.Stop();
//         while (vfx.HasAnySystemAwake())
//         {
//             yield return null;
//         }
//
//         vfx.Reinit();
//
//         if (_statusEffectAudioKey != VaultKey.Invalid)
//         {
//             _audioEvent.RaiseStopPlayback(_statusEffectAudioKey, name);
//         }
//
//         vfx.gameObject.SetActive(false);
//         OnHandlerFinished?.Invoke(this);
//     }
// }
