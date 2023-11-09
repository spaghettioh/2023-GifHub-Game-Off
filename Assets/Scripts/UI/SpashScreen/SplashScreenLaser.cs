using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenLaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _laserLine;

    [SerializeField]
    private LineRenderer _cutLine;

    [SerializeField]
    private Transform _origin;

    [SerializeField]
    private Transform _cutPoint;

    private IEnumerator Start()
    {
        var index = 0;
        var cutPointZ = 0f;
        var timer = 5f;
        while (_laserLine.enabled || timer > 0)
        {
            var pos = _cutPoint.position;
            Vector3 cut = new(pos.x, pos.y, cutPointZ);
            _laserLine.SetPosition(0, _origin.position);
            _laserLine.SetPosition(1, _cutPoint.position);

            _cutLine.positionCount = index + 1;
            _cutLine.SetPosition(index, cut);

            if (!_laserLine.enabled)
            {
                timer -= Time.deltaTime;
            }
            index++;
            cutPointZ -= 1f;
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update() { }
}
