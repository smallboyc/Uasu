public abstract class DialogueState : State
{
    protected DialogueManager _dialogueManager;

    public DialogueState(DialogueManager dialogueManager)
    {
        _dialogueManager = dialogueManager;
    }
}