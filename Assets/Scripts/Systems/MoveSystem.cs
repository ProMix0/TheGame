using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class MoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        private EcsFilter<ShipComponent> ships;

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                ship.transform.position += ship.transform.forward * (ship.currentVelocity * Time.deltaTime);
            }
        }
    }
}