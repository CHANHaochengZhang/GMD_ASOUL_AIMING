using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour


{

    
    public PlayerMovements pm;
    
    public Transform shooterPoint;
    public Transform playerBody; 

    public int bulletsMag = 30;//射击的位置
    public int range = 100;//射程
    public int bulletLeft = 720;//备弹
    public int currentBullets;//当前子弹数量
    public float fireRate = 0.1f;
    private float fireTimer;//
    
    //gun fire light
    public ParticleSystem muzzleFlash;
    public Light muzzleFlashLight;
    public GameObject hitParticle;
    public GameObject bulletHole;
    
    //bullet shell
    public Transform casingSpawnPoint;//shell drop position
    public Transform casingPrefab; //shell
    
    //sound audio
    private AudioSource audioSource;
    [FormerlySerializedAs("AK47SoundClip")] public AudioClip ak47SoundClip;
    public AudioClip reloadLeft;
    public AudioClip reloadOutOfAmmo;
    
    private bool gunShootInput;
    private bool isReload;
    public bool isAiming;

    [Header("Key setting")] [Tooltip("Reload")]private KeyCode reloadInputName;
   [SerializeField] [Tooltip("Look at weapon")]private KeyCode lookAtWeapon;
    
    [Header("UI setting")] 
    public Text ammoTextUI;
    public Image crossHairUI;
    
    [System.Serializable]
    public class spawnpoints
    {  
        [Header("Spawnpoints")]
        //Array holding casing spawn points 
        //Casing spawn point array
        public Transform casingSpawnPoint;
        //Bullet prefab spawn from this point
        public Transform bulletSpawnPoint;
        //Grenade prefab spawn from this point
        public Transform grenadeSpawnPoint;
    }
    [System.Serializable]
    public class prefabs
    {  
        [Header("Prefabs")]
        public Transform bulletPrefab;
        public Transform casingPrefab;
        public Transform grenadePrefab;
    }
    public prefabs Prefabs;
    public spawnpoints Spawnpoints;
    
    //Animation
    private Animator anim;
    
    //Camera
    public Camera mainCamera;
    
    
    // Start is called before the first frame update
    void Start()
    {
         
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentBullets = bulletsMag;
        reloadInputName = KeyCode.R;
        lookAtWeapon = KeyCode.F;
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        //FIRE
        if (gunShootInput=Input.GetMouseButton(0))
        {
            GunFire();
            // transform.localRotation = Quaternion.Euler(10,0f,0f);
            // mainCamera.transform.Rotate(Vector3.left*(bulletsMag-currentBullets));
            
            // mainCamera.transform.rotation = Quaternion.Euler(-2,0f,0f);
        }
        else
        {
            muzzleFlashLight.enabled = false;
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
//RELOAD
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("reload_out_of_ammo") || info.IsName("reload_ammo_left"))
        {
            isReload = true;
        }
        else
        {
            isReload = false;
        }
        if (Input.GetKeyDown(reloadInputName)&& currentBullets<bulletsMag && bulletLeft>0)
        {
            Reload();
        }
//LOOK AT WEAPON
        if (Input.GetKeyDown(lookAtWeapon))
        {
            Debug.Log("!!!look");
            anim.SetTrigger("Inspector");
        }
        
        anim.SetBool("Run",pm.isRun);
        anim.SetBool("Walk",pm.isWalk);
        DoingAim();
    }

    private void GunFire()
    {
        if (fireTimer < fireRate || currentBullets <= 0 || isReload || pm.isRun) return;

        Vector3 shootDirection = shooterPoint.forward;
        RaycastHit hit;
        if (Physics.Raycast(shooterPoint.position, shootDirection, out hit, range))
        {
            Debug.Log(hit.transform);
            GameObject hitParticleEffect =
                Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            GameObject bulletHoleEffect =
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

            //Spawn bullet at bullet spawnpoint
            var bullet = (Transform)Instantiate (
                Prefabs.bulletPrefab,
                Spawnpoints.bulletSpawnPoint.transform.position,
                Spawnpoints.bulletSpawnPoint.transform.rotation);

            //Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = 
                bullet.transform.forward * 150;
       
                 
            //Add tag to the bullet
            bullet.tag = "ak47Bullet";
            Debug.Log(bullet.tag+"shot");
            //Remove gravity from the rifle bullet
                bullet.GetComponent<Rigidbody>().useGravity = false;
          
       

            //Spawn casing prefab at spawnpoint
            Instantiate (Prefabs.casingPrefab, 
                Spawnpoints.casingSpawnPoint.transform.position, 
                Spawnpoints.casingSpawnPoint.transform.rotation);
            
            
            Destroy(hitParticleEffect, 1);
            Destroy(bulletHoleEffect, 3f);
        }

        if (!isAiming)
        {
            anim.CrossFadeInFixedTime("fire_gun",0.1f);
        }
        else
        {
            //aim animation
            anim.CrossFadeInFixedTime("aim_fire",0.1f);
            
        }

    
        currentBullets--;
        PlayerShootSound();
        Instantiate(casingPrefab, casingSpawnPoint.transform.position, casingSpawnPoint.transform.rotation);
        muzzleFlashLight.enabled = true;
        muzzleFlash.Play();
        UpdateAmmoUI();
        fireTimer = 0f;//Rest after firing
    }

    private void UpdateAmmoUI()
    {
        ammoTextUI.text = currentBullets + "/" + bulletLeft;
    }

    private void Reload()
    {
        if (bulletLeft<=0) return;
        PlayReloadAnimation();
        int bulletToLoad = bulletsMag - currentBullets;
        int bulletToReduce= (bulletLeft >= bulletToLoad) ? bulletToLoad : bulletLeft;
       bulletLeft -= bulletToReduce;
       currentBullets += bulletToReduce;
      
       UpdateAmmoUI();

    }

    public void PlayReloadAnimation()
    {
        if (currentBullets>0)
        {
            // play reload1
            anim.Play("reload_ammo_left",0,0);
            audioSource.clip = reloadLeft;
            audioSource.Play();
        }
        else  
        {
            //reload 2
            anim.Play("reload_out_of_ammo",0,0);
            audioSource.clip = reloadOutOfAmmo;
            audioSource.Play();
        }
    }

    private void PlayerShootSound()
    {
        audioSource.clip = ak47SoundClip;
        audioSource.Play();
    }

    public void DoingAim()
    {
        if (Input.GetMouseButton(1) && !isReload && !pm.isRun)
        {
            //aim,change crosshair pov 
            //Debug.Log("!!!!!!!!!aim");
            isAiming = true;
            anim.SetBool("Aim",true);
            crossHairUI.gameObject.SetActive(false);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 80, 0.3f);

        }
        else
        {
            //not aim
            isAiming = false;
            anim.SetBool("Aim",false);
            crossHairUI.gameObject.SetActive(true);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 90, 0.3f);
        }

        
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Yangtuo")
        {
            if ( c.gameObject.GetComponent<YangtuoController>().GetCurrentHealth() == 1)
            {
            
            }
          
        }
    }
}
