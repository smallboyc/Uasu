using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int Health = 3;
    public bool IsHurt;
    [SerializeField] GameObject SoulPrefab;

    public bool IsDead()
    {
        return Health <= 0;
    }
    public void Hurt()
    {
        IsHurt = true;
        Health--;
    }

    //Used as an Event in the Hurt Animation
    public void EndHurt()
    {
        // Debug.Log("ANIM = Trigger End Hurt");
        IsHurt = false;
    }

    public void GiveSoul()
    {
        if (SoulPrefab == null)
        {
            Debug.LogError("ERROR (EnemyHealthManager) : No Soul prefab found.");
            return;
        }
        StartCoroutine(GenerateSoul());
    }

    private IEnumerator GenerateSoul()
    {
        yield return new WaitForSeconds(1.2f);
        Vector3 soulSpawn = transform.position - transform.forward + transform.up * 0.2f;
        Instantiate(SoulPrefab, soulSpawn, Quaternion.identity);
    }
}
