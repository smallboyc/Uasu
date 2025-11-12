
using UnityEngine;

// Passive State = Player has just to read and pass the dialogue.
public class DialoguePassiveState : DialogueState
{
    public DialoguePassiveState(DialogueManager dialogueManager) : base(dialogueManager) { }
    public override void Enter()
    {
        Debug.Log("PASSIVE (enter)");
        Debug.Log(_dialogueManager.CurrentDialogue.id);
        _dialogueManager.DialogueText.text = "";
        _dialogueManager.DisplayDialogueCoroutine();
    }

    public override void Update()
    {
        if (PlayerInputManager.Instance.InteractPressed && _dialogueManager.CanInteract)
        {
            _dialogueManager.CanInteract = false;

            if (_dialogueManager.IsEndDialogue() || !_dialogueManager.PlayerCanAccessNextDialogue())
            {
                _dialogueManager.DialogueStateMachine.ChangeState(_dialogueManager.IdleState);
                return;
            }

            _dialogueManager.NextDialogue();

            if (_dialogueManager.DialogueHasChoices())
                _dialogueManager.DialogueStateMachine.ChangeState(_dialogueManager.ChoiceState);
            else
                _dialogueManager.DialogueStateMachine.ChangeState(_dialogueManager.PassiveState);
        }
    }

    public override void Exit()
    {
    }
}
