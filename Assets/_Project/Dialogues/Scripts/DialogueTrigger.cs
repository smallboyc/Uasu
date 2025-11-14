using UnityEngine;

// This script is attached to an NPC who can dialogue with the player.
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.CanInteract = false;
        DialogueManager.Instance.StartDialogue(GetCurrentDialogueID());
    }

    public bool PlayerCanDialogue(float playerRangeRadius = 4.0f, float playerRangeAngle = 50.0f)
    {
        return PlayerInRange(playerRangeRadius, playerRangeAngle) && DialogueIsNotRunning() && DialogueManager.Instance.CanInteract;
    }

    public bool DialogueIsNotRunning()
    {
        return !DialogueManager.Instance.DialogueIsRunning;
    }

    private bool PlayerInRange(float playerRangeRadius, float playerRangeAngle)
    {

        Collider[] players = Physics.OverlapSphere(transform.position, playerRangeRadius, _playerLayer);
        foreach (Collider player in players)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            return Vector3.Angle(transform.forward, dir) < playerRangeAngle;
        }
        return false;
    }

    // We want to let NPC choose their own dialogue schema.
    protected virtual int GetCurrentDialogueID()
    {
        return 0;
    }


}