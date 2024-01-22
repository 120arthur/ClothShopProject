using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour, IUI
{
    [SerializeField]
    private Button m_exit;

    private void Start()
    {
        m_exit.onClick.AddListener(CloseScreen);
    }

    public void OpenScreen()
    {
        gameObject.SetActive(true);
        Refresh();
    }

    public void CloseScreen()
    {
        gameObject.SetActive(false);
    }

    protected abstract void Refresh();

}
