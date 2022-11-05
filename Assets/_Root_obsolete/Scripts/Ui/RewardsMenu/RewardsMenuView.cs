using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class RewardsMenuView : MonoBehaviour
    {
        [field: SerializeField] public RewardView DailyRewardView { get; private set; }
        [field: SerializeField] public RewardView WeeklyRewardView { get; private set; }

        [Header("Buttons")]
        public Button DailyRewardsButton;
        public Button WeeklyRewardsButton;

        private RewardController _dailyRewardController;
        private RewardController _weeklyRewardController;

        private RewardsMenuController _installController;

        public List<RewardController> RewardControllersList { get; private set; }


        private void Awake()
        {
            _dailyRewardController = new RewardController(DailyRewardView);
            _weeklyRewardController = new RewardController(WeeklyRewardView);
            _installController = new(this);
            RewardControllersListSet();
        }

        private void Start()
        {
            _dailyRewardController.Init();
            _weeklyRewardController.Init();
            _installController.Init();
            RewardControllersList.Clear();
        }

        private void OnDestroy()
        {
            _dailyRewardController.Deinit();
            _weeklyRewardController.Deinit();
            _installController.Deinit();
        }

        private void RewardControllersListSet()
        {
            RewardControllersList = new List<RewardController>
            {
                _dailyRewardController,
                _weeklyRewardController
            };
        }
    }
}
