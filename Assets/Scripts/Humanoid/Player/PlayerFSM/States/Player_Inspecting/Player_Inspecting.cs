using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inspecting : Player_Layer
{
    Vector3 moveDir = Vector3.zero;
    readonly KeyAction switchToNormal = new KeyAction(KeyCode.F, 0);
    public Player_Inspecting(Layer parent, Player player) : base(parent, player)
    {
        defaultState = new Player_Inspecting_Normal(this, player);
        AddState("Normal", defaultState);
        AddState("BuildMode", new Player_Inspecting_BuildMode(this, player));
    }
    public override void OnStateEnter()
    {
        player.inspecting = true;
        player.freeCam.Priority = 11;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        UIManager.Instance.ScreenMarker(true);
        InputManager.AddAction(switchToNormal);
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        CameraRotation();
        Move();
        if (switchToNormal.GetKeyDown())
        {
            parentLayer.SwitchState("Normal");
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        player.inspecting = false;
        player.freeCam.Priority = 10;
        UIManager.Instance.ScreenMarker(false);
        InputManager.RemoveAction(switchToNormal);
        base.OnStateExit();
    }
    void CameraRotation()
    {
        if (player.cameraRotationClick.GetMouse())
        {
            float horizontalInput = player.cameraRotation.GetAxisX();
            float verticalInput = player.cameraRotation.GetAxisY();

            player.freeCam.transform.Rotate(Vector3.up, horizontalInput * player.camSensitivity, Space.World);
            player.freeCam.transform.Rotate(Vector3.right, ((player.camYReverse) ? -1.0f : 1.0f) * verticalInput * player.camSensitivity, Space.Self);
        }
    }
    void Move()
    {
        float moveX = ((player.moveA.GetKey() ? -1 : 0) + (player.moveD.GetKey() ? 1 : 0)) * player.flySpeed;
        float moveY = ((player.moveS.GetKey() ? -1 : 0) + (player.moveW.GetKey() ? 1 : 0)) * player.flySpeed;
        Vector3 dir = new Vector3(moveX, 0, moveY);
        player.freeCam.transform.Translate(dir * player.flySpeed * Time.deltaTime);
        if (InputManager.Input_CameraUp())
        {
            player.freeCam.transform.position += Vector3.up * player.updownSpeed * Time.deltaTime;
        }
        if (InputManager.Input_CameraDown())
        {
            player.freeCam.transform.position += Vector3.down * player.updownSpeed * Time.deltaTime;
        }
    }
}
