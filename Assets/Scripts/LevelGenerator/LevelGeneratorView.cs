using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mario
{
    public class LevelGeneratorView : MonoBehaviour
    {
        [SerializeField] private Vector3Int _mapSize;
        [SerializeField] private Grid _tileGrid;
        [SerializeField] private Tile[] _tileSet;
        [Header("Level Object Prefabs")]
        [SerializeField] private GameObject _boxPrefab;
        [SerializeField] private GameObject _boxParentObject;
        [SerializeField] private GameObject _flagSceneObject;
        [Header("Procedural Generation Settings")]
        [Range(0f, 1f)]
        [SerializeField] private float _holesFrequency;
        [Range(0f, 1f)]
        [SerializeField] private float _brickFrequency;
        [Range(0f, 0.1f)]
        [SerializeField] private float _boxFrequency;
        [SerializeField] private int _floorLevel;
        [SerializeField] private int _holesMinimumWidth;
        [SerializeField] private int _holesMaximumWidth;
        [SerializeField] private int _brickLayers;

        [Header("Tilemaps")]
        [SerializeField] public Tilemap Boundaries;
        [SerializeField] public Tilemap Floor;
        [SerializeField] public Tilemap Enviroment;

        public Vector3Int MapSize => _mapSize;
        public Grid TileGrid => _tileGrid;
        public Tile[] TileSet => _tileSet;
        public GameObject BoxPrefab => _boxPrefab;
        public GameObject BoxParentObject => _boxParentObject;        
        public GameObject FlagSceneObject => _flagSceneObject;
        public float HolesFrequency => _holesFrequency;
        public float BrickFrequency => _brickFrequency;
        public float BoxFrequency => _boxFrequency;
        public int HolesMinWidth => _holesMinimumWidth;
        public int HolesMaxWidth => _holesMaximumWidth;
        public int FloorLevel => _floorLevel;
        public int BrickLayers => _brickLayers;


    }
}
