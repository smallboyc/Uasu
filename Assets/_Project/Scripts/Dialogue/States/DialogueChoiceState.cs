
using UnityEngine;

// Choice state = Player has to make an action (choice between two options)
public class DialogueChoiceState : DialogueState
{
    public DialogueChoiceState(DialogueManager dialogueManager) : base(dialogueManager) { }
    public override void Enter()
    {
        Debug.Log("CHOICE (enter)");
        Debug.Log(_dialogueManager.CurrentDialogue.id);
        _dialogueManager.DialogueText.text = "";
        _dialogueManager.DisplayDialogueWithChoiceCoroutine();
    }

    public override void Update()
    {
        // If a button has been triggered by the player
        if (_dialogueManager.PlayerChose)
        {
            _dialogueManager.PlayerChose = false;

            //TODO : FOR THE CHOICE, we need to check the "playercanaccessnextdialogue" before clicking on the button.
            if (_dialogueManager.IsEndDialogue() || !_dialogueManager.PlayerCanAccessNextDialogue())
            {
                _dialogueManager.DialogueStateMachine.ChangeState(_dialogueManager.IdleState);
                return;
            }

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
