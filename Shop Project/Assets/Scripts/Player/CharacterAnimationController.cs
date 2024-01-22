using UnityEngine;

/// <summary>
///  Manages player animations for both body and clothing. Associated with each clothing piece.
/// </summary>
public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private ItemType m_itemType;

    public void SetAnimationTrigger(string triggerName)
    {
        if (m_animator != null && m_animator.runtimeAnimatorController != null)
        {
            m_animator.SetTrigger(triggerName);
        }
    }

    public void FlipSprite(bool flipX)
    {
        if (m_spriteRenderer != null)
        {
            m_spriteRenderer.flipX = flipX;
        }
    }

    public void ChangeAnimatorController(RuntimeAnimatorController animatorController, ItemType itemType)
    {
        if (m_itemType.Equals(itemType))
        {
            m_animator.runtimeAnimatorController = animatorController;
        }
    }

    public void RemoveAnimatorController(ItemType itemType)
    {
        if (m_itemType.Equals(itemType))
        {
            m_animator.runtimeAnimatorController = null;
        }
    }
}
