namespace Client
{
    /// <summary>
    /// ��������� ����� ��������� �����������
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