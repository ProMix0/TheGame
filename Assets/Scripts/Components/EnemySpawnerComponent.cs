namespace Client
{
    /// <summary>
    /// Компонент точки появления противников
    /// </summary>
    struct EnemySpawnerComponent
    {
        public ShipData shipData;

        public int spawnLimit;
        public int spawned;

        public int spawnPeriod;
        public float lastSpawnTime;
    }
}