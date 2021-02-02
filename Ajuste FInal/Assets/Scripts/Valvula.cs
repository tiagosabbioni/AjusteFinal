using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valvula : MonoBehaviour
{
    public GameObject button;
    public Animator anim;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if(button.GetComponent<Button>().isPressed == true)
        {
            anim.SetBool("Ligar", true);
        }
        else
        {
            anim.SetBool("Ligar", false);
        }


    }
}
