using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Tool.Localization.Examples
{
    internal class TextLocalizationView : LocalizationWindow
    {
        [SerializeField] private TMP_Text[] _localizableText;

        [SerializeField] private StringTableCollection _stringTableCollection;
        [SerializeField] private LocaleIdentifier _startingLocaleId;

        private TextLocalizationController _controller;

        protected override void OnStarted()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            _controller = new(_stringTableCollection, _startingLocaleId, _localizableText);
            _controller.Init();
        }

        protected override void OnDestroyed()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
            _controller.Deinit();
            _controller= null;
        }
        private void OnSelectedLocaleChanged(Locale _)
        {
            ;
        }

        //private void UpdateTextAsync() =>
            //LocalizationSettings.StringDatabase.GetTableAsync(_tableName).Completed +=
            //    handle =>
            //    {
            //        if (handle.Status == AsyncOperationStatus.Succeeded)
            //        {
            //            StringTable table = handle.Result;
            //            _changeText.text = table.GetEntry(_localizationTag)?.GetLocalizedString();
            //        }
            //        else
            //        {
            //            string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
            //            Debug.LogError(errorMessage);
            //        }
            //    };
    }
}