using UnityEngine;
namespace SiaX
{
    [CreateAssetMenu(fileName = "ColorPaletteManager", menuName = "ScriptableObjects/ColorPalette")]
    public class ColorPaletteManager : ScriptableObject
    {
        public Material defaultMaterial;
        public Material highlightMaterial;
        public Material selectedMaterial;
        public Material darkerMaterial;
        public Color defaultColor;
        public Color highlightColor;
        public Color selectedColor;
        public Color darkerColor;
        public Sprite logo;
    }
}
