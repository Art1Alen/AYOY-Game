using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Image _fillBarProgress;
    private float _lastValue;
   
    void Update()
    {
        if (!GameManager.singleton.GameStarted)
            return;
        float traveledDistance = GameManager.singleton.EntireDistance - GameManager.singleton.DistanceLeft;
        float value = traveledDistance / GameManager.singleton.EntireDistance;

        if (GameManager.singleton.gameObject && value < _lastValue)
            return;

        _fillBarProgress.fillAmount = Mathf.Lerp(_fillBarProgress.fillAmount, value, 5 * Time.deltaTime);

        _lastValue = value;
    }
}
