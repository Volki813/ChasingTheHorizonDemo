using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This was used to test highlighting buttons without selecting them
//All the Combat Preview data will be moved here in the future, instead of it being handled in the ActionMenuManager
public class CombatPreview : MonoBehaviour
{
    [Header("Attacker Preview")]
    public Image attackerPortrait = null;
    public Text attackerHP = null;
    public Text attackerDamage = null;
    public Text attackerHit = null;
    public Text attackerCrit = null;
    [Header("Defender Preview")]
    public Image defenderPortrait = null;
    public Text defenderHP = null;
    public Text defenderDamage = null;
    public Text defenderHit = null;
    public Text defenderCrit = null;

    CursorController cursor;
    [SerializeField] private Button backButton = null;

    private void Awake()
    {
        cursor = FindObjectOfType<CursorController>();
    }

    private void OnEnable()
    {
        StartCoroutine(HighlightBackButton());
        FillPreview();
    }

    private void OnDisable()
    {
        attackerPortrait.sprite = null;
        attackerHP.text = null;
        attackerDamage.text = null;
        attackerHit.text = null;
        attackerCrit.text = null;

        defenderPortrait.sprite = null;
        defenderHP.text = null;
        defenderDamage.text = null;
        defenderHit.text = null;
        defenderCrit.text = null;
    }

    private IEnumerator HighlightBackButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }
    private void FillPreview()
    {
        attackerPortrait.sprite = cursor.selectedUnit.unit.portrait;
        attackerHP.text = cursor.selectedUnit.currentHealth.ToString();
        attackerDamage.text = CombatManager.instance.Hit(cursor.selectedUnit, cursor.selectedUnit.target).ToString();
        attackerHit.text = (cursor.selectedUnit.CombatStatistics().hit - cursor.selectedUnit.target.CombatStatistics().avoid).ToString();
        attackerCrit.text = cursor.selectedUnit.CombatStatistics().crit.ToString();

        defenderPortrait.sprite = cursor.selectedUnit.target.unit.portrait;
        defenderHP.text = cursor.selectedUnit.target.currentHealth.ToString();
        defenderDamage.text = CombatManager.instance.Hit(cursor.selectedUnit.target, cursor.selectedUnit).ToString();
        defenderHit.text = (cursor.selectedUnit.target.CombatStatistics().hit - cursor.selectedUnit.CombatStatistics().avoid).ToString();
        defenderCrit.text = cursor.selectedUnit.target.CombatStatistics().crit.ToString();
    }
}
