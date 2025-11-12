
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
        if (_dialogueManager.PlayerChose)
        {
            _dialogueManager.PlayerChose = false;

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
