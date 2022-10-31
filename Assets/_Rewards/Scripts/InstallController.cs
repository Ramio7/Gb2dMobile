using System.Collections.Generic;

namespace Rewards
{
    internal class InstallController
    {
        private readonly InstallView _installView;

        private bool _isInitialized;

        private List<RewardView> _rewardViews;

        public InstallController(InstallView installView)
        {
            _installView = installView;
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
            _installView.DailyRewardsButton.onClick.AddListener(SetDailyRewardView);
            _installView.WeeklyRewardsButton.onClick.AddListener(SetWeeklyRewardView);
        }

        private void UnsubscribeButtons()
        {
            _installView.DailyRewardsButton.onClick.RemoveListener(SetDailyRewardView);
            _installView.WeeklyRewardsButton.onClick.RemoveListener(SetWeeklyRewardView);
        }

        public void SetDailyRewardView()
        {
            SetAllViewsBackground();
            _rewardViews[0].MountRootSlotsReward.gameObject.SetActive(true);
            //_installView.RewardControllersList[0].SubscribeButtons();
        }

        private void SetWeeklyRewardView()
        {
            SetAllViewsBackground();
            _rewardViews[1].MountRootSlotsReward.gameObject.SetActive(true);
            //_installView.RewardControllersList[1].SubscribeButtons();
        }

        private void SetAllViewsBackground()
        {
            _installView.RewardControllersList.ForEach(controller => controller.UnsubscribeButtons());
            //_rewardViews.ForEach(rewardView => rewardView.gameObject.;
        }

        private void RewardViewsListInit()
        {
            _rewardViews = new()
            {
                _installView.DailyRewardView,
                _installView.WeeklyRewardView
            };
        }
    }
}