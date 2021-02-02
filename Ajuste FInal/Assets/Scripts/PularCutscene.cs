using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PularCutscene : MonoBehaviour
{
    public string nomeDaCena;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Interação")){
            trocarCena();
        }
    }

    void trocarCena(){
         SceneManager.LoadScene(nomeDaCena);
    }
}