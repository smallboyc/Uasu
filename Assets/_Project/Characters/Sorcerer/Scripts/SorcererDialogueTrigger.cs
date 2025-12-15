using UnityEngine;

// This script is attached to an NPC who can dialogue with the player.
public class SorcererDialogueTrigger : DialogueTrigger
{
    protected override int GetCurrentDialogueID()
    {
        if (PlayerManager.Instance.HasAchievement("WHAT_IS_CALLING_ME_?"))
        {
            return 69;
        }
        if (PlayerManager.Instance.HasAchievement("THE_SORCERER_FLOWER"))
        {
            return 49;
        }
        if (PlayerManager.Instance.HasAchievement("GOOD_LUCK_LITTLE_HERO"))
        {
        
            return 48;
           
        }
       
        return 0;
    }
}