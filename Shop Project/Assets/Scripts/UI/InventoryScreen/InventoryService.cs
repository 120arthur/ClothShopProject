using System.Collections.Generic;

public class InventoryService
{
    private IPersistence m_persistence;

   public InventoryService(IPersistence persistence)
    {
        m_persistence = persistence;
    }

    public List<ScriptableItem> GetStorageItens()
    {
        return m_persistence.GetAllScriptableItems();
    }
}