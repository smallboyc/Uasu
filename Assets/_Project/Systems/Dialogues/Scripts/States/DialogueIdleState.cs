
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
        DialoguePanelManager.Instance.DialoguePanel.SetActive(false);
        DialoguePanelManager.Instance.DialogueBox.SetActive(false);
        DialoguePanelManager.Instance.ChoiceBox.SetActive(false);
        DialoguePanelManager.Instance.DialogueText.text = "";
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
        DialoguePanelManager.Instance.DialoguePanel.SetActive(true);
    }
}
