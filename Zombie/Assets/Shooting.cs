using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shooting : MonoBehaviour
{
    // objects required
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    // shooting parameters from weapon and character impacted
    private float damage = 10f;
    private float range = 100f;
    private float fireRate = 0.3f;


    private bool isShooting = false;
    private bool isInShootingAnim = false;
    AnimatePlayer playerAnim;
    
    // Update is called once per frame
    void Start()
    {
        playerAnim = GetComponent<AnimatePlayer>();
    }

    private void Update()
    {
        if (isShooting)
        {
            ShootingGun();
        }
    }
    public void StartShoot()
    {
        isShooting = true;
    }

    public void StopShoot()
    {
        if (!isShooting)
        {
            return;
        }
        isShooting = false;
    }

    public void FinishedShotAnimation()
    {
        isInShootingAnim = false;
        Debug.Log("workedOut");
    }

    private float animationTimer;

    private void ShootingGun()
    {
        float startPoint = Time.time;
        if (isShooting)
        {
            if(!isInShootingAnim)
            {
                playerAnim.shootAnimationGun(fireRate);
                isInShootingAnim = true;
                FireBullet();
            }

        }
       
    }

    private void FireBullet()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

    }
}
