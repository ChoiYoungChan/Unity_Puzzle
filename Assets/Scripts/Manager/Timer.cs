using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Private Field

    private float _gameTime;
    [SerializeField] Slider _timer;
    float _time = 0;
    bool _isTimeUp = false;

    #endregion

    #region Properties

    public bool IsCount { get; set; }
    public Action TimeUpCallBack { get; set; }
    public float PlayTime { get { return _timer.value; } }
    #endregion

    #region Private Method
    void Update()
    {
        if (!IsCount) return;
        _time += Time.deltaTime;
        _timer.value = _gameTime - _time;
        if(_time >= _gameTime && !_isTimeUp)
        {
            Debug.Log("### TimeUp");
            TimeUpCallBack?.Invoke();
            _isTimeUp = true;
        }
    }

    #endregion

    #region Public Method
    public void Initialize()
    {
        IsCount = false;
        _time = 0;
        _isTimeUp = false;
        _timer.maxValue = _gameTime;
        _timer.value = 0;
    }

    #endregion
}
