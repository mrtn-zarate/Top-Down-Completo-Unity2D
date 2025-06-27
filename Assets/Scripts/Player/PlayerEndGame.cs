using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject puerta;             // Asigna aqu� la puerta en el editor
    public GameObject mensajeFinalUI;     // UI con "Gracias por jugar"

    [SerializeField] private QuestSystem quest1;
    [SerializeField] private QuestSystem quest2;

    private bool gPressed = false;
    private bool hPressed = false;
    private bool triggered = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) gPressed = true;
        if (Input.GetKeyUp(KeyCode.G)) gPressed = false;

        if (Input.GetKeyDown(KeyCode.H)) hPressed = true;
        if (Input.GetKeyUp(KeyCode.H)) hPressed = false;

        // if (gPressed && hPressed && !triggered)
        if (quest1.IsPilarReady() && quest2.IsPilarReady() && !triggered)
        {
            triggered = true;
            StartCoroutine(ActivarFinalDelJuego());

            if (puerta != null)
            {
                puerta.SetActive(true);  // "Abre" la puerta desactiv�ndola
            }
        }
    }

    private IEnumerator ActivarFinalDelJuego()
    {

        yield return new WaitForSeconds(1f);

        if (mensajeFinalUI != null)
        {
            mensajeFinalUI.SetActive(true);  // Muestra el mensaje de "Gracias por jugar"
        }

        Time.timeScale = 0; // Pausa el juego
    }
}