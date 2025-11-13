
using UnityEngine;

// Choice state = Player has to make an action (choice between two options)
// /!\ A choice is an inbetween state => We can't start or end with a choice.
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
            // Choice => Passive state
            _dialogueManager.DialogueStateMachine.ChangeState(_dialogueManager.PassiveState);
        }
    }

    public override void Exit()
    {
    }
}
