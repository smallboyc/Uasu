using Unity.Cinemachine;
using UnityEngine;

public class IsometricCameraManager : MonoBehaviour
{
    private static IsometricCameraManager _instance;
    private CinemachineCamera _isometricCamera;
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
                Debug.LogError("No Instance found for PlayerInputManager.");
            }
            return _instance;
        }
    }

    public CinemachineCamera GetCamera
    {
        get
        {
            return _isometricCamera;
        }
    }

    void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _isometricCamera = GetComponent<CinemachineCamera>();
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
            _isometricCamera.transform.eulerAngles.y,
            _isometricCamera.transform.eulerAngles.z
        );

        _isometricCamera.transform.rotation = Quaternion.Lerp(
            _isometricCamera.transform.rotation,
            targetRotation,
            5.0f * Time.deltaTime
        );
    }

    private void SmoothCameraZoom()
    {
        _isometricCamera.Lens.OrthographicSize = Mathf.Lerp(
          _isometricCamera.Lens.OrthographicSize,
          _currentOrthographicSize,
          5.0f * Time.deltaTime
      );
    }
}
