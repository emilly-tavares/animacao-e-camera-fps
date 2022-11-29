using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoPlayer : MonoBehaviour
{
    private CharacterController character;
    private Animator animator;
    
    private Transform camera;

    Vector3 vetores;

    float velocidade = 2f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
       vetores.Set(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));

        //Captura a direção Z da câmera
        var forward = camera.TransformDirection(Vector3.forward);
        forward.y = 0;

        //Captura a direção X da câmera
        var right = camera.TransformDirection(Vector3.right);

        //Cria um vetor que modifica os valores de x e z baseados nos da câmera
        Vector3 direcao = vetores.x * right + vetores.z * forward;

        if (vetores != Vector3.zero && direcao.magnitude > 0.1f){
            
            //Dita a direção a qual o objeto deve olhar 
            //Quaternion.LoolRotation(direção desejada, direção atual)
            Quaternion freeRotation = Quaternion.LookRotation(direcao.normalized, transform.up);

            //Modifica a rotação do objeto com uma transição entre a direção a qual eu ditei e a rotação atual
            transform.rotation = Quaternion.Slerp(transform.rotation, freeRotation, Time.deltaTime * 10);
        }
      
        //Modifica o movimento do objeto com velocidade em todos os vetores
        character.Move(transform.forward * vetores.magnitude * velocidade * Time.deltaTime);
        character.Move(Vector3.down * Time.deltaTime);

        if(vetores != Vector3.zero){

           animator.SetBool("andando", true);
           
         
     
        }else{
            animator.SetBool("andando", false);
        }
    }
}
