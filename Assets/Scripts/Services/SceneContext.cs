using NaughtyAttributes;
using UnityEngine;
using Zenject;


namespace DemoProject
{
    public class SceneContext : MonoInstaller
    {
        [field: BoxGroup("Input References")] [field: SerializeField] public DynamicJoystick DynamicJoystick { get; private set; }
        [field: BoxGroup("Player References")] [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
        [field: BoxGroup("Player References")] [field: SerializeField] public PlayerView PlayerView { get; private set; }

        public override void InstallBindings()
        {
            InputReferenceInit();
            PlayerSettingsInit();




        }

        private void PlayerSettingsInit()
        {
            if (!PlayerSettings)
            {
                PlayerSettings = FindObjectOfType<PlayerSettings>();
            }
            
            if (!PlayerView)
            {
                PlayerView = FindObjectOfType<PlayerView>();
            }

            Container.Bind<PlayerSettings>().FromInstance(PlayerSettings).AsSingle();
            Container.Bind<PlayerView>().FromInstance(PlayerView).AsSingle();
        }

        private void InputReferenceInit()
        {
            if (!DynamicJoystick)
            {
                DynamicJoystick = FindObjectOfType<DynamicJoystick>();
            }

            Container.Bind<DynamicJoystick>().FromInstance(DynamicJoystick).AsSingle();
        }
    }
}