
using UnityEngine;

// Idle State = Dialogue is not active
public class DialogueIdleState : DialogueState
{
    public DialogueIdleState(DialogueManager dialogueManager) : base(dialogueManager) { }
    public override void Enter()
    {
        Debug.Log("IDLE (enter)");
        Debug.Log(_dialogueManager.CurrentDialogue.id);
        _dialogueManager.PlayerChose = false;
        _dialogueManager.DialoguePanel.SetActive(false);
        _dialogueManager.DialogueBox.SetActive(false);
        _dialogueManager.ChoiceBox.SetActive(false);
        _dialogueManager.DialogueText.text = "";
    }

    public override void Update()
    {
        if (PlayerInputManager.Instance.InteractPressed && _dialogueManager.CanInteract)
        {
            _dialogueManager.StartCoroutine(_dialogueManager.InteractionCooldown());

            if (_dialogueManager.IsEndDialogue())
            {
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
        _dialogueManager.DialoguePanel.SetActive(true);
    }
}
