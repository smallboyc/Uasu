using System.Collections;
using UnityEngine;

public class SorcererSleepManager : MonoBehaviour
{
    private bool _isSleeping = true;
    [SerializeField] private float _timeBeforeSleep = 10.0f;
    public bool IsSleeping
    {
        get => _isSleeping;
    }
    public void WakeUp()
    {
        _isSleeping = false;
    }

    public void FallAsleep()
    {
        _isSleeping = true;
    }

    public void StartSleepCountdown()
    {
        StartCoroutine(WaitForNextSleep());
    }

    private IEnumerator WaitForNextSleep()
    {
        yield return new WaitForSeconds(_timeBeforeSleep);
        _isSleeping = true;
    }
}
