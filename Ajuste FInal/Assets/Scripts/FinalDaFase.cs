using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDaFase : MonoBehaviour
{
    public string nomeDaCena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D (Collider2D collider){
        trocarCena();
    }

    public void trocarCena(){
        SceneManager.LoadScene(nomeDaCena);
    }
}