using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class PlaceTurretSystem : IEcsRunSystem {
        // auto-injected fields.
        private EcsWorld world ;
        private StaticData staticData;
        private SceneData sceneData;
        
        public void Run () {

            if (!Input.GetMouseButtonDown(0)) return;

            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = sceneData.camera.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 position= ray.GetPoint(distance);

                EcsEntity turret = world.NewEntity();

                GameObject gameObject = Object.Instantiate(staticData.turret);
                gameObject.transform.position = position;

                Utility.Bind(gameObject, turret);

                ref TurretComponent turretComponent = ref turret.Get<TurretComponent>();
                turretComponent.range = 50;
                turretComponent.damage = 1;
                turretComponent.firerate = 1;
                turretComponent.lastShootTime = 0;
            }
        }
    }
}