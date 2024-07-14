using UnityEngine.Events;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddListener(this Button value, UnityAction call)
    {
        value.onClick.AddListener(call);
    }

    public static void RemoveListener(this Button value, UnityAction call)
    {
        value.onClick.RemoveListener(call);
    }
}