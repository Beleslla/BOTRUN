using UnityEngine;
using TMPro;

public class ContadorDeTiempo : MonoBehaviour
{
    private float tiempoTranscurrido = 0;
    public TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI puntaje;

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        textoTiempo.text = tiempoTranscurrido.ToString("F0");
        float _puntaje = tiempoTranscurrido * 1000;
        puntaje.text = "Puntaje Final: " + _puntaje.ToString("F0");
    }
}