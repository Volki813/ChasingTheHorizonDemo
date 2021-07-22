using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    private Camera mainCamera;
    private float originalCameraSize = 7.080622f;
    private Vector3 originalCameraPosition = new Vector3(0, 1, -10);
    private CursorController cursor;

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
        cursor = FindObjectOfType<CursorController>();
    }

    public void EngageAttack(UnitLoader attacker, UnitLoader defender)
    {
        StartCoroutine(Attack(attacker, defender));

        attackerPortrait.sprite = attacker.unit.portrait;
        defenderPortrait.sprite = defender.unit.portrait;

        attackerHealth.maxValue = attacker.unit.statistics.health;
        attackerHealth.value = attacker.currentHealth;

        defenderHealth.maxValue = defender.unit.statistics.health;
        defenderHealth.value = defender.currentHealth;

    }

    private IEnumerator Attack(UnitLoader attacker, UnitLoader defender)
    {
        ActionCamera(attacker, defender);

        combatReadout.SetActive(true);
        yield return new WaitForSeconds(1f);

        AttackAnimation(attacker, defender);
        yield return new WaitForSeconds(attacker.animator.GetCurrentAnimatorClipInfo(0).Length);

        PlayEffect(attacker, defender);
        yield return new WaitForSeconds(1.2f);

        InitiatorAttack(attacker, defender);
        yield return new WaitForSeconds(0.3f);

        if(CheckForDeaths(attacker, defender) == "Attacker")
        {
            combatReadout.SetActive(false);
            ResetCamera();
            cursor.NeutralCursor = false;
            cursor.MapCursor = true;
            yield return null;
        }
        else if(CheckForDeaths(attacker, defender) == "Defender")
        {
            combatReadout.SetActive(false);
            attacker.Rest();
            ResetCamera();
            cursor.NeutralCursor = false;
            cursor.MapCursor = true;
            yield return null;
        }
        else
        {
            if(Vector2.Distance(attacker.transform.position, defender.transform.position) <= 1)
            {
                AttackAnimation(defender, attacker);
                yield return new WaitForSeconds((defender.animator.GetCurrentAnimatorClipInfo(0).Length));

                PlayEffect(defender, attacker);
                yield return new WaitForSeconds(1.2f);

                DefenderAttack(attacker, defender);
                yield return new WaitForSeconds(0.3f);
            }

            if(CheckForDeaths(attacker, defender) == "Attacker")
            {
                combatReadout.SetActive(false);
                ResetCamera();
                cursor.NeutralCursor = false;
                cursor.MapCursor = true;
                yield return null;
            }
            else if(CheckForDeaths(attacker, defender) == "Defender")
            {
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                cursor.NeutralCursor = false;
                cursor.MapCursor = true;
                yield return null;
            }
            else
            {
                if(CheckAttackSpeed(attacker, defender))
                {
                    InitiatorAttack(attacker, defender);
                    cursor.NeutralCursor = false;
                    cursor.MapCursor = true;
                    yield return null;
                }
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                cursor.NeutralCursor = false;
                cursor.MapCursor = true;
                yield return null;
            }
            combatReadout.SetActive(false);
            if(CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
            {
                attacker.Rest();
            }
            ResetCamera();
            cursor.NeutralCursor = false;
            cursor.MapCursor = true;
            yield return null;
        }
        combatReadout.SetActive(false);
        if (CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
        {
            attacker.Rest();
        }
        ResetCamera();
        cursor.NeutralCursor = false;
        cursor.MapCursor = true;
        yield return null;
    }


    private void ActionCamera(UnitLoader attacker, UnitLoader defender)
    {
        var point1 = attacker.transform.position;
        var point2 = defender.transform.position;

        var centerPoint = (point1 + point2) / 2;
        Vector3 zoomPoint = new Vector3(centerPoint.x, centerPoint.y, -10);

        mainCamera.transform.position = zoomPoint;
        mainCamera.orthographicSize = 4;
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
                defender.currentHealth = defender.currentHealth - Critical(attacker, defender);
                defenderHealth.value = defender.currentHealth;
            }
            else
            {
                //Unit Hits
                defender.currentHealth = defender.currentHealth - Hit(attacker, defender);
                defenderHealth.value = defender.currentHealth;
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
        if(Vector3.Distance(defender.transform.position, attacker.transform.position) <= 1 || defender.equippedWeapon.range == attacker.equippedWeapon.range)
        {
            //Check for a hit
            if (HitRoll(defender))
            {
                //Checks for a crit
                if (CritRoll(defender))
                {
                    //Unit Crits
                    attacker.currentHealth = attacker.currentHealth - Critical(defender, attacker);
                    attackerHealth.value = attacker.currentHealth;
                }
                else
                {
                    //Unit Hits
                    attacker.currentHealth = attacker.currentHealth - Hit(defender, attacker);
                    attackerHealth.value = attacker.currentHealth;
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
        //Diagonal Attacks
        if(attacker.unit.allyUnit)
        {
            if(attacker.transform.position.x > defender.transform.position.x && attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDownLeft"); //Down Left
                return;
            }
            if (attacker.transform.position.x < defender.transform.position.x && attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDownRight"); //Down Left
                return;
            }
            if (attacker.transform.position.x < defender.transform.position.x && attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUpRight"); //Down Left
                return;
            }
            if (attacker.transform.position.x > defender.transform.position.x && attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUpLeft"); //Down Left
                return;
            }
        }

        //XY Attacks
        if (attacker.transform.position.x > defender.transform.position.x)
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
        else if(attacker.transform.position.y < defender.transform.position.y)
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
            yield return new WaitForSeconds(attacker.equippedWeapon.animationLength.length);
            Destroy(effect);
        }
        yield return null;
    }
    
    private bool HitRoll(UnitLoader unit)
    {
        int roll = Random.Range(0, 99);

        if (roll > unit.CombatStatistics().hit)
            return false;

        else
            return true;
    }

    private bool CritRoll(UnitLoader unit)
    {
        int roll = Random.Range(0, 99);

        if (roll > unit.CombatStatistics().crit)
            return false;

        else
            return true;
    }

    public int Hit(UnitLoader attacker, UnitLoader defender)
    {
        return attacker.CombatStatistics().attack - defender.CombatStatistics().protection;
    }
    private int Critical(UnitLoader attacker, UnitLoader defender)
    {
        return attacker.CombatStatistics().attack * 2 - defender.CombatStatistics().protection;
    }

    private string CheckForDeaths(UnitLoader attacker, UnitLoader defender)
    {
        if(defender.currentHealth <= 0)
        {
            AIManager.instance.enemyOrder.Remove(defender);
            TurnManager.instance.RefreshTiles();
            defender.Death();
            return "Defender";
        }
        else if(attacker.currentHealth <= 0)
        {
            TurnManager.instance.RefreshTiles();
            attacker.Death();
            return "Attacker";
        }
        return null;
    }

    private bool CheckAttackSpeed(UnitLoader attacker, UnitLoader defender)
    {
        if(attacker.CombatStatistics().attackSpeed > defender.CombatStatistics().attackSpeed + 5)
            return true;
        else
            return false;
    }
}
