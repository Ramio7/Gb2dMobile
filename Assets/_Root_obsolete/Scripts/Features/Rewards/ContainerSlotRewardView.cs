using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textDays;
        [SerializeField] private TMP_Text _countReward;


        public void SetData(Reward reward, int countTime, bool isSelected)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            switch (reward.RewardType)
            {
                case RewardType.Daily:
                    _textDays.text = $"Day {countTime}";
                    break;
                case RewardType.Weekly:
                    _textDays.text = $"Week {countTime}";
                    break;
            }
            _countReward.text = reward.CountCurrency.ToString();

            UpdateBackground(isSelected);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}
