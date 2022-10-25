using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace DemoProject
{
    public class GameScreen : MonoBehaviour
    {
        [field: Foldout("References")] [field: SerializeField] public Button Button { get; private set; }

        private void Awake()
        {
            Button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var scene = SceneManager.GetActiveScene();
                    SceneManager.LoadSceneAsync(scene.name);
                })
                .AddTo(this);
        }
    }
}