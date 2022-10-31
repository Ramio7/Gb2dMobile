﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class RewardView : MonoBehaviour
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);

        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [field: Header("Rewards list")]

        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public List<Reward> Rewards { get; private set; }

        [field: Header("Time properties")]
        [field: SerializeField] public float TimeCooldown { get; private set; }
        [field: SerializeField] public float TimeDeadline { get; private set; }
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }

        [field: Header("UI properties")]
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set;  }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
    }
}