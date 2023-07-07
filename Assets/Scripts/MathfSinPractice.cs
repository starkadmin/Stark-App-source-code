using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathfSinPractice : MonoBehaviour
{

    private float _lerp;
    private Vector3 _originalPos;

    private void Start()
    {
        _lerp = 1;
        _originalPos = transform.position;
    }

    void Update()
    {
        CheckForInput();
        ExecuteLerpUsingSinWave();
    }

    private void ExecuteLerpUsingSinWave()
    {
        if (_lerp < 1)
        {
            _lerp = Mathf.Clamp(_lerp + Time.deltaTime, 0, 1);
            var addToYPos = Mathf.Sin(_lerp * Mathf.PI);
            transform.position = new Vector3(_originalPos.x, _originalPos.y + addToYPos, _originalPos.z);
        }
    }

    private void CheckForInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            TriggerLerp();
    }

    public void TriggerLerp()
    {
        _lerp = 0;
    }
}
