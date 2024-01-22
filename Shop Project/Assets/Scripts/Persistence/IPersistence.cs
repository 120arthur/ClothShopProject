using System.Collections.Generic;

public interface IPersistence
{
    void Save();
    void AddScriptableItem(ScriptableItem scriptableItem);
    bool HasScriptableItem(ScriptableItem scriptableItem);
    void RemoveScriptableItem(ScriptableItem scriptableItem);
    List<ScriptableItem> GetAllScriptableItems();
    bool CanSpendSoftCurrency(int amount);
    int GetsoftCurrencyAmount();
    void AddSofrCurrency(int value);
    void RemoveSoftCurrency(int value);
}