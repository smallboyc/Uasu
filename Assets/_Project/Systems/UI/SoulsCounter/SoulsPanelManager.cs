using System.Collections;
using UnityEngine;

public class SoulsCounterPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject soulsCounterPanel;
    [SerializeField] private float visibleTime = 5f;

    private int lastSoulCount;
    private Coroutine hideCoroutine;

    void Start()
    {
        // Empezar oculto
        soulsCounterPanel.SetActive(false);

        // Guardamos el valor inicial
        lastSoulCount = PlayerManager.Instance.SoulCounter;
    }

    void Update()
    {
        int currentSouls = PlayerManager.Instance.SoulCounter;

        // Si el número de almas aumenta → el player ha impactado con un Soul
        if (currentSouls > lastSoulCount)
        {
            ShowPanel();
            lastSoulCount = currentSouls;
            Debug.Log("He comio alma");
        }
    }

    void ShowPanel()
    {
        soulsCounterPanel.SetActive(true);

        // Reiniciar el temporizador si ya estaba activo
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterSeconds());
    }

    IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSeconds(visibleTime);
        soulsCounterPanel.SetActive(false);
    }
}