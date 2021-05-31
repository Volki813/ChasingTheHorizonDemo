using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void EngageAttack(UnitLoader attacker, UnitLoader defender)
    {
        StartCoroutine(Attack(attacker, defender));
    }
    private IEnumerator Attack(UnitLoader attacker, UnitLoader defender)
    {
        //Battle Starts
        Debug.Log("Battle Start!");
        yield return new WaitForSeconds(1f);

        //Assaulting units hit chance is rolled
        if (HitRoll(attacker) == true)
        {
            Debug.Log("Attacker Hit!");
            yield return new WaitForSeconds(1f);

            //If hit sucess is positive, the units crit chances are rolled
            if (CritRoll(attacker) == true)
            {
                Debug.Log("Attacker Crit!");
                yield return new WaitForSeconds(1f);

                //Assulting unit deals crit damage
                defender.hp = defender.hp - Critical(attacker, defender);

                Debug.Log("Attacker dealt " + Critical(attacker, defender) + "damage");
                yield return new WaitForSeconds(1f);

                //If defendeing unit HP hits 0, they die and battle ends
                if (CheckForDeaths(attacker, defender) == true)
                {
                    Debug.Log("Defender died!");
                    yield return new WaitForSeconds(1f);
                    yield return null;
                }
                else
                {
                    //Defending unit retaliates and follows steps 2 to 4
                    Debug.Log("Defender Retaliates!");
                    yield return new WaitForSeconds(1f);
                    if (HitRoll(defender) == true)
                    {
                        Debug.Log("Defender hit!");
                        yield return new WaitForSeconds(1f);

                        if (CritRoll(defender) == true)
                        {
                            Debug.Log("Defender crit!");
                            yield return new WaitForSeconds(1f);

                            attacker.hp = attacker.hp - Critical(defender, attacker);

                            Debug.Log("Defender dealt " + Critical(defender, attacker) + "damage");
                            yield return new WaitForSeconds(1f);


                            if (CheckForDeaths(attacker, defender) == true)
                            {
                                Debug.Log("Attacker died!");
                                yield return new WaitForSeconds(1f);
                                yield return null;
                            }
                            else
                            {
                                if(CheckAttackSpeed(attacker, defender) == true)
                                {
                                    Debug.Log("Attacker is fast enough for another attack");
                                    yield return new WaitForSeconds(1f);

                                    if (HitRoll(attacker) == true)
                                    {
                                        Debug.Log("Attacker hit!");
                                        yield return new WaitForSeconds(1f);

                                        if (CritRoll(attacker) == true)
                                        {
                                            Debug.Log("Attacker crit!");
                                            yield return new WaitForSeconds(1f);

                                            defender.hp = defender.hp - Critical(attacker, defender);

                                            Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                            yield return new WaitForSeconds(1f);
                                            yield return null;
                                        }
                                        else
                                        {
                                            defender.hp = defender.hp - Hit(attacker, defender);

                                            Debug.Log("Attack did " + Hit(attacker, defender) + "damage");
                                            yield return new WaitForSeconds(1f);
                                            yield return null;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("You missed");
                                        yield return null;
                                    }
                                }
                                else
                                {
                                    yield return null;
                                }
                            }
                        }
                        else
                        {
                            attacker.hp = attacker.hp - Hit(defender, attacker);

                            Debug.Log("Defender dealt " + Hit(defender, attacker) + "damage");


                            if (CheckForDeaths(attacker, defender) == true)
                            {
                                yield return null;
                            }
                            else
                            {
                                if (CheckAttackSpeed(attacker, defender) == true)
                                {
                                    Debug.Log("Attacker is fast enough for another attack");
                                    yield return new WaitForSeconds(1f);

                                    if (HitRoll(attacker) == true)
                                    {
                                        Debug.Log("Attacker hit!");
                                        yield return new WaitForSeconds(1f);

                                        if (CritRoll(attacker) == true)
                                        {
                                            Debug.Log("Attacker crit!");
                                            yield return new WaitForSeconds(1f);

                                            defender.hp = defender.hp - Critical(attacker, defender);

                                            Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                            yield return null;
                                        }
                                        else
                                        {
                                            defender.hp = defender.hp - Hit(attacker, defender);

                                            Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                            yield return null;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("You missed");
                                        yield return null;
                                    }
                                }
                                else
                                {
                                    yield return null;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Enemy Missed!");
                        yield return new WaitForSeconds(1f);

                        if (CheckAttackSpeed(attacker, defender) == true)
                        {
                            Debug.Log("Attacker is fast enough for another attack");
                            yield return new WaitForSeconds(1f);

                            if (HitRoll(attacker) == true)
                            {
                                Debug.Log("Attacker hit!");
                                yield return new WaitForSeconds(1f);

                                if (CritRoll(attacker) == true)
                                {
                                    Debug.Log("Attacker crit!");
                                    yield return new WaitForSeconds(1f);

                                    defender.hp = defender.hp - Critical(attacker, defender);

                                    Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                    yield return null;
                                }
                                else
                                {
                                    defender.hp = defender.hp - Hit(attacker, defender);

                                    Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                    yield return null;
                                }
                            }
                            else
                            {
                                Debug.Log("You missed");
                                yield return null;
                            }
                        }
                        else
                        {
                            yield return null;
                        }
                    }
                }
            }
            else
            {
                //Assulting unit deals normal damage
                defender.hp = defender.hp - Hit(attacker, defender);

                Debug.Log("Attack did " + Hit(attacker, defender) + "damage");
                yield return new WaitForSeconds(1f);

                //If defendeing unit HP hits 0, they die and battle ends
                if (CheckForDeaths(attacker, defender) == true)
                {
                    yield return null;
                }
                else
                {
                    //Defending unit retaliates and follows steps 2 to 4
                    Debug.Log("Defender Retaliates");
                    yield return new WaitForSeconds(1f);

                    if (HitRoll(defender) == true)
                    {
                        Debug.Log("Defender hit!");
                        yield return new WaitForSeconds(1f);

                        if (CritRoll(defender) == true)
                        {
                            Debug.Log("Defender crit!");
                            yield return new WaitForSeconds(1f);

                            attacker.hp = attacker.hp - Critical(defender, attacker);

                            Debug.Log("Attack did " + Critical(defender, attacker) + "damage");
                            yield return new WaitForSeconds(1f);

                            if (CheckForDeaths(attacker, defender) == true)
                            {
                                yield return null;
                            }
                            else
                            {
                                if (CheckAttackSpeed(attacker, defender) == true)
                                {
                                    Debug.Log("Attacker is fast enough for another attack");
                                    yield return new WaitForSeconds(1f);

                                    if (HitRoll(attacker) == true)
                                    {

                                        if (CritRoll(attacker) == true)
                                        {
                                            Debug.Log("Attacker crit!");
                                            yield return new WaitForSeconds(1f);

                                            defender.hp = defender.hp - Critical(attacker, defender);

                                            Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                            yield return null;
                                        }
                                        else
                                        {
                                            defender.hp = defender.hp - Hit(attacker, defender);

                                            Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                            yield return null;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("You missed");
                                        yield return null;
                                    }
                                }
                                else
                                {
                                    yield return null;
                                }
                            }
                        }
                        else
                        {
                            attacker.hp = attacker.hp - Hit(defender, attacker);
                            Debug.Log("Defender did " + Hit(defender, attacker) + "damage");

                            if (CheckForDeaths(attacker, defender) == true)
                            {
                                yield return null;
                            }
                            else
                            {
                                if (CheckAttackSpeed(attacker, defender) == true)
                                {
                                    Debug.Log("Attacker is fast enough for another attack");
                                    yield return new WaitForSeconds(1f);

                                    if (HitRoll(attacker) == true)
                                    {
                                        Debug.Log("Attacker hit!");
                                        yield return new WaitForSeconds(1f);                                      

                                        if (CritRoll(attacker) == true)
                                        {
                                            Debug.Log("Attacker crit!");
                                            yield return new WaitForSeconds(1f);

                                            defender.hp = defender.hp - Critical(attacker, defender);

                                            Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                            yield return null;
                                        }
                                        else
                                        {
                                            defender.hp = defender.hp - Hit(attacker, defender);

                                            Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                            yield return null;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("You missed");
                                        yield return null;
                                    }
                                }
                                else
                                {
                                    yield return null;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CheckAttackSpeed(attacker, defender) == true)
                        {
                            Debug.Log("Attacker is fast enough for another attack");
                            yield return new WaitForSeconds(1f);

                            if (HitRoll(attacker) == true)
                            {
                                Debug.Log("Attacker hit!");
                                yield return new WaitForSeconds(1f);

                                if (CritRoll(attacker) == true)
                                {
                                    Debug.Log("Attacker crit!");
                                    yield return new WaitForSeconds(1f);

                                    defender.hp = defender.hp - Critical(attacker, defender);

                                    Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                    yield return null;
                                }
                                else
                                {
                                    defender.hp = defender.hp - Hit(attacker, defender);

                                    Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                    yield return null;
                                }
                            }
                            else
                            {
                                Debug.Log("You missed");
                                yield return null;
                            }
                        }
                        else
                        {
                            yield return null;
                        }
                    }
                }
            }
        }
        else
        {
            //Assulting unit missed
            Debug.Log("You Missed!");
            yield return new WaitForSeconds(1f);

            //Defending unit attacks and follows steps 2 to 4
            if (HitRoll(defender) == true)
            {
                Debug.Log("Defender hit!");
                yield return new WaitForSeconds(1f);

                if (CritRoll(defender) == true)
                {
                    Debug.Log("Defender crit!");
                    yield return new WaitForSeconds(1f);

                    attacker.hp = attacker.hp - Critical(defender, attacker);

                    Debug.Log("Attack did " + Critical(defender, attacker) + "damage");
                    yield return new WaitForSeconds(1f);

                    if (CheckForDeaths(attacker, defender) == true)
                    {
                        yield return null;
                    }
                    else
                    {
                        if (CheckAttackSpeed(attacker, defender) == true)
                        {
                            Debug.Log("Attacker is fast enough for another attack");
                            yield return new WaitForSeconds(1f);

                            if (HitRoll(attacker) == true)
                            {
                                Debug.Log("Attacker hit!");
                                yield return new WaitForSeconds(1f);

                                if (CritRoll(attacker) == true)
                                {
                                    Debug.Log("Attacker crit!");
                                    yield return new WaitForSeconds(1f);

                                    defender.hp = defender.hp - Critical(attacker, defender);

                                    Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                    yield return null;
                                }
                                else
                                {
                                    defender.hp = defender.hp - Hit(attacker, defender);

                                    Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                    yield return null;
                                }
                            }
                            else
                            {
                                Debug.Log("You missed");
                                yield return null;
                            }
                        }
                        else
                        {
                            yield return null;
                        }
                    }
                }
                else
                {
                    attacker.hp = attacker.hp - Hit(defender, attacker);

                    Debug.Log("Attack did " + Hit(defender, attacker) + "damage");
                    yield return new WaitForSeconds(1f);

                    if (CheckForDeaths(attacker, defender) == true)
                    {
                        yield return null;
                    }
                    else
                    {
                        if (CheckAttackSpeed(attacker, defender) == true)
                        {
                            Debug.Log("Attacker is fast enough for another attack");
                            yield return new WaitForSeconds(1f);

                            if (HitRoll(attacker) == true)
                            {
                                Debug.Log("Attacker hit!");
                                yield return new WaitForSeconds(1f);

                                if (CritRoll(attacker) == true)
                                {
                                    Debug.Log("Attacker crit!");
                                    yield return new WaitForSeconds(1f);

                                    defender.hp = defender.hp - Critical(attacker, defender);

                                    Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                                    yield return null;
                                }
                                else
                                {
                                    defender.hp = defender.hp - Hit(attacker, defender);

                                    Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                                    yield return null;
                                }
                            }
                            else
                            {
                                Debug.Log("You missed");
                                yield return null;
                            }
                        }
                        else
                        {
                            yield return null;
                        }
                    }
                }
            }
            else
            {
                if (CheckAttackSpeed(attacker, defender) == true)
                {
                    Debug.Log("Attacker is fast enough for another attack");
                    yield return new WaitForSeconds(1f);

                    if (HitRoll(attacker) == true)
                    {
                        Debug.Log("Attacker hit!");
                        yield return new WaitForSeconds(1f);

                        if (CritRoll(attacker) == true)
                        {
                            Debug.Log("Attacker crit!");
                            yield return new WaitForSeconds(1f);

                            defender.hp = defender.hp - Critical(attacker, defender);

                            Debug.Log("Attack did " + Critical(attacker, defender) + "damage");
                            yield return null;
                        }
                        else
                        {
                            defender.hp = defender.hp - Hit(attacker, defender);

                            Debug.Log("Attack did " + Hit(attacker, defender) + "damage");

                            yield return null;
                        }
                    }
                    else
                    {
                        Debug.Log("You missed");
                        yield return null;
                    }
                }
                else
                {
                    yield return null;
                }
            }
        }
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

    private bool CheckForDeaths(UnitLoader attacker, UnitLoader defender)
    {
        if(defender.hp <= 0)
            return true;

        else if (attacker.hp <= 0)
            return true;

        else
            return false;
    }

    private bool CheckAttackSpeed(UnitLoader attacker, UnitLoader defender)
    {
        if(attacker.attackSpeed > defender.attackSpeed + 5)
            return true;
        else
            return false;
    }
}
