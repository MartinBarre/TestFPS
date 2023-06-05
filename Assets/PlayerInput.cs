using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Jump { get; private set; }
    public bool Weapon1 { get; private set; }
    public bool Weapon2 { get; private set; }
    public bool Pause { get; private set; }
    public bool Reload { get; private set; }
    public bool Shoot { get; private set; }
    public bool Walk { get; private set; }
    
    public void OnMovement(InputValue inputValue)
    {
        Movement = inputValue.Get<Vector2>();
    }

    public void OnLook(InputValue inputValue)
    {
        Look = inputValue.Get<Vector2>();
    }

    public void OnJump(InputValue inputValue)
    {
        Jump = inputValue.Get<float>() > 0.9f;
    }

    public void OnWeapon1(InputValue inputValue)
    {
        Weapon1 = inputValue.Get<float>() > 0.9f;
    }
    
    public void OnWeapon2(InputValue inputValue)
    {
        Weapon2 = inputValue.Get<float>() > 0.9f;
    }

    public void OnPauseInput(InputValue inputValue)
    {
        Pause = inputValue.Get<float>() > 0.9f;
    }

    public void OnReload(InputValue inputValue)
    {
        Reload = inputValue.Get<float>() > 0.9f;
    }

    public void OnShoot(InputValue inputValue)
    {
        Shoot = inputValue.Get<float>() > 0.9f;
    }

    public void OnWalk(InputValue inputValue)
    {
        Walk = inputValue.Get<float>() > 0.9f;
    }
}
