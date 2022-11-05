using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : MonoBehaviour
{
    [field: Header("Player Stats")]
    [field: SerializeField] public TMP_Text CountMoneyText { get; private set; }
    [field: SerializeField] public TMP_Text CountHealthText { get; private set; }
    [field: SerializeField] public TMP_Text CountPowerText { get; private set; }
    [field: SerializeField] public TMP_Text SecurityStatusText { get; private set; }
                           
    [field: Header("Enemy Stats")]
    [field: SerializeField] public TMP_Text CountPowerEnemyText { get; private set; }
                             
    [field: Header("Money Buttons")]
    [field: SerializeField]public Button AddMoneyButton { get; private set; }
    [field: SerializeField] public Button SubstractMoneyButton { get; private set; }
                              
    [field: Header("Health Buttons")]
    [field: SerializeField] public Button AddHealthButton { get; private set; }
    [field: SerializeField] public Button SubstractHealthButton { get; private set; }
                             
    [field: Header("Power Buttons")]
    [field: SerializeField] public Button AddPowerButton { get; private set; }
    [field: SerializeField] public Button SubstractPowerButton { get; private set; }
                                       
    [field: Header("Security Status Buttons")]
    [field: SerializeField] public Button AddSecurityStatusButton { get; private set; }
    [field: SerializeField] public Button SubstractSecurityStatusButton { get; private set; }

    [field: Header("Other Buttons")]
    [field: SerializeField] public Button FightButton { get; private set; }
    [field: SerializeField] public Button EscapeTheFightButton { get; private set; }
}
