using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jato : MonoBehaviour
{
    public bool ativo;
    public bool soltandoAgua;
    [SerializeField] GameObject prefabJato;
    [SerializeField] private float delay;
    private float tempoAteAlternar;
    [SerializeField] private AudioClip SOM_JATO;

    // Start is called before the first frame update
    void Start()
    {
        tempoAteAlternar = Time.time + delay; 
    }

    // Update is called once per frame
    void Update()
    {
        SoltaAgua();
    }

    void SoltaAgua(){
            if( tempoAteAlternar <= Time.time ){
                if(ativo == true){
                    ativo = false;
                }else if(ativo == false){
                    ativo = true;
                }
                soltandoAgua = false;
                tempoAteAlternar += delay;
            }
            if( ativo && !soltandoAgua ){
                GetComponent<AudioSource>().PlayOneShot(SOM_JATO);
                GameObject agua = Instantiate(prefabJato, new Vector3(transform.position.x + 0.05f, transform.position.y - 2.75f, 0f), Quaternion.identity);
                agua.transform.parent = this.transform;
                soltandoAgua = true;
            }
            tempoAteAlternar -= Time.deltaTime;
    }
}
