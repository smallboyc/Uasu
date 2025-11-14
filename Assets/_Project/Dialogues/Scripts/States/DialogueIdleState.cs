
using UnityEngine;

// Idle State = Dialogue is not active
public class DialogueIdleState : State
{
    private DialogueManager _dialogueManager = DialogueManager.Instance;
    public override void Enter()
    {
        Debug.Log("IDLE (enter)");
        Debug.Log(_dialogueManager.CurrentDialogue.id);
        _dialogueManager.CanInteractCooldownCoroutine();
        _dialogueManager.DialogueIsRunning = false; //End of dialogue means => We go back to the Idle State.
        _dialogueManager.PlayerChose = false;
        _dialogueManager.DialoguePanel.SetActive(false);
        _dialogueManager.DialogueBox.SetActive(false);
        _dialogueManager.ChoiceBox.SetActive(false);
        _dialogueManager.DialogueText.text = "";
    }

    public override void Update()
    {
        if (_dialogueManager.DialogueIsRunning)
        {
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
