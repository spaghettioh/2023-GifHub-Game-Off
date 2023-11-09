// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;

// public class UITester : MonoBehaviour
// {
//     public bool _update;
//     public AttackGearData _gear;
//     public int level = 1;
//     public GearQuality quality;
//     public UIPresenceDescriptionColorMapSO colorMap;
//     public VisualTreeAsset _detailsAsset;
//     public GearDetailsTemplateContainer _details;
//     public VisualTreeAsset _modDescriptionAsset;

//     private void OnValidate()
//     {
//         var root = GetComponent<UIDocument>().rootVisualElement;
//         root.Clear();
//         var template = _detailsAsset.CloneTree();
//         root.Add(template);
//         _details = new(template, _modDescriptionAsset, colorMap);
//         _details.SetGearDetails(_gear);
//         _update = false;
//     }
// }
