using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoEsgotado : MonoBehaviour
{
    public GameObject contador;
    private AudioSource audioS;
    [SerializeField] public AudioClip somAlarme;
    private bool restartingGame;

    // Start is called before the first frame update
    void Start()
    {
        contador = GameObject.Find("Contador de Tempo");
        audioS = GetComponent<AudioSource>();
        restartingGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(contador.GetComponent<ContadorTempo>().tempoSegundos <= 0){
            if(!restartingGame){
            audioS.PlayOneShot(somAlarme);
            GetComponent<Image>().enabled = true;
            StartCoroutine(PlayerController.ResetaFase());
            restartingGame = true;
            }
        }else{
            GetComponent<Image>().enabled = false;
        }
    }
}