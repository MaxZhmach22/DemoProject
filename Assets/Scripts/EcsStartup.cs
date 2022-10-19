using DemoProject.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UniRx;
using UnityEngine;
using Zenject;


namespace DemoProject {
    public sealed class EcsStartup : MonoBehaviour 
    {
        private EcsWorld _world;        
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _initSystems;
        private DynamicJoystick _dynamicJoystick;
        private PlayerSettings _playerSettings;
        private PlayerView _playerView;
        private PlayerAnimatorView _playerAnimatorView;
        private GroundChecker _groundChecker;
        private FishingRodHandView _fishingRodHandView;
        private FishingRodSpineView _fishingRodSpineView;
        private HookView _hookView;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        [Inject(Id = "Iceland")] private Transform _iceland;

        [Inject]
        private void Init(
            DynamicJoystick dynamicJoystick, 
            PlayerSettings playerSettings,
            PlayerView playerView,
            PlayerAnimatorView playerAnimatorView,
            GroundChecker groundChecker,
            FishingRodHandView fishingRodHandView,
            FishingRodSpineView fishingRodSpineView,
            HookView hookView)
        {
            _dynamicJoystick = dynamicJoystick;
            _playerSettings = playerSettings;
            _playerView = playerView;
            _playerAnimatorView = playerAnimatorView;
            _groundChecker = groundChecker;
            _fishingRodHandView = fishingRodHandView;
            _fishingRodSpineView = fishingRodSpineView;
            _hookView = hookView;
        }
        
        void Start () 
        {
            _world = new EcsWorld ();

            _initSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems (_world);

            AddInitSystems();
            AddFixedUpdateSystems();
            AddRunSystems();
            
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Init ();
        }

        private void AddInitSystems()
        {
            _initSystems
                .Add(new UnitTransformInitSystem())
                .Add(new JoystickInputInit(_dynamicJoystick))
                .Add(new DoubleTapCheckSystem(_disposable))
                .Add(new PlayerInitSystem(_playerView))
                .Add(new AnimatorInitSystem(_playerAnimatorView))
                .Add(new IcelandSwingSystem(_iceland))
                .Inject();
        }
        
        private void AddRunSystems()
        {
            _updateSystems
                .Add(new JoystickRunSystem(_dynamicJoystick))
                .Add(new AnimatorRunSystem())
                .Add(new PlayerMovementSystem())
                .Add(new FishingRodSwitcherSystem())
                .Add(new HookMoveSystem())
                .Inject()
                .Inject(_playerSettings, _fishingRodHandView, _fishingRodSpineView, _hookView);

        }

        private void AddFixedUpdateSystems()
        {
            _fixedUpdateSystems
                .Add(new GroundCheckSystem())
                .Add(new DoubleClickRaycastSystem())
                .Inject()
                .Inject(_playerSettings, _groundChecker);
        }

        void Update() 
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy () 
        {
            _disposable.Clear();
            
            if (_initSystems != null) 
            {
                _initSystems.Destroy ();
                _initSystems = null;
            }
            
            if (_fixedUpdateSystems != null) 
            {
                _fixedUpdateSystems.Destroy ();
                _fixedUpdateSystems = null;
            }
            
            if (_updateSystems != null) 
            {
                _updateSystems.Destroy ();
                _updateSystems = null;
            }
            
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}