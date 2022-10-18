using System;
using DemoProject.Input;
using DemoProject.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
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

        [Inject]
        private void Init(
            DynamicJoystick dynamicJoystick, 
            PlayerSettings playerSettings,
            PlayerView playerView,
            PlayerAnimatorView playerAnimatorView)
        {
            _dynamicJoystick = dynamicJoystick;
            _playerSettings = playerSettings;
            _playerView = playerView;
            _playerAnimatorView = playerAnimatorView;
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
                .Add(new PlayerInitSystem(_playerView))
                .Add(new AnimatorInitSystem(_playerAnimatorView))
                .Inject();
        }
        
        private void AddRunSystems()
        {
            _updateSystems
                .Add(new JoystickRunSystem(_dynamicJoystick))
                .Add(new AnimatorRunSystem())
                .Inject();

        }

        private void AddFixedUpdateSystems()
        {
            _fixedUpdateSystems
                .Inject();
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