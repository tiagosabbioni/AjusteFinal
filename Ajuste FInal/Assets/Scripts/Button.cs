using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    
    public Animator anim;
    public AudioClip BUTTON_SOUND;
    public bool isPressed;
    public int phase;
    public bool isDisjuntor;
    public bool isFlutuante;

    void Start()
    {
        this.phase = 0;

        this.anim = GetComponent<Animator>();
    }


}