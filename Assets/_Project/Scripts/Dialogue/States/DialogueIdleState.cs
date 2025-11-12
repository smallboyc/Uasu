
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
            _dialogueManager.CanInteract = false;

            // We pass to the next dialogue only if :
            // - We're not at the beginning/end of the dialogue list.
            // - We have all necessary achievements required by the next dialogue.
            if (!_dialogueManager.IsStartDialogue() && !_dialogueManager.IsEndDialogue() && _dialogueManager.PlayerCanAccessNextDialogue())
            {
                _dialogueManager.NextDialogue();
            }
            
            // Is the dialogue of "choice" or "passive" type?
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
