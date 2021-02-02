using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    //ATRIBUTOS

    //COMPONENTES
        private AudioSource audioS;
        private SpriteRenderer spr;
        private Rigidbody2D rb2d;
        private Animator anim;
    
    //GAMEOBJECTS
        [SerializeField] private GameObject playerController;
        [SerializeField] private GameObject socket;
        private GameObject contadorObjetivo;
        [SerializeField] private AudioClip THROW_SOUND;

    //VALORES
        [SerializeField] private float xThrowStrenght;
        [SerializeField] private float yThrowStrenght;
        [SerializeField] private float myAngle;
        private Vector2 newAngle;
        [SerializeField] public int myCode;
    
    //BOOLEANS
        [HideInInspector] public bool isInserted;

        [HideInInspector] public bool onPlayer;


    void Awake()
    {
        this.spr = GetComponent<SpriteRenderer>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.audioS = GetComponent<AudioSource>();
    }

    void Start()
    {
        this.onPlayer = false;
        this.contadorObjetivo = GameObject.FindGameObjectWithTag("ContadorObjetivo");
        this.playerController = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        this.Pick();
        this.Throw();
        this.Insert();
    }

    public void Pick()
    {
        if(PlayerController.objectCode == this.myCode){
            if ( this.playerController.GetComponent<PlayerController>().isHolding && !this.isInserted ){
                this.transform.position = this.playerController.GetComponent<PlayerController>().transform.position;
            }else if ( Input.GetButtonDown("Interação") && !this.playerController.GetComponent<PlayerController>().isHolding && !this.isInserted ){
                this.rb2d.velocity = Vector2.zero;
                this.transform.position = this.transform.position;
            }
        }
    }

    public void Throw()
    {
        if(PlayerController.objectCode == myCode){
            if ( this.playerController.GetComponent<PlayerController>().isHolding){
                this.newAngle = new Vector2(Mathf.Cos(this.myAngle  * Mathf.Deg2Rad) * this.playerController.GetComponent<PlayerController>().castDirection, Mathf.Sin(this.myAngle * Mathf.Deg2Rad));
                if (Input.GetKeyDown(KeyCode.X)){
                    audioS.PlayOneShot(THROW_SOUND);
                    this.playerController.GetComponent<PlayerController>().isHolding = false;
                    this.rb2d.velocity = Vector2.zero;
                    this.rb2d.AddForce(new Vector2(newAngle.normalized.x * this.xThrowStrenght, newAngle.normalized.y * this.yThrowStrenght), ForceMode2D.Impulse);
                }
            }
        }
    }

    public void Insert()
    {
        if ( this.isInserted )
        {
            this.rb2d.gravityScale = 0f;
            this.transform.position = transform.parent.position;
            this.transform.parent.parent.GetComponent<Animator>().SetBool("ligado", true);
            this.transform.Rotate(new Vector3(0, 0, 1));
            this.spr.sortingOrder = this.socket.GetComponent<SpriteRenderer>().sortingOrder + 1 ;
            this.playerController.GetComponent<SpriteRenderer>().sortingOrder = this.spr.sortingOrder + 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject == this.socket && !this.playerController.GetComponent<PlayerController>().isHolding )
        {
            this.transform.position = collision.gameObject.transform.position;
            this.transform.parent = collision.gameObject.transform;
            this.playerController.GetComponent<PlayerController>().isHolding = false;
            this.ContaObjetivo();
            this.isInserted = true;
        }
    }

    private void ContaObjetivo(){
        this.contadorObjetivo.GetComponent<ContadorObjetivo>().indicador += 1;
    }
}