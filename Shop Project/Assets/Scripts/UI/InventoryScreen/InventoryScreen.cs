using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryScreen : UI
{
    [SerializeField]
    private InventoryItem m_inventoryItem;

    [Inject]
    private IPersistence m_persistence;
    [Inject]
    IInstantiator m_instantiator;

    private InventoryService m_inventoryService;

    private List<InventoryItem> m_inventoryItems = new List<InventoryItem>();

    [SerializeField]
    private Transform m_itemsContainer;

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

        if (m_inventoryItems.Count < storedItems.Count)
        {
            int missingItemsAmount = storedItems.Count - m_inventoryItems.Count;
            for (int i = 0; i < missingItemsAmount; i++)
            {
                InventoryItem inventoryItem = m_instantiator.InstantiatePrefabForComponent<InventoryItem>(m_inventoryItem, m_itemsContainer.transform);
                inventoryItem.SetEnabled(false);
                m_inventoryItems.Add(inventoryItem);
            }
        }

        for (int i = 0; i < storedItems.Count; i++)
        {
            if (i > m_inventoryItems.Count)
            {
                return;
            }

            m_inventoryItems[i].Init(storedItems[i]);
            m_inventoryItems[i].SetEnabled(true);
        }
    }
}
