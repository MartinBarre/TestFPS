using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    public WeaponController wc;
    public PlayerController playerController;

    public void ReloadFinished()
    {
        wc.ReloadFinished();
    }

    public void WeaponTakeOffFinished()
    {
        playerController.WeaponTakeOffFinished();
    }
}
