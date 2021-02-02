using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Porta : MonoBehaviour
{

    int contAtual;
    int contMaximo;
    public GameObject contador1;
    public GameObject contador2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int.TryParse(contador1.GetComponent<TextMeshProUGUI>().text, out contAtual);
        int.TryParse(contador2.GetComponent<TextMeshProUGUI>().text, out contMaximo);

        if( contAtual >= contMaximo){
            GetComponent<Animator>().SetBool("aberto", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else{
            GetComponent<Animator>().SetBool("aberto", false);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
