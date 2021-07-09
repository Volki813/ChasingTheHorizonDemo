using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BattleStatistics
{
    public int attack;
    public int attackSpeed;
    public int protection;
    public int resilience;
    public int hit;
    public int avoid;
    public int crit;
    public int vigilance;
    public int hitChance;
    public int critChance;
    public int damage;


    public BattleStatistics(int atk = 0, int atksp = 0, int prtc = 0, int res = 0, int h = 0, int avo = 0, int crt = 0, int vig = 0, int hc = 0, int cc = 0, int dmg = 0)
    {
        //Constructor

        attack = atk;
        attackSpeed = atksp;
        protection = prtc;
        resilience = res;
        hit = h;
        avoid = avo;
        crit = crt;
        vigilance = vig;
        hitChance = hc;
        critChance = cc;
        damage = dmg;
    }


    //same deal as the statistics

    //addition
    public static BattleStatistics operator +(BattleStatistics a, BattleStatistics b) 
        => new BattleStatistics
        (
            (a.attack + b.attack),
            (a.attackSpeed + b.attackSpeed),
            (a.protection + b.protection),
            (a.resilience + b.resilience),
            (a.hit + b.hit),
            (a.avoid + b.avoid),
            (a.crit + b.crit),
            (a.vigilance + b.vigilance),
            (a.hitChance + b.hitChance),
            (a.critChance + b.critChance),
            (a.damage + b.damage)           
        );
    
    //subtraction
    public static BattleStatistics operator -(BattleStatistics a, BattleStatistics b) 
        => new BattleStatistics
        (
            (a.attack - b.attack),
            (a.attackSpeed - b.attackSpeed),
            (a.protection - b.protection),
            (a.resilience - b.resilience),
            (a.hit - b.hit),
            (a.avoid - b.avoid),
            (a.crit - b.crit),
            (a.vigilance - b.vigilance),
            (a.hitChance - b.hitChance),
            (a.critChance - b.critChance),
            (a.damage - b.damage)              
        );

    //multiplication
    public static BattleStatistics operator *(BattleStatistics a, BattleStatistics b) 
        => new BattleStatistics
        (
            (a.attack * b.attack),
            (a.attackSpeed * b.attackSpeed),
            (a.protection * b.protection),
            (a.resilience * b.resilience),
            (a.hit * b.hit),
            (a.avoid * b.avoid),
            (a.crit * b.crit),
            (a.vigilance * b.vigilance),
            (a.hitChance * b.hitChance),
            (a.critChance * b.critChance),
            (a.damage * b.damage)             
        );

    //division
    public static BattleStatistics operator /(BattleStatistics a, BattleStatistics b) 
        => new BattleStatistics
        (
            (a.attack / b.attack),
            (a.attackSpeed / b.attackSpeed),
            (a.protection / b.protection),
            (a.resilience / b.resilience),
            (a.hit / b.hit),
            (a.avoid / b.avoid),
            (a.crit / b.crit),
            (a.vigilance / b.vigilance),
            (a.hitChance / b.hitChance),
            (a.critChance / b.critChance),
            (a.damage / b.damage)
        );
}
