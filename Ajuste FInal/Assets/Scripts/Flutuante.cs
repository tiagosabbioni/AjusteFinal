using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flutuante : MonoBehaviour
{
    private float fator;
    public bool jatoEsquerdo;
    public bool jatoDireito;
    public GameObject botaoEsquerdo;
    public GameObject botaoDireito;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        jatoEsquerdo = botaoEsquerdo.GetComponent<Button>().isPressed;
        jatoDireito = botaoDireito.GetComponent<Button>().isPressed;
        if(jatoEsquerdo){
            transform.position = new Vector3(transform.position.x + 1 * Time.deltaTime, transform.position.y, 0);
        }else if(jatoDireito){
            transform.position = new Vector3(transform.position.x - 1 * Time.deltaTime, transform.position.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Plataforma")){
                jatoEsquerdo = false;
                jatoDireito = false;
                botaoEsquerdo.GetComponent<Button>().isPressed = false;
                botaoDireito.GetComponent<Button>().isPressed = false;
        }
    }
}