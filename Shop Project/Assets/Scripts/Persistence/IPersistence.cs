using System.Collections.Generic;

public interface IPersistence
{
    void AddItem(ScriptableItem scriptableItem);
    void AddSofrCurrency(int value);
    bool CanSpendValue(int amount);
    List<ScriptableItem> GetAllItens();
    int GetsoftCurrencyAmount();
    bool HasIten(ScriptableItem scriptableItem);
    void RemoveItem(ScriptableItem scriptableItem);
    void RemoveSoftCurrency(int value);
    void Save();
}