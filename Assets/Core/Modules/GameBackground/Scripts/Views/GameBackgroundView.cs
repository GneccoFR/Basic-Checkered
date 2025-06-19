using UnityEngine;

namespace Core.Modules.GameBackground.Scripts.Views
{
    public class GameBackgroundView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer myRenderer;
        [SerializeField] private float offsetEffectBrake;

        void Update()
        {
            Material myMaterial = myRenderer.material;
            Vector2 offset = myMaterial.mainTextureOffset;
            offset.y += Time.deltaTime/offsetEffectBrake;
            myMaterial.mainTextureOffset = offset;
        }
    }
}
