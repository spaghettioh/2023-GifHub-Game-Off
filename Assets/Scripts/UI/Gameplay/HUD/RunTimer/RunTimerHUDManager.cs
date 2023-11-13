using UnityEngine;

public class RunTimerHUDManager : HUDManager
{
    // TODO this should pull from InvData
    // listen for any menu, pause, gear swap, etc events
    // stop timer counting when any UI is open
    [SerializeField]
    private HUDCounter _minutes;

    [SerializeField]
    private HUDCounter _seconds;
    [Header("Listening for...")]
    private bool _timerIsRunning;
    private float _timeElapsed;

    private void Start()
    {
        SetUpCounter(_minutes);
        SetUpCounter(_seconds);
        _timerIsRunning = true;
    }

    private void Update()
    {
        if (_timerIsRunning)
        {
            _timeElapsed += Time.deltaTime;
            SetTimer();
        }
    }

    private void SetTimer()
    {
        float minutes = Mathf.FloorToInt(_timeElapsed / 60);
        float seconds = Mathf.FloorToInt(_timeElapsed % 60);
        _minutes.SetValue(minutes);
        _seconds.SetValue(seconds);
    }
}
