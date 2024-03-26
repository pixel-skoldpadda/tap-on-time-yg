using System;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class Translation
    {
        [SerializeField] private Language language;
        [SerializeField] private string value;

        public Language Language => language;
        public string Value => value;
    }
}