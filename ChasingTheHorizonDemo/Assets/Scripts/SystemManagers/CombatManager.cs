using System.Collections;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//The CombatManager handles all combat between units
//There should only be 1 CombatManager in a given scene
//You can initiate combat between two units in any script

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance { get; private set; }

    //VARIABLES 
    private float originalCameraSize = 7.080622f;
    private Vector3 originalCameraPosition = new Vector3(0, 1, -10);
    private bool dialoguePlayed = false;

    //REFERENCES
    DialogueHolder dialogueHolder;
    CursorController cursor;
    private Camera mainCamera = null;
    private Animator cameraAnimator = null;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject screenDim = null;
    [SerializeField] private GameObject combatReadout = null;
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
        cameraAnimator = mainCamera.GetComponent<Animator>();
        cursor = FindObjectOfType<CursorController>();
        dialogueHolder = FindObjectOfType<DialogueHolder>();
    }

    public void EngageAttack(UnitLoader attacker, UnitLoader defender)
    {
        StartCoroutine(Attack(attacker, defender));

        attackerHealth.maxValue = attacker.unit.statistics.health;
        attackerHealth.value = attacker.currentHealth;

        defenderHealth.maxValue = defender.unit.statistics.health;
        defenderHealth.value = defender.currentHealth;
    }
    private IEnumerator Attack(UnitLoader attacker, UnitLoader defender)
    {
        dialoguePlayed = false;

        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>()){
            if(unit.GetComponent<SpriteRenderer>().color == Color.red){
                unit.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        cursor.controls.Disable();
        cursor.GetComponent<Animator>().SetBool("Invisible", true);

        //Check if defender has dialogue for when it's attacked
        if(defender.attackedDialogue != null){
            MapDialogueManager.instance.WriteSingle(defender.attackedDialogue);
            yield return new WaitForSeconds(0.5f);
            defender.attackedDialogue = null;
        }
        yield return new WaitUntil(() => !screenDim.activeSelf);

        ActionCamera(attacker, defender);
        yield return new WaitUntil(() => mainCamera.orthographicSize == 4);
        attackerHealth.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasSpace(attacker.gameObject);
        defenderHealth.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasSpace(defender.gameObject);

        combatReadout.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        AttackAnimation(attacker, defender);
        yield return new WaitForSeconds(attacker.animator.GetCurrentAnimatorClipInfo(0).Length - 0.2f);        

        if(attacker.equippedWeapon.animation)
        {
            PlayEffect(attacker, defender);
            yield return new WaitForSeconds(attacker.equippedWeapon.animationLength.length);
        }

        InitiatorAttack(attacker, defender);
        yield return new WaitForSeconds(0.3f);

        if(CheckForDeaths(attacker, defender) == "Attacker")
        {
            attacker.DelayedDeath();
            combatReadout.SetActive(false);
            ResetCamera();
            if(attacker.unit.allyUnit)
            {
                cursor.controls.UI.Disable();
                cursor.controls.MapScene.Enable();
                cursor.SetState(new MapState(cursor));
            }
            yield return null;
        }
        else if(CheckForDeaths(attacker, defender) == "Defender")
        {
            //Checks if the defender has dialogue for when its defeated
            if(!dialoguePlayed && defender.defeatedDialogue != null){
                MapDialogueManager.instance.WriteSingle(defender.defeatedDialogue);
                dialoguePlayed = true;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitUntil(() => !screenDim.activeSelf);

            defender.DelayedDeath();
            combatReadout.SetActive(false);
            attacker.Rest();
            ResetCamera();
            if(attacker.unit.allyUnit)
            {
                cursor.controls.UI.Disable();
                cursor.controls.MapScene.Enable();
                cursor.SetState(new MapState(cursor));
            }
            yield return null;
        }
        else
        {
            if(CheckDistance(defender, attacker) <= 1 || defender.equippedWeapon.range >= attacker.equippedWeapon.range)
            {
                AttackAnimation(defender, attacker);
                yield return new WaitForSeconds((defender.animator.GetCurrentAnimatorClipInfo(0).Length - 0.2f));

                if(defender.equippedWeapon.animation)
                {
                    PlayEffect(defender, attacker); 
                    yield return new WaitForSeconds(defender.equippedWeapon.animationLength.length);
                }

                DefenderAttack(attacker, defender);
                yield return new WaitForSeconds(0.3f);
            }

            if(CheckForDeaths(attacker, defender) == "Attacker")
            {
                attacker.DelayedDeath();
                combatReadout.SetActive(false);
                ResetCamera();
                if (attacker.unit.allyUnit)
                {
                    cursor.controls.UI.Disable();
                    cursor.controls.MapScene.Enable();
                    cursor.SetState(new MapState(cursor));
                }
                yield return null;
            }
            else if(CheckForDeaths(attacker, defender) == "Defender")
            {
                //Checks if the defender has dialogue for when its defeated
                if(!dialoguePlayed && defender.defeatedDialogue != null){
                    MapDialogueManager.instance.WriteSingle(defender.defeatedDialogue);
                    dialoguePlayed = true;
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitUntil(() => !screenDim.activeSelf);

                defender.DelayedDeath();
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                if(attacker.unit.allyUnit)
                {
                    cursor.controls.UI.Disable();
                    cursor.controls.MapScene.Enable();
                    cursor.SetState(new MapState(cursor));
                }
                yield break;
            }
            else
            {
                if(CheckAttackSpeed(attacker, defender))
                {
                    AttackAnimation(attacker, defender);
                    yield return new WaitForSeconds(attacker.animator.GetCurrentAnimatorClipInfo(0).Length - 0.2f);

                    if(attacker.equippedWeapon.animation)
                    {
                        PlayEffect(attacker, defender);
                        yield return new WaitForSeconds(attacker.equippedWeapon.animationLength.length);
                    }

                    InitiatorAttack(attacker, defender);
                    yield return new WaitForSeconds(0.3f);

                    cursor.SetState(new MapState(cursor));
                    yield return null;
                }
                combatReadout.SetActive(false);
                attacker.Rest();
                ResetCamera();
                if(attacker.unit.allyUnit)
                {
                    cursor.controls.UI.Disable();
                    cursor.controls.MapScene.Enable();
                    cursor.SetState(new MapState(cursor));
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
            if (attacker.unit.allyUnit)
            {
                cursor.controls.UI.Disable();
                cursor.controls.MapScene.Enable();
                cursor.SetState(new MapState(cursor));
            }
            yield return null;
        }
        combatReadout.SetActive(false);
        if(CheckForDeaths(attacker, defender) == "Defender" || CheckForDeaths(attacker, defender) == null)
        {
            attacker.Rest();
        }
        ResetCamera();
        if(attacker.unit.allyUnit)
        {
            cursor.controls.UI.Disable();
            cursor.controls.MapScene.Enable();
            cursor.SetState(new MapState(cursor));
        }

        //This block checks if an Ally Unit is below 50% health, which if true triggers their battle dialogue
        if(attacker.unit.allyUnit && attacker.currentHealth < (attacker.unit.statistics.health * .5f) && attacker.currentHealth > 0 && !dialoguePlayed){
            MapDialogueManager.instance.WriteSingle(attacker.GetComponent<BattleDialogue>().under50Quote);
            dialoguePlayed = true;
        }
        else if(defender.unit.allyUnit && defender.currentHealth < (defender.unit.statistics.health * .5f) && defender.currentHealth > 0 && !dialoguePlayed){
            MapDialogueManager.instance.WriteSingle(defender.GetComponent<BattleDialogue>().under50Quote);
            dialoguePlayed = true;
        }

        if(TurnManager.instance.currentState.stateType == TurnState.StateType.Player)
        {
            cursor.GetComponent<Animator>().SetBool("Invisible", false);
        }        
        yield return null;
    }

    private void ActionCamera(UnitLoader attacker, UnitLoader defender)
    {
        var point1 = attacker.transform.position;
        var point2 = defender.transform.position;

        var centerPoint = (point1 + point2) / 2;
        Vector3 zoomPoint = new Vector3(centerPoint.x, centerPoint.y, -10);

        StartCoroutine(MoveCamera(zoomPoint));
        //mainCamera.transform.position = zoomPoint;
        //cameraAnimator.SetTrigger("ZoomIn");
    }
    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        while(mainCamera.transform.position != targetPosition)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, 7f * Time.deltaTime);
            yield return null;
        }
        cameraAnimator.SetTrigger("ZoomIn");
    }
    private void ResetCamera()
    {                
        cameraAnimator.SetTrigger("ZoomOut");  
    }
    
    private Vector3 WorldToCanvasSpace(GameObject unit)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 uiOffset = new Vector2((float)canvasRect.sizeDelta.x / 2f, (float)canvasRect.sizeDelta.y / 2f);
        Vector2 viewPortPosition = mainCamera.WorldToViewportPoint(unit.transform.position);
        Vector2 proportionalPosition = new Vector2(viewPortPosition.x * canvasRect.sizeDelta.x, viewPortPosition.y * canvasRect.sizeDelta.y);
        return proportionalPosition - uiOffset - new Vector2(0, 75);
    }
    private IEnumerator Shake(float intensity, bool attacker)
    {
        Vector3 originalPosition = new Vector3(0, 0, 0);
        GameObject healthBar = null;
        bool isShaking = true;
        var startTime = Time.time;
        
        //Determine which healthbar to shake
        if(attacker){
            originalPosition = defenderHealth.gameObject.transform.position;
            healthBar = defenderHealth.gameObject;
        }
        else{
            originalPosition = attackerHealth.gameObject.transform.position;
            healthBar = attackerHealth.gameObject;
        }

        //Determine intensity of shake
        if(intensity > 10){
            intensity = 0.4f;
        }
        else if(intensity < 10){
            intensity = 0.2f;
        }

        //Shake the healthbar
        while(isShaking && (Time.time - startTime) < 0.2f){
            healthBar.transform.position = originalPosition + Random.insideUnitSphere * intensity;
            yield return null;
        }

        //Resets healthbar position
        isShaking = false;
        if(attacker){
            defenderHealth.transform.position = originalPosition;
        }
        else{
            attackerHealth.transform.position = originalPosition;
        }
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
                SoundManager.instance.PlayFX(4);
                battleText.SetText("Crit");                
                Instantiate(battleText, defender.transform.position, Quaternion.identity);                
                defender.currentHealth = defender.currentHealth - Critical(attacker, defender);
                StartCoroutine(Shake(Critical(attacker, defender), true));
                defenderHealth.value = defender.currentHealth;

                //Display critial quote
                if (attacker.unit.allyUnit && !defender.defeatedDialogue)
                {
                    MapDialogueManager.instance.WriteSingle(attacker.GetComponent<BattleDialogue>().RandomCritQuote());
                    dialoguePlayed = true;
                }
            }
            else
            {
                //Unit Hits
                SoundManager.instance.PlayFX(3);
                battleText.SetText("Hit");
                Instantiate(battleText, defender.transform.position, Quaternion.identity);
                defender.currentHealth = defender.currentHealth - Hit(attacker, defender);
                StartCoroutine(Shake(Hit(attacker, defender), true));
                defenderHealth.value = defender.currentHealth;                
            }
        }
        else
        {
            //Unit Misses
            SoundManager.instance.PlayFX(5);
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
                    SoundManager.instance.PlayFX(4);
                    battleText.SetText("Crit");
                    Instantiate(battleText, attacker.transform.position, Quaternion.identity);
                    attacker.currentHealth = attacker.currentHealth - Critical(defender, attacker);
                    StartCoroutine(Shake(Critical(defender, attacker), false));
                    attackerHealth.value = attacker.currentHealth;
                    
                    //Display critial quote
                    if(defender.unit.allyUnit && !attacker.defeatedDialogue)
                    {
                        MapDialogueManager.instance.WriteSingle(defender.GetComponent<BattleDialogue>().RandomCritQuote());
                        dialoguePlayed = true;
                    }
                }
                else
                {
                    //Unit Hits
                    SoundManager.instance.PlayFX(3);
                    battleText.SetText("Hit");
                    Instantiate(battleText, attacker.transform.position, Quaternion.identity);                    
                    attacker.currentHealth = attacker.currentHealth - Hit(defender, attacker);
                    StartCoroutine(Shake(Hit(defender, attacker), false));
                    attackerHealth.value = attacker.currentHealth;
                }
            }
            else
            {
                //Unit Misses
                SoundManager.instance.PlayFX(5);
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
