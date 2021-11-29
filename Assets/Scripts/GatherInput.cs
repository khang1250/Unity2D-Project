using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls myControls;

    public GameObject pausePanel;


    public float valueX;
    public bool jumpInput;

    public bool attackInput;

    public bool slideInput;

    public float valueY;
    public bool climbInput;

    public bool menuInput;

    private bool pause = false;

    public bool LvlUpInput;

    public bool interactInput;

    public static GatherInput instance;

    private void Awake()
    {
        myControls = new Controls();
        instance = this;
    }
    
        private void OnEnable()
        {
            myControls.Player.Move.performed += StartMove;
            myControls.Player.Move.canceled  += StopMove;

            myControls.Player.Jump.performed += JumpStart;
            myControls.Player.Jump.canceled  += JumpStop;

            myControls.Player.Attack.performed += TryToAttack;
            myControls.Player.Attack.canceled += StopTryToAttack;

            myControls.Player.Slide.performed += TryToSlide;
            myControls.Player.Slide.canceled += StopTryToSlide;

            myControls.Player.Climb.performed += ClimbStart;
            myControls.Player.Climb.canceled += ClimbStop;

            myControls.Player.LvlUP.performed += LevelUpStart;
            myControls.Player.LvlUP.canceled += LevelUpStop;

            myControls.Player.Interact.performed += InteractStart;
            myControls.Player.Interact.canceled += InteractStart;

            myControls.Ui.Pause.performed += PauseGame;
            myControls.Ui.Menu.performed += OpenMenu;

            myControls.Player.Enable();
            myControls.Ui.Pause.Enable();
            myControls.Ui.Menu.Enable();
        }
    
    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled  -= StopMove;

        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled  -= JumpStop;

        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled -= StopTryToAttack;

        myControls.Player.Slide.performed -= TryToSlide;
        myControls.Player.Slide.canceled -= StopTryToSlide;

        myControls.Player.Climb.performed -= ClimbStart;
        myControls.Player.Climb.canceled -= ClimbStop;

        myControls.Player.LvlUP.performed -= LevelUpStart;
        myControls.Player.LvlUP.canceled -= LevelUpStop;

        myControls.Player.Interact.performed -= InteractStart;
        myControls.Player.Interact.canceled -= InteractStart;

        myControls.Ui.Pause.performed -= PauseGame;
        myControls.Ui.Menu.performed -= CloseMenu;

        myControls.Player.Disable();
        myControls.Ui.Pause.Disable();
        myControls.Ui.Menu.Disable();
        //myControls.Disable();
    }

    public void DisableControls()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;

        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled  -= StopTryToAttack;

        myControls.Player.Slide.performed -= TryToSlide;
        myControls.Player.Slide.canceled -= StopTryToSlide;

        myControls.Player.Climb.performed -= ClimbStart;
        myControls.Player.Climb.canceled -= ClimbStop;

        myControls.Player.LvlUP.performed -= LevelUpStart;
        myControls.Player.LvlUP.canceled -= LevelUpStop;

        myControls.Player.Disable();  
        valueX = 0;
    }

    #region Move
    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX=ctx.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }
    #endregion

    #region Jump
    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }
    #endregion

    #region Attack
    private void TryToAttack(InputAction.CallbackContext ctx)
    {
        attackInput = true;
    }

    private void StopTryToAttack(InputAction.CallbackContext ctx)
    {
        attackInput = false;
    }
    #endregion

    #region Slide
    private void TryToSlide(InputAction.CallbackContext ctx)
    {
        slideInput = true;
    }

    private void StopTryToSlide(InputAction.CallbackContext ctx)
    {
        slideInput = false;
    }
    #endregion

    #region Climb Ladder
    private void ClimbStart(InputAction.CallbackContext ctx)
    {
        valueY = Mathf.RoundToInt(ctx.ReadValue<float>());

        if (Mathf.Abs(valueY) > 0)
        {
            climbInput = true;
        }
    }

    private void ClimbStop(InputAction.CallbackContext ctx)
    {
        climbInput = false;
        valueY = 0;
    }
    #endregion

    #region Pause
    private void PauseGame(InputAction.CallbackContext ctx)
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
            myControls.Player.Disable();
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            myControls.Player.Enable();
            pausePanel.SetActive(false);

        }
    }
    #endregion

    #region Test Level Up
    private void LevelUpStart(InputAction.CallbackContext ctx)
    {
        LvlUpInput = true;
    }

    private void LevelUpStop(InputAction.CallbackContext ctx)
    {
        LvlUpInput = false;
    }
    #endregion

    #region Interact
    private void InteractStart(InputAction.CallbackContext ctx)
    {
        interactInput = true;
    }

    private void InteractStop(InputAction.CallbackContext ctx)
    {
        interactInput = false;
    }
    #endregion

    #region
    private void OpenMenu(InputAction.CallbackContext ctx)
    {
        menuInput = true;
    }
    private void CloseMenu(InputAction.CallbackContext ctx)
    {
        menuInput = false;
    }
    #endregion
}
