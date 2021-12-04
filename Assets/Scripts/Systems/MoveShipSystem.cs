using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class MoveShipSystem : IEcsRunSystem
    {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        private EcsFilter<ShipComponent, InputEvent> ships;

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                InputEvent input = ships.Get2(index);
                ship.transform.position += new Vector3(input.direction.x, 0, input.direction.y);
            }
        }
    }
}