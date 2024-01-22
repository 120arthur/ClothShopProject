using UnityEngine;
public enum ItemType
{
    HAT,
    SHIRT,
    PANTS,
    SHOES,
    BODY
}

[CreateAssetMenu(fileName = "New scriptable item", menuName = "scriptable item")]
public class ScriptableItem : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public int ItemPrice;
    public int Discount;
    public ItemType ItemType;
    public RuntimeAnimatorController ItemControllerAnimator;
}