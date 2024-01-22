using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryScreen : UI
{
    [Inject]
    private IPersistence m_persistence;
    [Inject]
    IInstantiator m_instantiator;

    [SerializeField]
    private InventoryItem m_inventoryItem;
    [SerializeField]
    private Transform m_itemsContainer;

    private InventoryService m_inventoryService;

    private List<InventoryItem> m_inventoryItems = new List<InventoryItem>();

    [Inject]
    private void Init()
    {
        m_inventoryService = new InventoryService(m_persistence);
    }

    protected override void Refresh()
    {
        PopulateInventory();
    }

    private void PopulateInventory()
    {
        List<ScriptableItem> storedItems = m_inventoryService.GetStorageItens();
        DisableAllInventoryItens();
        CheckCurrentItems(storedItems);

        for (int i = 0; i < storedItems.Count; i++)
        {
            m_inventoryItems[i].Init(storedItems[i]);
            m_inventoryItems[i].SetEnabled(true);
        }
    }

    private void CheckCurrentItems(List<ScriptableItem> storedItems)
    {
        if (m_inventoryItems.Count < storedItems.Count)
        {
            InstantiateMissingItens(storedItems);
        }
    }

    private void InstantiateMissingItens(List<ScriptableItem> storedItems)
    {
        int missingItemsAmount = storedItems.Count - m_inventoryItems.Count;
        for (int i = 0; i < missingItemsAmount; i++)
        {
            InventoryItem inventoryItem = m_instantiator.InstantiatePrefabForComponent<InventoryItem>(m_inventoryItem, m_itemsContainer.transform);
            inventoryItem.SetEnabled(false);
            m_inventoryItems.Add(inventoryItem);
        }
    }

    public void DisableAllInventoryItens()
    {
        for (int i = 0; i < m_inventoryItems.Count; i++)
        {
            m_inventoryItems[i].SetEnabled(false);
        }
    }
}
