using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Компонент, представляющий информацию о перемещении
    /// </summary>
    struct MovableComponent
    {
        public int maxSpeed;
        public Vector3 destination;
    }
}
