using UnityEngine;
using System.Collections.Generic;
using System.Collections;
  
public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;
    // reemplazado Dictionary por lista para manejar 1 objeto por unidad
    List<GameObject> itemsDisplayed = new List<GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        updateDisplay();
    }

    // destruye lo mostrado actualmente
    void ClearDisplay()
    {
        for (int i = 0; i < itemsDisplayed.Count; i++)
        {
            if (itemsDisplayed[i] != null)
                Destroy(itemsDisplayed[i]);
        }
        itemsDisplayed.Clear();
    }

    public void CreateDisplay()
    {
        ClearDisplay();
        if (inventory == null || inventory.Container == null) return;

        int index = 0; // índice plano: una ranura por unidad
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var slot = inventory.Container[i];
            if (slot == null || slot.item == null || slot.item.prefab == null) continue;

            // por cada unidad en el slot, crear un objeto separado
            for (int a = 0; a < slot.amount; a++)
            {
                var obj = Instantiate(slot.item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = getSlotPosition(index);
                itemsDisplayed.Add(obj);
                index++;
            }
        }
    }

    public void updateDisplay()
    {
        if (inventory == null || inventory.Container == null) return;

        // calcular total de unidades actual
        int totalUnits = 0;
        for (int i = 0; i < inventory.Container.Count; i++)
            totalUnits += inventory.Container[i] != null ? inventory.Container[i].amount : 0;

        // si el número de objetos mostrados no coincide, reconstruir la vista
        if (totalUnits != itemsDisplayed.Count)
            CreateDisplay();
    }

    public Vector3 getSlotPosition(int i){
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),
                           Y_START + (-Y_SPACE_BETWEEN_ITEM) * (i / NUMBER_OF_COLUMN),
                           0f);
    }
}
