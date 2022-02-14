using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Transform shootPoint;

    [SerializeField] Object shootProj;
    [SerializeField] Object superShootProj;
    [SerializeField] Object arrestProj;
    [SerializeField] Object superArrestProj;

    [SerializeField] float shootCooldown = 0.2f;
    float timeToShoot = 0.0f;

    [SerializeField] float chargeTime = 0.5f;
    float chargingFor = 0.0f;
    bool charging = false;
    bool chargingLethal = true;
    bool charged = false;

    [SerializeField] GameObject chargeEffect;
    GameObject chargeEffectInstance;

    [SerializeField] int maxBuffer = 2;
    int bufferedCharge = 0;
    int bufferedShoot = 0;

    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        //Handle Cooldown
        if (timeToShoot > 0.0f)
            timeToShoot -= Time.deltaTime;

        if (charging && !charged)
        {
            chargingFor += Time.deltaTime;
            
            if (chargingFor > chargeTime)
            {
                Charged();
            }
        }
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
        else if (bufferedShoot < maxBuffer)
        {
            if (context.started)
            {
                //if we started charging while we couldn't, start charging as soon as we can
                if (bufferedCharge == 0)
                    StartCoroutine(BufferedCharge(lethal));
                bufferedCharge++;
            }
            else if (context.canceled)
            {
                //if we release even before we could shoot, shoot immediately when we get the chance
                bufferedShoot++;
            }
        }
    }

    IEnumerator BufferedCharge(bool lethal)
    {
        do
        {
            do
            {
                yield return null;
            } while (timeToShoot >= 0.0f);

            StartCharging(lethal);
            bufferedCharge--;

            if (bufferedShoot > 0)
            {
                yield return null;
                Shoot(lethal);
                bufferedShoot--;
            }
        } while (bufferedCharge > 0);

        bufferedShoot = 0;
    }

    void StartCharging(bool lethal)
    {
        //Safety to avoid duplicates
        if (chargeEffectInstance != null)
            Destroy(chargeEffectInstance);

        //VFX
        chargeEffectInstance = Instantiate(chargeEffect, shootPoint);

        charging = true;
        chargingLethal = lethal;
    }

    void Charged()
    {
        //VFX
        chargeEffectInstance.GetComponent<Charge>().Charged();

        charged = true;
    }

    void StopCharging()
    {
        //VFX
        if (chargeEffectInstance)
            chargeEffectInstance.GetComponent<Charge>().Release();

        charged = false;
        charging = false;
        chargingFor = 0.0f;
    }
    
    //Create the projectile and handles shooting logic
    void Shoot(bool lethal)
    {
        timeToShoot = shootCooldown;

        //Find out what we are going to shoot
        Object projectile;
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
