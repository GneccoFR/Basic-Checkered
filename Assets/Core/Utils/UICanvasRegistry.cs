using UnityEngine;

namespace Core.Utils
{
    public class UICanvasRegistry : MonoBehaviour
    {
        public static Canvas MainCanvas { get; private set; }

        public static void Register(Canvas canvas)
        {
            MainCanvas = canvas;
        }
    }
}
