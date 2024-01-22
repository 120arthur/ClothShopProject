using System.Collections.Generic;
using UnityEngine;
public class ShopService
{
    private IPersistence m_persistence;

    public ShopService(IPersistence persistence)
    {
        m_persistence = persistence;
    }

    public List<ScriptableItem> GetStoredScriptableItems()
    {
        return m_persistence.GetAllScriptableItems();
    }

    public void BuyScriptableItem(ScriptableItem scriptableItem)
    {
        if (m_persistence.CanSpendSoftCurrency(scriptableItem.ItemPrice))
        {
            m_persistence.RemoveSoftCurrency(scriptableItem.ItemPrice);
            m_persistence.AddScriptableItem(scriptableItem);
        }
    }

    public void SellScriptableItem(ScriptableItem scriptableItem)
    {
        if (m_persistence.HasScriptableItem(scriptableItem))
        {
            m_persistence.RemoveScriptableItem(scriptableItem);
            m_persistence.AddSofrCurrency(scriptableItem.ItemPrice - scriptableItem.Discount);
        }
    }

    public int GetCurrencyAmount()
    {
        return m_persistence.GetsoftCurrencyAmount();
    }
}
