using System.Collections;
using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        public StaticData _config;
        public SceneData _scene;
        public RuntimeData _runtime = new RuntimeData();
        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            Service<RuntimeData>.Set(_runtime);
            Service<EcsWorld>.Set(_world);
            Service<StaticData>.Set(_config);
            Service<SceneData>.Set(_scene);

            UI _ui = GetOrSetUI(_config);

            _systems
                .Add(new InitializeSystem())

                .Add(new WinSystem())
                .Add(new LoseSystem())
                .Add(new ChangeGameStateSystem())
                .Add(new InteractSystem())
                
                
                .Add(new InputSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerSpeedSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerJumpSystem())
                .Add(new PlayerRotateSystem())

                .Add(new MoveCameraToPlayerSystem())
                .Add(new TimerSystem())

                .OneFrame<JumpFlag>()
                .OneFrame<SprintFlag>()
                .OneFrame<InteractEvent>()
                .Inject(_runtime)
                .Inject(_config)
                .Inject(_scene)
                .Inject(_ui)
                .Init();
            
        }

        public static UI GetOrSetUI(StaticData staticData)
        {
            var ui = Service<UI>.Get();
            if (!ui)
            {
                ui = Instantiate(staticData.UIPrefab);
                Service<UI>.Set(ui);
            }

            return ui;
        }

        void Update() => _systems?.Run();

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}