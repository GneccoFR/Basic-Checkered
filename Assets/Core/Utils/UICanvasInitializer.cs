using UnityEngine;

namespace Core.Utils
{
    public class UICanvasInitializer : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        private void Awake()
        {
            UICanvasRegistry.Register(canvas);
        }
    }
}
