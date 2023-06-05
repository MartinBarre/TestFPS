using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("GLOBAL")]
    public float damage;
    public float fireRate;
    public float recoilRecuperation;

    [Header("MAGAZINE")]
    public int magazineSize;
    public int magazineStore;

    [Header("CAMERA SHAKE")]
    public float shakePushDuration;
    public float shakeRestoreDuration;
    public float shakePush;

    [Header("SOUNDS")]
    public AudioClip audioShoot;
    public AudioClip audioShootEmpty;
    public AudioClip audioReload;
}
