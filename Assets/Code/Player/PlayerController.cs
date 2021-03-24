using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.IK;

public sealed class PlayerController : MonoBehaviour
{
    #region Properties
    [Header("Movement settings")]
    [Tooltip("Speed ammount to apply when player moves, multiplied by Time.deltaTime.")]
    [SerializeField] float speed;
    [Tooltip("The animator of the complete back arm.")]
    [SerializeField]Animator backArmAnimator;
    [Header("Raycast collision settings")]
    [Tooltip("The ground layer to check if raycast collides with him.")]
    [SerializeField] LayerMask groundLayer;
    [Tooltip("Distance to draw the raycast in Vector2.Up direction, to check collision with ground at jump.")]
    [SerializeField] float distanceTop;
    [Tooltip("Distance to draw the raycast in transform.right, to check collision with walls on ground movement.")]
    [SerializeField] float distanceFront;
    [Header("Aim settings")]
    [Tooltip("The sight of the gun to put over the screen.")]
    [SerializeField] Transform mouseSight;
    [Tooltip("Target transform used to move the arm when is aiming on screen.")]
    [SerializeField] Transform frontArmTarget;
    [Tooltip("Target transform used to move the arm when is aiming on screen.")]
    [SerializeField] Transform backArmTarget; 
    [Tooltip("Back arm LimbSolver2D to update the arm target following player aim with two hands gun.")]
    [SerializeField] LimbSolver2D limbSolver2D;
    public Gun gun{get;set;}
    private Rigidbody2D rigid;
    private Animator animator;
    private Camera mainCam;
    private Vector2 mouseSightPosition;
    public Transform secondHandGrab{get;set;}
    public Transform twoHandsGun{get;set;}
    private bool idle, movement = true, onGround, death;
    public bool IsDeath { set => death = value; }
    public bool Movement { set => movement = value; }
    private float xInput;
    #endregion
    #region Unity Methods
    void Awake()
    {
        idle=true;
        mouseSight.SetParent(null);
        mainCam=Camera.main;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (movement)
        {
            float xVelo = speed * xInput * Time.deltaTime;
            rigid.SetVelocity(xVelo, 0);
        }
        else if (death)
        {
            rigid.SetVelocity(0, 0);
        }
    }
    private void LateUpdate() {
        animator.SetBool("walk", !idle);
        backArmAnimator.SetBool("walk", !idle);

        animator.SetBool("idle", idle);
        backArmAnimator.SetBool("idle", idle);
    }
    #endregion
    
    #region Input
    public void OnLook(InputAction.CallbackContext context){
        mouseSightPosition=mainCam.ScreenToWorldPoint(context.ReadValue<Vector2>());
        mouseSight.position=mouseSightPosition;
        frontArmTarget.position = mouseSight.position;
        if(secondHandGrab!=null)secondHandGrab.position = twoHandsGun.position;
        if(mouseSight.GetX()>transform.GetX()){
            transform.localScale=new Vector2(.8f,.8f);
        }else{
            transform.localScale = new Vector2(-.8f, .8f);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed && movement)
        {
            xInput = context.ReadValue<Vector2>().x;
            if (xInput > 0) transform.eulerAngles.Set(0, 0, 0);
            else transform.eulerAngles.Set(0, 180, 0);

            idle=false;
        }
        else if(context.canceled){
            if(xInput!=0)xInput=0;
            idle=true;
        }
    }
    public void OnPause(InputAction.CallbackContext context){
        if(context.performed){
            Pause.instance.PauseGame();
        }
    }
    public void OnSelect(InputAction.CallbackContext context){
        if(context.performed){
            
        }
    }
    public void OnFire(InputAction.CallbackContext context){
        if(context.performed && movement){
            if(gun!=null && gun.gameObject.activeSelf){
                gun.Shoot();
            }
        }
    }
    #endregion
}