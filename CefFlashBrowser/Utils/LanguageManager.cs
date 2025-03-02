﻿using CefFlashBrowser.Models.Data;
using SimpleMvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CefFlashBrowser.Utils
{
    public static class LanguageManager
    {
        private static Dictionary<string, ResourceDictionary> LanguageDictionaries { get; }

        static LanguageManager()
        {
            var languages = new ResourceDictionary
            { Source = new Uri("Assets/Language/langs.xaml", UriKind.Relative) };

            LanguageDictionaries = new Dictionary<string, ResourceDictionary>();
            foreach (var langDic in (ResourceDictionary[])languages["SupportedLanguages"])
            {
                string lang = System.IO.Path.GetFileNameWithoutExtension(langDic.Source.ToString());
                LanguageDictionaries.Add(lang, langDic);
            }

            CurrentLanguage = GlobalData.Settings.Language;
        }




        private static ResourceDictionary GetCurLangResDic()
        {
            return Application.Current.Resources.MergedDictionaries[0];
        }

        private static void SetCurLangResDic(ResourceDictionary dic)
        {
            Application.Current.Resources.MergedDictionaries[0] = dic;
        }




        public static IEnumerable<string> GetSupportedLanguage()
        {
            return from item in LanguageDictionaries orderby GetLanguageName(item.Key) select item.Key;
        }

        public static bool IsSupportedLanguage(string language)
        {
            return LanguageDictionaries.ContainsKey(language);
        }

        public static string CurrentLanguage
        {
            get
            {
                string url = GetCurLangResDic().Source.ToString();
                return System.IO.Path.GetFileNameWithoutExtension(url);
            }
            set
            {
                if (IsSupportedLanguage(value))
                {
                    SetCurLangResDic(LanguageDictionaries[value]);
                    GlobalData.Settings.Language = value;
                    Messenger.Global.Send(MessageTokens.LANGUAGE_CHANGED, value);
                }
            }
        }




        public static string GetString(string language, string key)
        {
            if (key != null && IsSupportedLanguage(language))
            {
                var dic = LanguageDictionaries[language];
                return dic.Contains(key) ? dic[key].ToString() : string.Empty;
            }
            else
            {
                return null;
            }
        }

        public static string GetString(string key)
        {
            return GetString(GlobalData.Settings.Language, key);
        }

        public static string GetLanguageName(string language)
        {
            return GetString(language, "language_name");
        }

        public static string GetLocale(string language)
        {
            return GetString(language, "locale");
        }
    }
}
