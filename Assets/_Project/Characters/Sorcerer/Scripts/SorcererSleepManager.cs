using System.Collections;
using UnityEngine;

public class SorcererSleepManager : MonoBehaviour
{
    private bool _isSleeping = true;
    [SerializeField] private float _timeBeforeSleep = 10.0f;

    public bool IsSleeping => _isSleeping;

    private Coroutine _sleepCoroutine;

    public void WakeUp()
    {
        _isSleeping = false;
        StopSleepCountdownCoroutine();
    }

    public void FallAsleep()
    {
        _isSleeping = true;
    }

    private IEnumerator SleepCountdown()
    {
        yield return new WaitForSeconds(_timeBeforeSleep);
        _isSleeping = true;
        _sleepCoroutine = null;
    }

    public void StartSleepCountdownCoroutine()
    {
        if (_sleepCoroutine != null)
        {
            StopCoroutine(_sleepCoroutine);
        }
        _sleepCoroutine = StartCoroutine(SleepCountdown());
    }

    public void StopSleepCountdownCoroutine()
    {
        if (_sleepCoroutine != null)
        {
            StopCoroutine(_sleepCoroutine);
            _sleepCoroutine = null; 
        }
    }
}
