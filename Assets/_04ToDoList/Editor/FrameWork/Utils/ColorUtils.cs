using System;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Utils
{
    public static class ColorUtils
    {
        public static string ToText(this Color color)
        {
            return $"{{r:{color.r},g:{color.g},b:{color.b},a:{color.a}}}";
        }

        public static Color ToColor(this string colorText)
        {
            try
            {
                int r0 = colorText.IndexOf(':', 0) + 1;
                int r1 = colorText.IndexOf(',', 0);

                int g0 = colorText.IndexOf(':', r0) + 1;
                int g1 = colorText.IndexOf(',', r1 + 1);

                int b0 = colorText.IndexOf(':', g0) + 1;
                int b1 = colorText.IndexOf(',', g1 + 1);

                int a0 = colorText.IndexOf(':', b0) + 1;
                int a1 = colorText.Length - 1;

                float r = float.Parse(colorText.Substring(r0, r1 - r0));
                float g = float.Parse(colorText.Substring(g0, g1 - g0));
                float b = float.Parse(colorText.Substring(b0, b1 - b0));
                float a = float.Parse(colorText.Substring(a0, a1 - a0));

                return new Color(r, g, b, a);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return Color.black;
            }
        }
    }
}