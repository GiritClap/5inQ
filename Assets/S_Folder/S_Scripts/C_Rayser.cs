using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Rayser : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rayser()
    {
        animator.Play("Rayser");
    }

    
}
