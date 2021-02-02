﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PessoasFumaca : MonoBehaviour
{
    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){

            dust.Play();
            Destroy(dust, 3f);
            Destroy(this.gameObject);
        }
    }



}
