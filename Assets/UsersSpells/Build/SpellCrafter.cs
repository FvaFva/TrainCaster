using UnityEngine;

public class SpellCrafter : MonoBehaviour
{
    public ISpellBuild Craft()
    {
        return new CraftedSpell();
    }
}