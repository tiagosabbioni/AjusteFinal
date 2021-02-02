using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum estadoPlayer{
    padrao, interagindo    
}

public class PlayerController : MonoBehaviour
{
    //ATRIBUTOS

    //PARTICULAS
        public ParticleSystem smoke;

    //COMPONENTES BÁSICOS DO JOGADOR

        private Animator anim;
        private AudioSource audioSource;
        private BoxCollider2D boxCollider2d;
        private Rigidbody2D rb2D;
        private SpriteRenderer spr;

    //CENA
        private Scene currentScene;
        //Nome da Cena Atual
        public static string sceneName;

    //VALORES DO JOGADOR
        [SerializeField] private float speed, jumpForce;
        private int lifePoints;
        //Variável para mudar o sentido do raycast de pegar objetos
        [HideInInspector] public float castDirection;
         public static int objectCode;
        


    //LAYERS
        [SerializeField] private LayerMask objectLayer, platformLayer, leverLayer, buttonLayer;

    //GAMEOBJECTS
        private GameObject lever;
        private GameMaster gameMaster;
        private GameObject button;
        private GameObject disjuntor;
        private GameObject contadorObjetivo;

    //INPUTS DO JOGADOR
        private float movement;
        private bool jumpInput;
        private bool interactionInput;


    //BOOLEANS DO JOGADOR
        [HideInInspector] public bool isHolding;
        [HideInInspector] public bool isGrounded;

    //SONS DO JOGADOR

        [SerializeField] private AudioClip JUMP_SOUND;
        [SerializeField] private AudioClip WALK_SOUND;
        [SerializeField] private AudioClip PICK_SOUND;
        [SerializeField] private AudioClip SPAWN_SOUND;
        [SerializeField] private AudioClip DEATH_SOUND;

    void Awake(){
        this.anim = GetComponent<Animator>();
        this.boxCollider2d = GetComponent<BoxCollider2D>();
        this.rb2D = GetComponent<Rigidbody2D>();
        this.spr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if(GameMaster.playerColidiu){
            this.transform.position = gameMaster.ultimoCheckpoint;
        }else{
            this.transform.position = this.transform.position;
        }
        castDirection = 1f;
        isHolding = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(this.SPAWN_SOUND);
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        this.contadorObjetivo = GameObject.FindGameObjectWithTag("ContadorObjetivo");
    }

    void Update()
    {
        
        this.interactionInput = Input.GetButtonDown("Interação");
        if(Time.timeSinceLevelLoad >= 1.5f){
            this.Move();
            this.GroundTest();
            this.Animate();
            Jump();
        }
        //Ignora colisão com objetos interativos
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        //GodMode
        if (Input.GetKeyDown(KeyCode.I)){
            lifePoints += 99999;
        }
        
        this.PickObject();
        this.PuxaAlavanca();
        this.ApertaBotao();
    }

    private void Animate(){
        this.anim.SetFloat("Velocidade", Mathf.Abs(this.movement));
        this.anim.SetBool("IsHolding", this.isHolding);
        this.anim.SetBool("IsJumping", !this.GroundTest());
    }

    public void Move()
    {
        this.movement = (Input.GetAxis("Horizontal") * this.speed);
        this.rb2D.velocity = new Vector2(this.movement, this.rb2D.velocity.y);
        if ( this.movement > 0 ){
            this.spr.flipX = false;
            this.castDirection = 1f;
        }
        else if ( this.movement < 0 ){
            this.spr.flipX = true;
            this.castDirection = -1f;
        }
    }

