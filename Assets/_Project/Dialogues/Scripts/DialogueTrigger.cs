using UnityEngine;

// This script is attached to an NPC who can dialogue with the player.
public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(GetCurrentDialogueID());
    }

    public int GetCurrentDialogueID()
    {
        if (PlayerManager.Instance.HasAchievement("THE_SORCERER_FLOWER"))
        {
            return 24;
        }
        if (PlayerManager.Instance.HasAchievement("GOOD_LUCK_LITTLE_HERO"))
        {
            return 23;
        }
        return 18;
    }

    void Update()
    {
        // We don't want to start a dialogue if it is already running.
        if (DialogueManager.Instance.DialogueIsRunning)
            return;

        // If a dialogue is not running and the player interacts with a NPC => Run a new dialogue.
        if (PlayerInputManager.Instance.InteractPressed && DialogueManager.Instance.CanInteract)
        {
            DialogueManager.Instance.CanInteract = false;
            TriggerDialogue();
        }
    }
}