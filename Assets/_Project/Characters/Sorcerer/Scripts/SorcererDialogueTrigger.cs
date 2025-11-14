using UnityEngine;

// This script is attached to an NPC who can dialogue with the player.
public class SorcererDialogueTrigger : DialogueTrigger
{
    protected override int GetCurrentDialogueID()
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

}