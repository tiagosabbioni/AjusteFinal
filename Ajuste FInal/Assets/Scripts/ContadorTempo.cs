using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.Experimental.Rendering.Universal;

public class ContadorTempo : MonoBehaviour
{
    public float tempoSegundos;
    private TextMeshProUGUI campoTexto;
    private int indicMin;
    private int indicSec;
    
    // Start is called before the first frame update
    void Start()
    {
        campoTexto = GetComponent<TextMeshProUGUI>();
        indicMin = (int)tempoSegundos/60;
        indicSec = (int)tempoSegundos%60;
        if(indicSec > 9){
            campoTexto.text = (indicMin + ":" + indicSec);
        }else{
            campoTexto.text = (indicMin + ":0" + indicSec); 
        }
        StartCoroutine(ReduzTempo());
    }

    // Update is called once per frame
    void Update()
    {
        //Cheat para aumentar o tempo
        if(Input.GetKeyDown(KeyCode.L)){
            tempoSegundos += 120;
        }
    }

    IEnumerator ReduzTempo(){
        yield return new WaitForSeconds(1f);
        if(tempoSegundos > 0){
        tempoSegundos -= 1f;
        }
        indicMin = (int)tempoSegundos/60;
        indicSec = (int)tempoSegundos%60;
        if(indicSec > 9){
            campoTexto.text = (indicMin + ":" + indicSec);
        }else{
            campoTexto.text = (indicMin + ":0" + indicSec); 
        }
        StartCoroutine(ReduzTempo());
    }
}
