using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUseClothEvent : MonoBehaviour
{
   public ScriptableItem Item;
    public OnUseClothEvent(ScriptableItem scriptableItem)
    {
        Item = scriptableItem;
    }
}

public class OnRemoveClothEvent : MonoBehaviour
{
   public ScriptableItem Item;
    public OnRemoveClothEvent(ScriptableItem scriptableItem)
    {
        Item = scriptableItem;
    }
}
