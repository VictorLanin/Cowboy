using System;
using System.Text;
using UnityEngine;

namespace Resources.Scripts
{
    public static class StringManipulator
    {
        public static string GetProperName(string nameToChange)
        {
            StringBuilder _sb = new StringBuilder(nameToChange);
            if (nameToChange.IndexOf('(') == -1) return _sb.ToString();
            var index = nameToChange.IndexOf('(');
            _sb.Remove(index, nameToChange.Length - index);
            if (char.IsWhiteSpace(_sb[_sb.Length - 1]))
            {
                _sb.Remove(_sb.Length - 1, 1);
            }
            return _sb.ToString();
        }

        public static int GetNumberFromName(string nameToChange)
        {
            var sbuilder = new StringBuilder();
            
            foreach (var nameChar in nameToChange)
            {
                if(!char.IsDigit(nameChar)) continue;
                sbuilder.Append(nameChar);
            }
            return int.Parse(sbuilder.ToString());
        }
    }
}