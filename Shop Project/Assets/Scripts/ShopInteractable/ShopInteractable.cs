using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ShopInteractable : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [Inject]
    private ShopScreen m_shopScreen;

    [SerializeField]
    private GameObject m_circleImage;
    [SerializeField]
    private GameObject m_instructionText;

    private bool m_canOpenScreen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == PLAYER_TAG)
        {
            m_circleImage.gameObject.SetActive(true);
            m_instructionText.gameObject.SetActive(true);
            m_canOpenScreen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.tag == PLAYER_TAG)
        {
            m_circleImage.gameObject.SetActive(false);
            m_instructionText.gameObject.SetActive(false);
            m_canOpenScreen = false;
        }
    }

    public void Interact(InputAction.CallbackContext value)
    {
        if (m_canOpenScreen)
        {
            m_shopScreen.OpenScreen();
        }
    }

}