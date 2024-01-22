using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryPersistence : IPersistence
{
    private string m_filePath;

    private InventoryData m_data;

    public InventoryPersistence()
    {
        m_filePath = Path.Combine(Application.persistentDataPath, "InventoryData.json");
        LoadPesistence();
    }

    public void Save()
    {
            string jsonData = JsonUtility.ToJson(m_data);
            File.WriteAllText(m_filePath, jsonData);

            Debug.Log("SaveGame");
    }

    private void LoadPesistence()
    {
        if (File.Exists(m_filePath))
        {
            string jsonData = File.ReadAllText(m_filePath);
            m_data = JsonUtility.FromJson<InventoryData>(jsonData);
        }
        else
        {
            Debug.LogWarning("Data file not found.");
            m_data = new InventoryData
            {
                ScriptableItems = new List<ScriptableItem>(),
                SoftCurencyAmount = 1000
            };
        }
    }

    public void RemoveItem(ScriptableItem scriptableItem)
    {
        for (int i = 0; i < m_data.ScriptableItems.Count; i++)
        {
            if (m_data.ScriptableItems[i] == scriptableItem)
            {
                m_data.ScriptableItems.RemoveAt(i);
            }
        }
        Save();
    }

    public bool HasIten(ScriptableItem scriptableItem)
    {
        return m_data.ScriptableItems.Contains(scriptableItem);
    }

    public void AddItem(ScriptableItem scriptableItem)
    {
        m_data.ScriptableItems.Add(scriptableItem);
        Save();
    }

    public List<ScriptableItem> GetAllItens()
    {
        return m_data.ScriptableItems;
    }

    public int GetsoftCurrencyAmount()
    {
        return m_data.SoftCurencyAmount;
    }

    public bool CanSpendValue(int amount)
    {
        if (amount < 0 || (m_data.SoftCurencyAmount - amount) < 0)
        {
            return false;
        }
        return true;
    }

    public void RemoveSoftCurrency(int value)
    {
        if (value < 0 || (m_data.SoftCurencyAmount - value) < 0)
        {
            Debug.Log("There is not enough money: " + value);
            return;
        }

        m_data.SoftCurencyAmount -= value;
        Save();
    }

    public void AddSofrCurrency(int value)
    {
        m_data.SoftCurencyAmount += value;
        Save();
    }
}

[Serializable]
public class InventoryData
{
    public List<ScriptableItem> ScriptableItems;
    public int SoftCurencyAmount;
}