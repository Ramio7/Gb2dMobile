using System.Collections.Generic;

namespace Rewards
{
    internal class RewardsMenuController
    {
        private readonly RewardsMenuView _rewardsMenuView;

        private bool _isInitialized;

        private List<RewardView> _rewardViews;

        public RewardsMenuController(RewardsMenuView rewardsMenuView)
        {
            _rewardsMenuView = rewardsMenuView;
        }

        public void Init()
        {
            if (_isInitialized) return;

            SubscribeButtons();
            RewardViewsListInit();

            _isInitialized = true;
        }

        public void Deinit()
        {
            if (!_isInitialized) return;

            UnsubscribeButtons();
            _rewardViews.Clear();

            _isInitialized = false;
        }

        private void SubscribeButtons()
        {
            _rewardsMenuView.DailyRewardsButton.onClick.AddListener(SetDailyRewardView);
            _rewardsMenuView.WeeklyRewardsButton.onClick.AddListener(SetWeeklyRewardView);
        }

        private void UnsubscribeButtons()
        {
            _rewardsMenuView.DailyRewardsButton.onClick.RemoveListener(SetDailyRewardView);
            _rewardsMenuView.WeeklyRewardsButton.onClick.RemoveListener(SetWeeklyRewardView);
        }

        public void SetDailyRewardView()
        {
            SetAllViewsBackground();
            _rewardViews[0].MountRootSlotsReward.gameObject.SetActive(true);
            //_rewardsMenuView.RewardControllersList[0].SubscribeButtons();
        }

        private void SetWeeklyRewardView()
        {
            SetAllViewsBackground();
            _rewardViews[1].MountRootSlotsReward.gameObject.SetActive(true);
            //_rewardsMenuView.RewardControllersList[1].SubscribeButtons();
        }

        private void SetAllViewsBackground()
        {
            _rewardsMenuView.RewardControllersList.ForEach(controller => controller.UnsubscribeButtons());
            //_rewardViews.ForEach(rewardView => rewardView.gameObject.;
        }

        private void RewardViewsListInit()
        {
            _rewardViews = new()
            {
                _rewardsMenuView.DailyRewardView,
                _rewardsMenuView.WeeklyRewardView
            };
        }
    }
}