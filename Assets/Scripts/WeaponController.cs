using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public Weapon weapon;
    public Animator animator;
    public TMP_Text txtAmmo;

    private int loaderAmmo;
    private int storeAmmo;
    private bool reloading;

    [SerializeField] private RaycastManager raycastManager;
    private PlayerInput playerInput;
    private RecoilManager recoilManager;
    
    private AudioSource audioSource;
    public float fireRateTimer;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponentInParent<PlayerInput>();
        recoilManager = GetComponentInParent<RecoilManager>();
        loaderAmmo = weapon.magazineSize;
        storeAmmo = weapon.magazineSize * weapon.magazineStore;
        txtAmmo.SetText(loaderAmmo + " / " + storeAmmo);
    }

    private void Update()
    {
        if (playerInput.Reload && loaderAmmo < weapon.magazineSize && storeAmmo > 0 && !reloading)
        {
            reloading = true;
            animator.SetTrigger("Reload");
            audioSource.PlayOneShot(weapon.audioReload);
        }

        if (playerInput.Shoot && !reloading)
        {
            while (fireRateTimer <= 0)
            {
                fireRateTimer = weapon.fireRate;
                if (loaderAmmo <= 0)
                {
                    audioSource.PlayOneShot(weapon.audioShootEmpty);
                    break;
                }
                recoilManager.RecoilFire();
                raycastManager.LaunchRaycast();
                animator.SetTrigger("Shooting");
                loaderAmmo--;
                txtAmmo.SetText(loaderAmmo + " / " + storeAmmo);
                audioSource.PlayOneShot(weapon.audioShoot);
            }
        }
        else
        {
            animator.ResetTrigger("Shooting");
        }
        
        fireRateTimer -= Time.deltaTime * 1000f;
    }

    public void ReloadFinished()
    {
        var nbBulletToReload = storeAmmo >= weapon.magazineSize - loaderAmmo ? weapon.magazineSize - loaderAmmo : storeAmmo;
        loaderAmmo += nbBulletToReload;
        storeAmmo -= nbBulletToReload;
        txtAmmo.SetText(loaderAmmo + " / " + storeAmmo);
        reloading = false;
        animator.ResetTrigger("Reload");
    }

    private void OnEnable()
    {
        animator.SetTrigger("TakeOn");
        txtAmmo.SetText(loaderAmmo + " / " + storeAmmo);
        reloading = false;
    }
}
