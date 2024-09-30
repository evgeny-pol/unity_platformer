using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InputAxisAttribute))]
public class InputAxisAttributeDrawer : StringAttributePopupDrawer
{
    private static readonly string[] s_values = new string[]
    {
        InputAxis.Horizontal,
        InputAxis.Jump,
        InputAxis.HeroAbility1
    };

    protected override string[] Values => s_values;
}
