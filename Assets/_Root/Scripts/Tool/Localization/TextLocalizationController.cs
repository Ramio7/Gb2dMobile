using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Tool.Localization.Examples
{
    internal class TextLocalizationController
    {
        private StringTableCollection _stringTableCollection;
        private LocaleIdentifier _currentLocaleId;
        private TMP_Text[] _localizableText;

        public TextLocalizationController(StringTableCollection stringTableCollection, LocaleIdentifier currentLocaleId, TMP_Text[] localizableText)
        {
            _stringTableCollection = stringTableCollection;
            _currentLocaleId = currentLocaleId;
            _localizableText = localizableText;
        }

        public void Init()
        {
            AddNewStringTableEntriesFromView();
        }

        public void Deinit()
        {
            _stringTableCollection.ClearAllEntries();
        }

        private void AddNewStringTableEntriesFromView()
        {
            foreach (TMP_Text tMP_Text in _localizableText)
            {
                _stringTableCollection.StringTables[0].AddEntry(tMP_Text.name, tMP_Text.text);
            }
        }

        private void UpdateText()
        {
            foreach (var item in _localizableText)
            {

            }
        }
    }
}