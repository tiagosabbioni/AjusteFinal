using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interfaceText;
    [SerializeField] public string[] lines;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float valorDelay;
    private int index;
    public float nextTextDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.AutoType());
        interfaceText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this.MudaFrase();
        if(Input.GetButton("Interação")){
            this.typeSpeed = 0.01f;
        }else{
            this.typeSpeed = 0.03f;
        }
    }
    IEnumerator AutoType(){
        //Transforma o valor do índice atual de falas[] em um array de chars, e executa o loop para cada letra no array de letras.
        foreach (char letter in lines[index].ToCharArray()){
            this.interfaceText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        this.nextTextDelay = Time.time + valorDelay;
    }


    public void MudaFrase(){
        if(this.interfaceText.text == this.lines[index]){
            if(this.index < this.lines.Length - 1){
                if(Time.time > this.nextTextDelay){
                    this.index++;
                    this.interfaceText.text = "";
                    StartCoroutine(this.AutoType());
                }
            }else{
                this.interfaceText.text = "";
            }
        }
        if(this.index == this.lines.Length){
            this.gameObject.SetActive(false);
        }
    }

}