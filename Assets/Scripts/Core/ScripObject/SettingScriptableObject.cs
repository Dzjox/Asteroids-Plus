using UnityEngine;

namespace AsteroidsPlus.ScripObject
{
    [CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
    public class SettingScriptableObject : ScriptableObject
    {
        [Header("Ship")]
        public GameObject ShipPrefab;
        public Vector2 ShipStartPosiotion = Vector2.zero;
        public float ShipStartRotation = 0;
        public float ShipStartSpeed = 0;
        public float ShipAcceleration = 0.25f;
        public float ShipRotationSpeed = 100;
        public float ShipMaxSpeed = 5;
        [Header("Cannon")]
        public float CannonDelayTime = 0.05f;
        [Header("Missle")]
        public GameObject MisslePrefab;
        public float MissleSpeed = 10;
        public float MissleFlyTime = 1f;
        [Header("Laser")]
        public int LaserMaxCharges = 3;
        public int LaserAddChargeTime = 10;
        [Header("LaserBeam")]
        public GameObject LaserBeamPrefab;
        public float LaserBeamTime = 1f;
        public float LaserBeamLengthScale = 10;
        public Vector3 LaserBeamOffset = new Vector3(0, 13, 0);
        [Header("Asteroids")]
        public GameObject AsteroidPrefab;
        public float AsteroidsMaxSpeed = 3;
        public int AsteroidsInIdle = 10;
        public int AsteroidsInFirstRound = 2;
        public float AsteroidsArrivalTime = 5;
        public float AsteroidsCountWithTime = 0.1f;
        public float AsteroidsSafeZoneRadius = 1.5f;
        [Header("MiniAsteroids")]
        public int MiniAsteroidsCount = 3;
        public float MiniAsteroidsScale = 0.3f;
        [Header("Score")]
        public int ScoreAsteroidPriсe = 120;
        public int ScoreUFOPriсe = 300;
        [Header("UFO")]
        public GameObject UFOPrefab;
        public float UFOSpeed = 1;
        public float UFOSpawnTime = 10;
    }
}
