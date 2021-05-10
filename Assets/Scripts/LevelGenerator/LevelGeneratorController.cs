using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mario
{
    public class LevelGeneratorController
    {
        private LevelGeneratorView _view;
        private Vector3Int _mapSize;
        private int[,] _map;
        public LevelGeneratorController(LevelGeneratorView levelGeneratorView)
        {
            _view = levelGeneratorView;
            _mapSize = _view.MapSize;
            _map = new int[_mapSize.x, _mapSize.y];
        }

        public void Awake()
        {
            DrawBoundaries();
            GenerateFloor(5);
        }

        private void GenerateFloor(int floorLevel)
        {
            Tile _floorTile = _view.TileSet[22];
            int holeWidth = 0;

            for (int i = 0; i < _mapSize.x; i ++)
            {
                if (holeWidth == 0)
                {
                    for (int j = 0; j < floorLevel; j++)
                    {
                        _view.Floor.SetTile(new Vector3Int(i, j, 0), _floorTile);
                    }
                }
                if (UnityEngine.Random.value <= _view.HolesFrequency && holeWidth == 0)
                {
                    holeWidth = UnityEngine.Random.Range(_view.HolesMinWidth, _view.HolesMaxWidth + 1);
                    Debug.Log(holeWidth);
                }
                if (holeWidth == 0)
                {
                    for (int j = 0; j < floorLevel; j++)
                    {
                        _view.Floor.SetTile(new Vector3Int(i, j, 0), _floorTile);
                    }
                }
                else
                {
                    holeWidth--;
                }
            }
        }

        private void DrawBoundaries()
        {
            
            Tile _boundaryTile = _view.TileSet[23];

            for (int i = -1; i <= _mapSize.x; i++)
            {
                _view.Boundaries.SetTile(new Vector3Int(i, -1,        0),       _boundaryTile);
                _view.Boundaries.SetTile(new Vector3Int(i, _mapSize.y, 0),      _boundaryTile);
            }
            for (int j = -1; j <= _mapSize.y; j++)
            {
                _view.Boundaries.SetTile(new Vector3Int(-1,        j, 0),       _boundaryTile);
                _view.Boundaries.SetTile(new Vector3Int(_mapSize.x, j, 0),      _boundaryTile);
            }
        }
    }
}
