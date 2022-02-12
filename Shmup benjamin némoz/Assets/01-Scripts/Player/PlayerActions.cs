using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Transform shootPoint;

    [SerializeField] string projectilesFolderPath;
    [SerializeField] string shootProjPath;
    Object shootProj;

    [SerializeField] string superShootProjPath;
    Object superShootProj;

    [SerializeField] string arrestProjPath;
    Object arrestProj;

    [SerializeField] string superArrestProjPath;
    Object superArrestProj;

    [SerializeField] float shootCooldown = 0.2f;
    float timeToShoot = 0.0f;

    [SerializeField] float chargeTime = 0.5f;
    float chargingFor = 0.0f;
    bool charging = false;
    bool chargingLethal = true;

    bool bufferedCharge = false;
    bool bufferedShoot = false;

    private void Start()
    {
        shootProj = AssetDatabase.LoadAssetAtPath(projectilesFolderPath + shootProjPath, typeof(GameObject));
        superShootProj = AssetDatabase.LoadAssetAtPath(projectilesFolderPath + superShootProjPath, typeof(GameObject));

        arrestProj = AssetDatabase.LoadAssetAtPath(projectilesFolderPath + arrestProjPath, typeof(GameObject));
        superArrestProj = AssetDatabase.LoadAssetAtPath(projectilesFolderPath + superArrestProjPath, typeof(GameObject));
    }

    private void Update()
    {
        //Handle Cooldown
        if (timeToShoot > 0.0f)
            timeToShoot -= Time.deltaTime;

        if (charging)
            chargingFor += Time.deltaTime;
    }

    public void ShootInput(InputAction.CallbackContext context)
    {
        HandleInput(context, true);
    }

    public void ArrestInput(InputAction.CallbackContext context)
    {
        HandleInput(context, false);
    }

    //Handles the shooting input and wether we should shoot or not
    void HandleInput(InputAction.CallbackContext context, bool lethal)
    {
        if (timeToShoot <= 0.0f)
        {
            if (context.started && !charging) //Start charging a shot only if we started pressing and weren't already charging
            {
                StartCharging(lethal);
            }
            //Shoot only when we release the button and if we were charging something and the shoot input is the same as the charge type
            else if (context.canceled && charging && lethal == chargingLethal)
            {
                Shoot(lethal);
                StopCharging();
            }
        }
        //Buffer the input, only one shot can be buffered
        else
        {
            if (context.started && !bufferedCharge)
            {
                //if we started charging while we couldn't, start charging as soon as we can
                StartCoroutine(BufferedCharge(lethal));
                bufferedCharge = true;
            }
            else if (context.canceled && !bufferedShoot)
            {
                //if we release even before we could shoot, shoot immediately when we get the chance
                bufferedShoot = true;
            }
        }
    }

    IEnumerator BufferedCharge(bool lethal)
    {
        do
        {
            yield return null;
        } while (timeToShoot >= 0.0f);

        StartCharging(lethal);
        bufferedCharge = false;

        if (bufferedShoot)
        {
            yield return null;
            Shoot(lethal);
            bufferedShoot = false;
        }
    }

    void StartCharging(bool lethal)
    {
        //TODO Put vfx

        charging = true;
        chargingLethal = lethal;
    }

    void StopCharging()
    {
        //TODO put vfx

        charging = false;
        chargingFor = 0.0f;
    }
    
    //Create the projectile and handles shooting logic
    void Shoot(bool lethal)
    {
        timeToShoot = shootCooldown;

        //Find out what we are going to shoot
        Object projectile;
        bool charged = chargingFor >= chargeTime;
        if (lethal)
        {
            if (charged)
                projectile = superShootProj;
            else
                projectile = shootProj;
        }
        else
        {
            if (charged)
                projectile = superArrestProj;
            else
                projectile = arrestProj;
        }

        Instantiate(projectile, shootPoint.position, Quaternion.identity);

        StopCharging();
    }
}
