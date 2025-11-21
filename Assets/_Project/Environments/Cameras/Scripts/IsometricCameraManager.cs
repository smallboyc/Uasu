using Unity.Cinemachine;
using UnityEngine;

public class IsometricCameraManager : MonoBehaviour
{
    private static IsometricCameraManager _instance;
    [HideInInspector] public CinemachineCamera IsometricCamera;
    private float _initialCameraAngle = 35.0f;
    private float _lockCameraAngle = 45.0f;
    private float _currentCameraAngle;

    private float _initialOrthographicSize = 7.5f;
    private float _lockOrthographicSize = 7.0f;
    private float _currentOrthographicSize;

    public static IsometricCameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (IsometricCameraManager): No Instance found.");
            }
            return _instance;
        }
    }
    void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log("INFO (IsometricCameraManager): An instance has been deleted because of the Singleton Pattern");
            return;
        }
        _instance = this;

        DontDestroyOnLoad(gameObject);

        IsometricCamera = GetComponentInChildren<CinemachineCamera>();
    }

    private void Start()
    {
        _currentCameraAngle = _initialCameraAngle;
        _currentOrthographicSize = _initialOrthographicSize;
    }

    private void Update()
    {
        SmoothCameraRotation();
        SmoothCameraZoom();
    }

    public void ActiveCameraLockEffect()
    {
        _currentCameraAngle = _lockCameraAngle;
        _currentOrthographicSize = _lockOrthographicSize;
    }

    public void CancelCameraLockEffect()
    {
        _currentCameraAngle = _initialCameraAngle;
        _currentOrthographicSize = _initialOrthographicSize;
    }

    private void SmoothCameraRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(
            _currentCameraAngle,
            IsometricCamera.transform.eulerAngles.y,
            IsometricCamera.transform.eulerAngles.z
        );

        IsometricCamera.transform.rotation = Quaternion.Lerp(
            IsometricCamera.transform.rotation,
            targetRotation,
            5.0f * Time.deltaTime
        );
    }

    private void SmoothCameraZoom()
    {
        IsometricCamera.Lens.OrthographicSize = Mathf.Lerp(
          IsometricCamera.Lens.OrthographicSize,
          _currentOrthographicSize,
          5.0f * Time.deltaTime
      );
    }
}
