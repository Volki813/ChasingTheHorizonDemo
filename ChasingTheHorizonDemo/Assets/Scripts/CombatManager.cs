using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    private Camera mainCamera;
    private float originalCameraSize = 7.080622f;
    private Vector3 originalCameraPosition = new Vector3(0, 1, -10);

    public static CombatManager instance { get; private set; }

    public GameObject combatReadout;

    public Image attackerPortrait;
    public Image defenderPortrait;

    public Slider attackerHealth;
    public Slider defenderHealth;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
    }

    public void EngageAttack(UnitLoader attacker, UnitLoader defender)
    {
        StartCoroutine(Attack(attacker, defender));

        attackerPortrait.sprite = attacker.unit.portrait;
        defenderPortrait.sprite = defender.unit.portrait;

        attackerHealth.maxValue = attacker.unit.health;
        attackerHealth.value = attacker.hp;

        defenderHealth.maxValue = defender.unit.health;
        defenderHealth.value = defender.hp;

    }

    private IEnumerator Attack(UnitLoader attacker, UnitLoader defender)
    {
        ActionCamera(attacker, defender);

        combatReadout.SetActive(true);
        yield return new WaitForSeconds(1f);

        AttackAnimation(attacker, defender);
        yield return new WaitForSeconds(1f);

        PlayEffect(attacker, defender);
        yield return new WaitForSeconds(1f);

        InitiatorAttack(attacker, defender);
        yield return new WaitForSeconds(1f);

        if(CheckForDeaths(attacker, defender) == "Attacker")
        {
            combatReadout.SetActive(false);
            ResetCamera();
            yield return null;
        }
        else if(CheckForDeaths(attacker, defender) == "Defender")
        {
            combatReadout.SetActive(false);
            attacker.Rest();
            ResetCamera();
            yield return null;
        }
        else
        {
            AttackAnimation(defender, attacker);
            yield return new WaitForSeconds(1f);

            PlayEffect(defender, attacker);
            yield return new WaitForSeconds(1f);

            DefenderAttack(attacker, defender);
            yield return new WaitForSeconds(1f);

            if(CheckForDeaths(attacker, defender) == "Attacker")
            {
                combatReadout.SetActive(false);
                ResetCamera();
                yield return null;
            }
            else if(CheckForDeaths(attacker, defender) == "Defender")
            {
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                yield return null;
            }
            else
            {
                if(CheckAttackSpeed(attacker, defender))
                {
                    InitiatorAttack(attacker, defender);
                    yield return null;
                }
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                yield return null;
            }
            combatReadout.SetActive(false);
            if(CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
            {
                attacker.Rest();
            }
            ResetCamera();
            yield return null;
        }
        combatReadout.SetActive(false);
        if (CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
        {
            attacker.Rest();
        }
        ResetCamera();
        yield return null;
    }



    private void ActionCamera(UnitLoader attacker, UnitLoader defender)
    {
        var point1 = attacker.transform.position;
        var point2 = defender.transform.position;

        var centerPoint = (point1 + point2) / 2;
        Vector3 zoomPoint = new Vector3(centerPoint.x, centerPoint.y, -10);

        mainCamera.transform.position = zoomPoint;
        mainCamera.orthographicSize = 2;
    }
    private void ResetCamera()
    {
        mainCamera.transform.position = originalCameraPosition;
        mainCamera.orthographicSize = originalCameraSize;

    }
    
    private void InitiatorAttack(UnitLoader attacker, UnitLoader defender)
    {
        //Check for a hit
        if(HitRoll(attacker))
        {
            //Checks for a crit
            if(CritRoll(attacker))
            {
                //Unit Crits
                defender.hp = defender.hp - Critical(attacker, defender);
                defenderHealth.value = defender.hp;
            }
            else
            {
                //Unit Hits
                defender.hp = defender.hp - Hit(attacker, defender);
                defenderHealth.value = defender.hp;
            }
        }
        else
        {
            //Unit Misses
            return;
        }
    }
    private void DefenderAttack(UnitLoader attacker, UnitLoader defender)
    {
        if(Vector3.Distance(defender.transform.position, attacker.transform.position) <= 1)
        {
            //Check for a hit
            if (HitRoll(defender))
            {
                //Checks for a crit
                if (CritRoll(defender))
                {
                    //Unit Crits
                    attacker.hp = attacker.hp - Critical(defender, attacker);
                    attackerHealth.value = attacker.hp;
                }
                else
                {
                    //Unit Hits
                    attacker.hp = attacker.hp - Hit(defender, attacker);
                    attackerHealth.value = attacker.hp;
                }
            }
            else
            {
                //Unit Misses
                return;
            }
        }
    }

    private void AttackAnimation(UnitLoader attacker, UnitLoader defender)
    {
        if(attacker.transform.position.x > defender.transform.position.x)
        {
            attacker.animator.SetTrigger("AttackLeft"); //left
        }
        else if(attacker.transform.position.x < defender.transform.position.x)
        {
            attacker.animator.SetTrigger("AttackRight"); //right
        }
        else if(attacker.transform.position.y > defender.transform.position.y)
        {
            attacker.animator.SetTrigger("AttackDown"); //down
        }
        else
        {
            attacker.animator.SetTrigger("AttackUp"); //up
        }
    }
    private void PlayEffect(UnitLoader attacker, UnitLoader defender)
    {
        StartCoroutine(EffectAnimation(attacker, defender));
    }
    private IEnumerator EffectAnimation(UnitLoader attacker, UnitLoader defender)
    {
        if(attacker.equippedWeapon.animation != null)
        {
            var effect = Instantiate(attacker.equippedWeapon.animation, defender.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.68f);
            Destroy(effect);
        }
        yield return null;
    }
    
    private bool HitRoll(UnitLoader unit)
    {
        int roll = Random.Range(0, 99);

        if (roll > unit.hit)
            return false;

        else
            return true;
    }
    private bool CritRoll(UnitLoader unit)
    {
        int roll = Random.Range(0, 99);

        if (roll > unit.crit)
            return false;

        else
            return true;
    }

    public int Hit(UnitLoader attacker, UnitLoader defender)
    {
        return attacker.attack - defender.protection;
    }
    private int Critical(UnitLoader attacker, UnitLoader defender)
    {
        return attacker.attack * 2 - defender.protection;
    }

    private string CheckForDeaths(UnitLoader attacker, UnitLoader defender)
    {
        if(defender.hp <= 0)
        {
            return "Defender";
        }
        else if(attacker.hp <= 0)
        {
            return "Attacker";
        }
        return null;
    }
    private bool CheckAttackSpeed(UnitLoader attacker, UnitLoader defender)
    {
        if(attacker.attackSpeed > defender.attackSpeed + 5)
            return true;
        else
            return false;
    }
}
