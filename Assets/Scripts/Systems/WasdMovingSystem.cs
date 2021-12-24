using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class WasdMovingSystem : IEcsRunSystem
    {
        private EcsFilter<WasdMovableComponent, GameObjectComponent> toMove;

        public void Run()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 forward = Vector3.right * (vertical * Time.deltaTime);
            Vector3 right = Vector3.back * (horizontal * Time.deltaTime);
            foreach (var index in toMove)
            {
                float speed = toMove.Get1(index).speed;
                toMove.Get2(index).gameObject.transform.position += (forward + right) * speed;
            }
        }
    }
}