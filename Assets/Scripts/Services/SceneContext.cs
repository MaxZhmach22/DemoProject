using System.Linq;
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
        [field: BoxGroup("Player References")] [field: SerializeField] public PlayerAnimatorView AnimatorView { get; private set; }
        [field: BoxGroup("Player References")] [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
        [field: BoxGroup("Iceland References")] [field: SerializeField] public Transform IcelandTransform { get; private set; }
        [field: BoxGroup("Fishing References")] [field: SerializeField] public FishingRodHandView FishingRodHandView { get; private set; }
        [field: BoxGroup("Fishing References")] [field: SerializeField] public FishingRodSpineView FishingRodSpineView { get; private set; }
        [field: BoxGroup("Fishing References")] [field: SerializeField] public HookView HookView { get; private set; }

        public override void InstallBindings()
        {
            InputReferenceInit();
            PlayerSettingsInit();
            FishingRodInit();
        }

        private void PlayerSettingsInit()
        {
            if (!PlayerSettings)
            {
                PlayerSettings = FindObjectOfType<PlayerSettings>(true);
            }
            
            if (!PlayerView)
            {
                PlayerView = FindObjectOfType<PlayerView>(true);
            }

            if (!AnimatorView)
            {
                AnimatorView = FindObjectOfType<PlayerAnimatorView>(true);
            }

            if (!GroundChecker)
            {
                GroundChecker = FindObjectOfType<GroundChecker>(true);
            }

            if (IcelandTransform)
            {
                IcelandTransform = FindObjectsOfType<Transform>().First(x => x.name == "Iceland");
            }

            Container.Bind<Transform>().WithId("Iceland").FromInstance(IcelandTransform).AsSingle();
            Container.Bind<GroundChecker>().FromInstance(GroundChecker).AsSingle();
            Container.Bind<PlayerSettings>().FromInstance(PlayerSettings).AsSingle();
            Container.Bind<PlayerView>().FromInstance(PlayerView).AsSingle();
            Container.Bind<PlayerAnimatorView>().FromInstance(AnimatorView).AsSingle();
        }

        private void InputReferenceInit()
        {
            if (!DynamicJoystick)
            {
                DynamicJoystick = FindObjectOfType<DynamicJoystick>(true);
            }

            Container.Bind<DynamicJoystick>().FromInstance(DynamicJoystick).AsSingle();
        }

        private void FishingRodInit()
        {
            if (!FishingRodHandView)
            {
                FishingRodHandView = FindObjectOfType<FishingRodHandView>(true);
            }

            if (!FishingRodSpineView)
            {
                FishingRodSpineView = FindObjectOfType<FishingRodSpineView>(true);
            }

            if (!HookView)
            {
                HookView = FindObjectOfType<HookView>(true);
            }

            Container.Bind<HookView>().FromInstance(HookView).AsSingle();
            Container.Bind<FishingRodHandView>().FromInstance(FishingRodHandView).AsSingle();
            Container.Bind<FishingRodSpineView>().FromInstance(FishingRodSpineView).AsSingle();
        }
    }
}