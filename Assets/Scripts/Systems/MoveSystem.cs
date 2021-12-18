using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class MoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        private EcsFilter<ShipComponent, MovableComponent> ships;

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                MovableComponent movable = ships.Get2(index);
                ship.ship.transform.position += ship.ship.transform.forward * (movable.currentVelocity * Time.deltaTime);
            }
        }
    }
}