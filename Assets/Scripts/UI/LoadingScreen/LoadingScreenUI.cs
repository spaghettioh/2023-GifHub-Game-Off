using UnityEngine;

public class LoadingScreenUI : UI
{
    [SerializeField]
    private BoolEventSO _loadingScreenEvent;

    [SerializeField]
    private GameObject _loadingCamera;

    private void OnEnable()
    {
        _loadingScreenEvent.OnEventRaised += HandleLoadingScreenEvent;
    }

    private void OnDisable()
    {
        _loadingScreenEvent.OnEventRaised -= HandleLoadingScreenEvent;
    }

    protected override void OnUIStart()
    {
        _loadingCamera.SetActive(false);
    }

    private void HandleLoadingScreenEvent(bool isActive)
    {
        _loadingCamera.SetActive(isActive);
        if (isActive)
        {
            ShowUI();
            return;
        }

        HideUI();
    }
}
