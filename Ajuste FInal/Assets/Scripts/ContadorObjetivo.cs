using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorObjetivo : MonoBehaviour
{
    [HideInInspector] public int indicador;
    private TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Start()
    {
        this.indicador = 0;
        this.texto = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this.texto.text = this.indicador.ToString();
    }
}
