using Services.Ads.UnityAds;
using UnityEngine.Advertisements;

internal class RewardedPlayer : UnityAdsPlayer
{
    public RewardedPlayer(string id) : base(id) { }

    protected override void OnPlaying() => Advertisement.Show(Id);
    protected override void Load() => Advertisement.Load(Id);
}