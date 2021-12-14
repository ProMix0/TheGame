using UnityEngine;

namespace Client {
    struct ShipComponent {
        public Transform transform;

        public int maxVelocity;
        public float currentVelocity;
        public int acceleration;

        public int maxRotateVelocity;
        public float currentRotateVelocity;
        public int rotateAcceleration;
    }
}