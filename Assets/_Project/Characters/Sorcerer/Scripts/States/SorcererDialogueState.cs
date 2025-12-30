using UnityEngine;

public class SorcererDialogueState : SorcererState
{
    public SorcererDialogueState(SorcererManager sorcererManager) : base(sorcererManager) { }

    public override void Enter()
    {
        SoundManager.Instance.PlaySoundClip(_sorcererManager.Interact, _sorcererManager.transform);
        UIManager.Instance.Show(PanelType.Dialogue);
        _sorcererManager.DialogueTrigger.TriggerDialogue();
    }

    public override void Update()
    {
        if (_sorcererManager.DialogueTrigger.DialogueIsNotRunning())
        {
            _sorcererManager.SorcererStateMachine.ChangeState(_sorcererManager.WakeUpState);
        }
    }

    public override void Exit()
    {
        UIManager.Instance.Hide(PanelType.Dialogue);
    }
}
