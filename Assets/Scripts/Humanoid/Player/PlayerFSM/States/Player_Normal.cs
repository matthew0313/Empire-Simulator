using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Normal : Player_State
{
    Vector3 moveDir = Vector3.zero;
    readonly KeyAction sprint = new KeyAction(KeyCode.LeftShift, 0);
    readonly KeyAction jump = new KeyAction(KeyCode.Space, 0);
    readonly KeyAction switchToInspect = new KeyAction(KeyCode.F, 0);
    public Player_Normal(Layer parent, Player player) : base(parent, player)
    {

    }
    public override void OnStateEnter()
    {
        player.normalCam.Priority = 11;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputManager.AddAction(sprint, jump, switchToInspect);
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        CameraRotation();
        Move();
        if (switchToInspect.GetKeyDown() && EmpireManager.Instance.currentIsland is ClaimableIsland)
        {
            parentLayer.SwitchState("Inspecting");
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        player.normalCam.Priority = 10;
        InputManager.RemoveAction(sprint, jump, switchToInspect);
        base.OnStateExit();
    }
    float prevDirY = 0.0f;
    void CameraRotation()
    {
        if (player.cameraRotationClick.GetMouse())
        {
            float horizontalInput = player.cameraRotation.GetAxisX();
            float verticalInput = player.cameraRotation.GetAxisY();

            player.transform.Rotate(0, horizontalInput * player.camSensitivity, 0, Space.World);
            player.camAnchor.transform.Rotate(Vector3.right, ((player.camYReverse) ? -1.0f : 1.0f) * verticalInput * player.camSensitivity, Space.Self);
        }
    }
    void Move()
    {
        float moveX = (sprint.GetKey() ? player.runSpeed : player.moveSpeed) * ((player.moveA.GetKey() ? -1 : 0) + (player.moveD.GetKey() ? 1 : 0));
        float moveY = (sprint.GetKey() ? player.runSpeed : player.moveSpeed) * ((player.moveS.GetKey() ? -1 : 0) + (player.moveW.GetKey() ? 1 : 0));
        float prevY = moveDir.y;
        moveDir = (moveX * player.transform.TransformDirection(Vector3.right)) + (moveY * player.transform.TransformDirection(Vector3.forward));
        if (player.controller.isGrounded)
        {
            if (jump.GetKeyDown())
            {
                moveDir.y = player.jumpPower;
            }
            else
            {
                moveDir.y = 0.0f;
            }
        }
        else
        {
            moveDir.y = prevY;
        }
        if (player.controller.isGrounded == false)
        {
            moveDir.y -= player.gravity * Time.deltaTime;
        }
        player.controller.Move(moveDir * Time.deltaTime);
    }
}
