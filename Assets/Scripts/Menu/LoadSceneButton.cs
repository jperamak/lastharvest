using UnityEngine;

namespace Assets.Scripts.Menu
{
    [RequireComponent(typeof(GUITexture))]
    public class LoadSceneButton : ButtonBase
    {
        public string sceneName;

        protected override void OnButtonClicked()
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("Scene name not set");
                return;
            }

            Application.LoadLevel(sceneName);
        }
    }
}
