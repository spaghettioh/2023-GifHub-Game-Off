using UnityEngine;

public class MapCameraManager : MonoBehaviour
{
    // [SerializeField]
    // private BoxCollider _boundary;
    //
    // [SerializeField]
    // private MapBoundaryProcedure _mapBoundaryProcedure;
    //
    // [SerializeField]
    // private MapMenuCameraController _mapMenuCameraController;
    //
    // [SerializeField]
    // private MiniMapCameraHandler _miniMapCameraHandler;
    //
    // [SerializeField]
    // private ClumpRuntimeDataSO _runtimeData;
    //
    // [Header("Listening for...")]
    // [SerializeField]
    // private IntEventSO _stageGenerated;
    //
    // private void OnEnable()
    // {
    //     _stageGenerated.OnEventRaised += HandleStageGenerated;
    //     _mapMenuCameraController.OnManagerEnabled();
    // }
    //
    // private void OnDisable()
    // {
    //     _stageGenerated.OnEventRaised -= HandleStageGenerated;
    //     _mapMenuCameraController.OnManagerDisabled();
    // }
    //
    // private void LateUpdate()
    // {
    //     if (_runtimeData.Transform.IsNull())
    //     {
    //         return;
    //     }
    //
    //     if (_mapMenuCameraController.IsMenuOpen)
    //     {
    //         _mapMenuCameraController.UpdateCameraPosition();
    //         return;
    //     }
    //
    //     _miniMapCameraHandler.UpdateCameraPosition(_runtimeData.Position);
    // }
    //
    // private void HandleStageGenerated(int _)
    // {
    //     GenerateBoundary();
    //     var b = _boundary.bounds;
    //     _mapMenuCameraController.Initialize(b.min, b.max, b.center.y);
    //     // this.SelectInHierarchy(true);
    // }
    //
    // private void GenerateBoundary()
    // {
    //     // RoomComponent[] rooms = FindObjectsOfType<RoomComponent>();
    //     //
    //     // if (rooms.Length == 0)
    //     // {
    //     //     return;
    //     // }
    //     //
    //     // IEnumerable<Bounds> bounds = rooms.Select(r => r.Collider.bounds);
    //     // _mapBoundaryProcedure.GenerateBoundary(_boundary, bounds);
    // }
}
