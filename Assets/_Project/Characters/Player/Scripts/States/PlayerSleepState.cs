
using UnityEngine;

public class PlayerSleepState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;
    public override void Enter()
    {
        IsometricCameraManager.Instance.ActiveCameraZoomEffect(2.0f);
        _playerManager.AnimationManager.PlaySleepAnimation();
        HelpManager.Instance.SetHelpText("INTERACT to Wake Up !");
        UIManager.Instance.Show(PanelType.Help);
    }

    public override void Update()
    {
        if (PlayerInputManager.Instance.InteractPressed && !UIManager.Instance.GamePaused)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        _playerManager.AnimationManager.StopSleepAnimation();
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(_playerManager.WakingUpSounds, _playerManager.transform);
        IsometricCameraManager.Instance.CancelCameraZoomEffect(2.5f);
        UIManager.Instance.Hide(PanelType.Help);
    }
}
