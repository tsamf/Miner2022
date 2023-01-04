using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float runSpeed = 5f;

    private Rigidbody2D myRigidbody2D;

    void Awake() {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value)
    {
        Debug.Log("I moved!");
    }

    void OnJump(InputValue value)
    {
        myRigidbody2D.velocity += new Vector2(0, jumpSpeed);
        Debug.Log("I jumped!");
    }
}
