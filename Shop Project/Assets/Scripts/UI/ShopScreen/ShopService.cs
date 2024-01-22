using System.Collections.Generic;
using UnityEngine;
public class ShopService
{
    private IPersistence m_persistence;

    public ShopService(IPersistence persistence)
    {
        m_persistence = persistence;
    }

    public List<ScriptableItem> GetStorageItens()
    {
        return m_persistence.GetAllItens();
    }

    public void BuyIten(ScriptableItem itemType)
    {
        if (m_persistence.CanSpendValue(itemType.ItemPrice))
        {
            m_persistence.RemoveSoftCurrency(itemType.ItemPrice);
            m_persistence.AddItem(itemType);
        }

        Debug.Log("OpenPopUp");
    }

    public void SellIten(ScriptableItem itemType)
    {
        if (m_persistence.HasIten(itemType))
        {
            m_persistence.RemoveItem(itemType);
            m_persistence.AddSofrCurrency(itemType.ItemPrice - itemType.Discount);
        }
    }

    public int GetCurrencyAmount()
    {
        return m_persistence.GetsoftCurrencyAmount();
    }
}