    public void Jump()
    {
        this.jumpInput = Input.GetButtonDown("Jump");
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isGrounded){
                this.rb2D.AddForce(Vector2.up * this.jumpForce);
                CreateSmoke();
                audioSource.PlayOneShot(JUMP_SOUND);
            }
        }
    }

    private bool GroundTest()
    {
        float margem = 0.25f;
        //Detecta se há colisão com algum objeto e, caso haja, atribui o gameObject à variável hit
        RaycastHit2D hit = Physics2D.BoxCast(this.boxCollider2d.bounds.center, this.boxCollider2d.bounds.size, 0f, Vector2.down, margem, this.platformLayer);
        //Representa visualmente a colisão com a plataforma no canvas
        if ( hit.collider != null ){
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down, Color.red);
            this.isGrounded = true;
        }
        else{
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down, Color.green);
            this.isGrounded = false;
            
        }
        return hit.collider != null;
    }

    public void PickObject()
    {
        float dist = 0.05f;
        
        //Detecta colisão horizontal com a layer dos objetos para que o jogador possa pegar o objeto à sua frente.
        RaycastHit2D hit = Physics2D.BoxCast(this.boxCollider2d.bounds.center, this.boxCollider2d.bounds.size, 0f, Vector2.right * this.castDirection, dist, this.objectLayer);
        
        if ( hit.collider != null ){
            objectCode = hit.collider.gameObject.GetComponent<Objeto>().myCode;
            if ( this.interactionInput && this.rb2D.velocity.x == 0 && !this.isHolding){
                this.isHolding = true;
                this.audioSource.PlayOneShot(PICK_SOUND);
            }else if ( this.interactionInput && this.isHolding ){
                this.isHolding = false;
            }
        }
    }

    public static IEnumerator ResetaFase()
    {
        yield return new WaitForSeconds (2f);
        SceneManager.LoadScene(sceneName);
    }

    public void TakeDamage()
    {
        this.lifePoints -= 1;
        if(this.lifePoints<= 0){
            anim.SetBool("morto", true);
            this.audioSource.PlayOneShot(this.DEATH_SOUND);
            this.rb2D.velocity = Vector2.zero;
            this.rb2D.AddForce(Vector2.down * 200);
            rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
            rb2D.freezeRotation = true;
            this.StartCoroutine(ResetaFase());
        }         
    }

    public void PuxaAlavanca()
    {
        float dist = 0.05f;

        //Detecta colisão horizontal com a layer dos objetos para que o jogador possa pegar o objeto à sua frente.
        RaycastHit2D hit = Physics2D.BoxCast(this.boxCollider2d.bounds.center, this.boxCollider2d.bounds.size, 0f, Vector2.right * this.castDirection, dist, this.leverLayer);
        if(hit.collider != null){
            lever = hit.collider.gameObject;
        }
        if(hit.collider != null && interactionInput && this.lever.GetComponent<Alavanca>().phase == 0)
        {
            this.lever.GetComponent<Alavanca>().phase = 1;
            this.lever.GetComponent<Alavanca>().anim.SetInteger("estagio", 1);
            this.lever.GetComponent<AudioSource>().PlayOneShot(this.lever.GetComponent<Alavanca>().LEVER_SOUND);
            this.lever.GetComponent<Alavanca>().CreateSpark();
            anim.SetBool("interagindo", true);
            StartCoroutine(DesativaInteracao());
           
        
        }else if(hit.collider != null && interactionInput && this.lever.GetComponent<Alavanca>().phase == 1)
        {
            this.lever.GetComponent<Alavanca>().phase = 0;
            this.lever.GetComponent<Alavanca>().anim.SetInteger("estagio", 0);
            this.lever.GetComponent<AudioSource>().PlayOneShot(this.lever.GetComponent<Alavanca>().LEVER_SOUND);
            this.lever.GetComponent<Alavanca>().CreateSpark();
            anim.SetBool("interagindo", true);
            StartCoroutine(DesativaInteracao());

        }

    }

    public void ApertaBotao()
    {
        float dist = 0.05f;

        //Detecta colisão horizontal com a layer dos objetos para que o jogador possa pegar o objeto à sua frente.
        RaycastHit2D hit = Physics2D.BoxCast(this.boxCollider2d.bounds.center, this.boxCollider2d.bounds.size, 0f, Vector2.right * this.castDirection, dist, this.buttonLayer);
        if (hit.collider != null)
        {
            button = hit.collider.gameObject;
        }
        if (hit.collider != null && interactionInput && this.button.GetComponent<Button>().phase == 0)
        {
            this.button.GetComponent<Button>().phase = 1;
            this.button.GetComponent<Button>().anim.SetInteger("estagio", 1);
            this.button.GetComponent<AudioSource>().PlayOneShot(this.button.GetComponent<Button>().BUTTON_SOUND);
            anim.SetBool("interagindo", true);
            StartCoroutine(DesativaInteracao());
            this.button.GetComponent<Button>().isPressed = true;
            if(this.button.GetComponent<Button>().isDisjuntor == true)
            {
                this.contadorObjetivo.GetComponent<ContadorObjetivo>().indicador += 1;
            }

        }
        else if (hit.collider != null && interactionInput && this.button.GetComponent<Button>().phase == 1)
        {
            this.button.GetComponent<Button>().phase = 0;
            this.button.GetComponent<Button>().anim.SetInteger("estagio", 0);
            this.button.GetComponent<AudioSource>().PlayOneShot(this.button.GetComponent<Button>().BUTTON_SOUND);
            anim.SetBool("interagindo", true);
            StartCoroutine(DesativaInteracao());
            this.button.GetComponent<Button>().isPressed = false;
            if (this.button.GetComponent<Button>().isDisjuntor == true)
            {
                this.contadorObjetivo.GetComponent<ContadorObjetivo>().indicador -= 1;
            }

        }

    }



    IEnumerator DesativaInteracao(){
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("interagindo", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageDealer"))
        {
            this.TakeDamage();
        }
    
    }

    void CreateSmoke()
    {
        smoke.Play();  
    }


}