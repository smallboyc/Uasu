using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerManager : CharacterManager
{
    private PlayerLocomotionManager _playerLocomotionManager;
    private PlayerAnimationManager _playerAnimationManager;

    protected override void Awake()
    {
        base.Awake();
        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        _playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    protected override void Update()
    {
        base.Update();
        _playerLocomotionManager.HandleAllMovement();
        _playerAnimationManager.HandlePlayerAnimations(_playerLocomotionManager);
    }
}