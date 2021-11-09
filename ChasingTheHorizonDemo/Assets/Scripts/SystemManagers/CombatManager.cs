using System.Collections;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//The CombatManager handles all combat between units
//There should only be 1 CombatManager in a given scene
//You can initiate combat between two units in any script

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance { get; private set; }

    //VARIABLES 
    private float originalCameraSize = 7.080622f;
    private Vector3 originalCameraPosition = new Vector3(0, 1, -10);

    //REFERENCES
    DialogueHolder dialogueHolder;
    CursorController cursor;
    private Camera mainCamera = null;
    [SerializeField] private GameObject combatReadout = null;
    [SerializeField] private Image attackerPortrait = null;
    [SerializeField] private Image defenderPortrait = null;
    [SerializeField] private Slider attackerHealth = null;
    [SerializeField] private Slider defenderHealth = null;
    [SerializeField] private BattleText battleText = null;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        cursor = FindObjectOfType<CursorController>();
        dialogueHolder = FindObjectOfType<DialogueHolder>();
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
        cursor.controls.Disable();
        cursor.GetComponent<SpriteRenderer>().sprite = null;

        ActionCamera(attacker, defender);

        combatReadout.SetActive(true);
        yield return new WaitForSeconds(1f);

        AttackAnimation(attacker, defender);
        yield return new WaitForSeconds(attacker.animator.GetCurrentAnimatorClipInfo(0).Length);

        if(attacker.equippedWeapon.animation)
        {
            PlayEffect(attacker, defender);
            yield return new WaitForSeconds(1.2f);
        }

        InitiatorAttack(attacker, defender);
        yield return new WaitForSeconds(0.3f);

        if(CheckForDeaths(attacker, defender) == "Attacker")
        {
            attacker.DelayedDeath();
            combatReadout.SetActive(false);
            ResetCamera();
            cursor.ResetState();
            if(attacker.unit.allyUnit)
            {
                cursor.controls.MapCursor.Enable();
            }
            yield return null;
        }
        else if(CheckForDeaths(attacker, defender) == "Defender")
        {
            defender.DelayedDeath();
            combatReadout.SetActive(false);
            attacker.Rest();
            ResetCamera();
            cursor.ResetState();
            if (attacker.unit.allyUnit)
            {
                cursor.controls.MapCursor.Enable();
            }
            yield return null;
        }
        else
        {
            if(CheckDistance(defender, attacker) <= 1 || defender.equippedWeapon.range >= attacker.equippedWeapon.range)
            {
                AttackAnimation(defender, attacker);
                yield return new WaitForSeconds((defender.animator.GetCurrentAnimatorClipInfo(0).Length));

                if(defender.equippedWeapon.animation)
                {
                    PlayEffect(defender, attacker);
                    yield return new WaitForSeconds(1.2f);
                }

                DefenderAttack(attacker, defender);
                yield return new WaitForSeconds(0.3f);
            }

            if(CheckForDeaths(attacker, defender) == "Attacker")
            {
                attacker.DelayedDeath();
                combatReadout.SetActive(false);
                ResetCamera();
                cursor.ResetState();
                if (attacker.unit.allyUnit)
                {
                    cursor.controls.MapCursor.Enable();
                }
                yield return null;
            }
            else if(CheckForDeaths(attacker, defender) == "Defender")
            {   
                defender.DelayedDeath();
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                cursor.ResetState();
                if (attacker.unit.allyUnit)
                {
                    cursor.controls.MapCursor.Enable();
                }
                yield break;
            }
            else
            {
                if(CheckAttackSpeed(attacker, defender))
                {
                    AttackAnimation(attacker, defender);
                    yield return new WaitForSeconds(attacker.animator.GetCurrentAnimatorClipInfo(0).Length);

                    if(attacker.equippedWeapon.animation)
                    {
                        PlayEffect(attacker, defender);
                        yield return new WaitForSeconds(1.2f);
                    }

                    InitiatorAttack(attacker, defender);
                    yield return new WaitForSeconds(0.3f);

                    cursor.ResetState();
                    cursor.controls.MapCursor.Enable(); yield return null;
                }
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                cursor.ResetState();
                if (attacker.unit.allyUnit)
                {
                    cursor.controls.MapCursor.Enable();
                }
                yield return null;
            }
            combatReadout.SetActive(false);
            if(CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
            {
                defender.DelayedDeath();
                attacker.Rest();
            }
            ResetCamera();
            cursor.ResetState();
            if (attacker.unit.allyUnit)
            {
                cursor.controls.MapCursor.Enable();
            }
            yield return null;
        }
        combatReadout.SetActive(false);
        if (CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
        {
            attacker.Rest();
        }
        ResetCamera();
        cursor.ResetState();
        if(attacker.unit.allyUnit)
        {
            cursor.controls.MapCursor.Enable();
        }

        //This block checks if an Ally Unit is below 50% health, which if true triggers their battle dialogue
        if(!BattleDialogueManager.instance.busy && attacker.unit.allyUnit && attacker.currentHealth < attacker.unit.statistics.health * 0.5f && attacker.currentHealth > 0)
        {
            int randomNumber = Random.Range(0, attacker.GetComponent<BattleDialogue>().under50Quotes.Length);
            BattleDialogueManager.instance.WriteDialogue(attacker.GetComponent<BattleDialogue>().under50Quotes[randomNumber], attacker);
        }
        else if(!BattleDialogueManager.instance.busy && defender.unit.allyUnit && defender.currentHealth < defender.unit.statistics.health * 0.5f && defender.currentHealth > 0)
        {
            int randomNumber = Random.Range(0, defender.GetComponent<BattleDialogue>().under50Quotes.Length);
            BattleDialogueManager.instance.WriteDialogue(defender.GetComponent<BattleDialogue>().under50Quotes[randomNumber], defender);
        }

        cursor.GetComponent<SpriteRenderer>().sprite = cursor.highlight;
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
                battleText.SetText("Crit");                
                Instantiate(battleText, defender.transform.position, Quaternion.identity);
                defender.currentHealth = defender.currentHealth - Critical(attacker, defender);
                defenderHealth.value = defender.currentHealth;
                if (attacker.unit.allyUnit)
                {
                    BattleDialogueManager.instance.busy = true;
                    int randomNumber = Random.Range(0, attacker.GetComponent<BattleDialogue>().criticalQuotes.Length);
                    BattleDialogueManager.instance.WriteDialogue(attacker.GetComponent<BattleDialogue>().criticalQuotes[randomNumber], attacker);
                }
            }
            else
            {
                //Unit Hits
                battleText.SetText("Hit");
                Instantiate(battleText, defender.transform.position, Quaternion.identity);
                defender.currentHealth = defender.currentHealth - Hit(attacker, defender);
                defenderHealth.value = defender.currentHealth;
            }
        }
        else
        {
            //Unit Misses
            battleText.SetText("Miss");
            Instantiate(battleText, defender.transform.position, Quaternion.identity);
            return;
        }
    }
    private void DefenderAttack(UnitLoader attacker, UnitLoader defender)
    {
        if(CheckDistance(defender, attacker) <= 1 || defender.equippedWeapon.range >= attacker.equippedWeapon.range)
        {
            //Check for a hit
            if (HitRoll(defender))
            {
                //Checks for a crit
                if (CritRoll(defender))
                {
                    //Unit Crits                    
                    battleText.SetText("Crit");
                    Instantiate(battleText, attacker.transform.position, Quaternion.identity);
                    attacker.currentHealth = attacker.currentHealth - Critical(defender, attacker);
                    attackerHealth.value = attacker.currentHealth;
                    if (defender.unit.allyUnit)
                    {
                        BattleDialogueManager.instance.busy = true;
                        int randomNumber = Random.Range(0, defender.GetComponent<BattleDialogue>().criticalQuotes.Length);
                        BattleDialogueManager.instance.WriteDialogue(defender.GetComponent<BattleDialogue>().criticalQuotes[randomNumber], defender);
                    }
                }
                else
                {
                    //Unit Hits
                    battleText.SetText("Hit");
                    Instantiate(battleText, attacker.transform.position, Quaternion.identity);
                    attacker.currentHealth = attacker.currentHealth - Hit(defender, attacker);
                    attackerHealth.value = attacker.currentHealth;
                }
            }
            else
            {
                //Unit Misses
                battleText.SetText("Miss");
                Instantiate(battleText, attacker.transform.position, Quaternion.identity);
                return;
            }
        }
    }    

    private void AttackAnimation(UnitLoader attacker, UnitLoader defender)
    {
        //XY Attacks
        if(attacker.transform.position.y == defender.transform.position.y)
        {
            if (attacker.transform.position.x < defender.transform.position.x)
            {
                attacker.animator.SetTrigger("AttackRight"); //Unit is to the left of enemy so it attacks right
                return;
            }
            else if (attacker.transform.position.x > defender.transform.position.x)
            {
                attacker.animator.SetTrigger("AttackLeft"); //Unit is to the right of the enemy so it attacks left
                return;
            }
        }
        else if (attacker.transform.position.x == defender.transform.position.x)
        {
            if (attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUp"); //Unit is below the enemy so it attacks up
                return;
            }
            else if (attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDown"); //Unit is above the enemy so it attacks down
                return;
            }
        }

        //Checks if a unit is in a diagonal position but doesn't have diagonal attack animations
        else if(!attacker.unit.diagonal)
        {
            if (attacker.transform.position.x < defender.transform.position.x && attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUp");
                return;
            }
            if (attacker.transform.position.x > defender.transform.position.x && attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUp");
                return;
            }
            if (attacker.transform.position.x < defender.transform.position.x && attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDown");
                return;
            }
            if (attacker.transform.position.x > defender.transform.position.x && attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDown");
                return;
            }
        }

        //Diagonal Attacks
        else if(attacker.unit.diagonal)
        {
            if (attacker.transform.position.x < defender.transform.position.x && attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUpRight"); //Unit is down left of the enemy so it attacks up right
                return;
            }
            if (attacker.transform.position.x > defender.transform.position.x && attacker.transform.position.y < defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackUpLeft"); //Unit is down right of the enemy so it attacks up left
                return;
            }
            if (attacker.transform.position.x < defender.transform.position.x && attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDownRight"); //Unit is up right of the enemy so it attacks down left
                return;
            }
            if (attacker.transform.position.x > defender.transform.position.x && attacker.transform.position.y > defender.transform.position.y)
            {
                attacker.animator.SetTrigger("AttackDownLeft"); //Unit is up left of the enemy so it attacks down right
                return;
            }
        }
    }
    private void PlayEffect(UnitLoader attacker, UnitLoader defender)
    {
        StartCoroutine(EffectAnimation(attacker, defender));
    }
    private IEnumerator EffectAnimation(UnitLoader attacker, UnitLoader defender)
    {
        var effect = Instantiate(attacker.equippedWeapon.animation, defender.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attacker.equippedWeapon.animationLength.length);
        Destroy(effect);        
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
            return "Defender";
        }
        else if(attacker.currentHealth <= 0)
        {
            TurnManager.instance.RefreshTiles();
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

    private float CheckDistance(UnitLoader unit1, UnitLoader unit2)
    {
        return Mathf.Abs(unit1.transform.position.x - unit2.transform.position.x) + Mathf.Abs(unit1.transform.position.y - unit2.transform.position.y);
    }
}
