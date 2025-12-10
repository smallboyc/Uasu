using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public InventoryObject inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnTriggerEnter(Collider other)
   {
    var item = other.GetComponent<Item>();
    if (item)
    {
        inventory.AddItem(item.item, 1);
        Destroy(other.gameObject);
    }
}
private void OnApplicationQuit()
{
    inventory.Container.Clear();
}
}
