using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private ItemType m_itemType;

    public void SetAnimationTrigger(string TriggerName)
    {
        if (m_animator != null)
        {
            m_animator.SetTrigger(TriggerName);
        }
    }

    public void FlipSprite(bool flipX)
    {
        if (m_spriteRenderer != null)
        {
            m_spriteRenderer.flipX = flipX;
        }
    }

    public void ChangeAnimator(RuntimeAnimatorController controller, ItemType itemType)
    {
        if (m_itemType.Equals(itemType))
        {
            m_animator.runtimeAnimatorController = controller;
        }
    }

    public void RemoveAnimator(ItemType itemType)
    {
        if (m_itemType.Equals(itemType))
        {
            m_animator.runtimeAnimatorController = null;
        }
    }
}
