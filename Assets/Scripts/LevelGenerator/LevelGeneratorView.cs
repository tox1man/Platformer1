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
        [Header("Procedural Generation Settings")]
        [Range(0f, 1f)]
        [SerializeField] private float _holesFrequency;
        [SerializeField] private int _holesMinimumWidth;
        [SerializeField] private int _holesMaximumWidth;

        [Header("Tilemaps")]
        [SerializeField] public Tilemap Boundaries;
        [SerializeField] public Tilemap Floor;
        [SerializeField] public Tilemap Enviroment;

        public Vector3Int MapSize => _mapSize;
        public Grid TileGrid => _tileGrid;
        public Tile[] TileSet => _tileSet;
        public float HolesFrequency => _holesFrequency;
        public int HolesMinWidth => _holesMinimumWidth;
        public int HolesMaxWidth => _holesMaximumWidth;


    }
}
