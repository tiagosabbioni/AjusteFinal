using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanterna : MonoBehaviour
{
    public Transform lanterna;
    public Transform espelho;
    public Transform porta;
    public Transform botao1;
    public Transform botao2;
    public Transform plataforma;
    public LayerMask espelhoLayer, doorLayer;
    
    public GameObject Laser1;
    public GameObject Laser2;
    public GameObject Laser3;
    public bool criarLaser;

    public float laserX;
    public float laserY;
    public float laserY2;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        criarLaser = true;
    }

    void Update()
    {
        if(this.botao1.GetComponent<Button>().isPressed == true && this.botao2.GetComponent<Button>().isPressed == true)
        {
            VerificarLaser();
            anim.SetInteger("Ligar", 1);

            //CriarLaser
            if (criarLaser == true){
            Instantiate(Laser1, new Vector2(transform.position.x + laserX, transform.position.y), Quaternion.identity);
            Instantiate(Laser2, new Vector2(espelho.transform.position.x, espelho.transform.position.y - laserY), Quaternion.Euler(0, 0, 90));
            Instantiate(Laser3, new Vector2(espelho.transform.position.x, espelho.transform.position.y - laserY2), Quaternion.Euler(0, 0, 90));
            criarLaser = false;
            }
        }
    }

    void VerificarLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(lanterna.position, lanterna.right, espelhoLayer);

        if (hit.transform == espelho)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(espelho.transform.position.x, espelho.transform.position.y - 0.1f), Vector2.down , doorLayer);
 
            if (hit2.transform == porta)
            {

                    this.porta.GetComponent<Porta3>().anim.SetInteger("Abrir", 1);
                    this.porta.GetComponent<BoxCollider2D>().enabled = false;
                    Debug.Log(hit2.collider.name);
            }            
 
            if(hit2.transform == plataforma)
            {
                this.porta.GetComponent<Porta3>().anim.SetInteger("Abrir", 0);
                this.porta.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}

