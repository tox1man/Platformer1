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
            // DebugDrawBoundaries();
            GenerateFloor(_view.FloorLevel, true);
            GenerateBricks(_view.FloorLevel);
            GenerateFlag(_view.FloorLevel);
        }

        private void GenerateFloor(int floorLevel, bool createSafeZone)
        {
            Tile _floorTile = _view.TileSet[22];
            int holeWidth = 0;

            if (createSafeZone)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < floorLevel; j++)
                    {
                        _view.Floor.SetTile(new Vector3Int(i, j, 0), _floorTile);
                    }
                }
            }
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
                    holeWidth = UnityEngine.Random.Range(_view.HolesMinWidth + 1, _view.HolesMaxWidth + 1);
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

        private void GenerateBricks(int floorLevel)
        {
            Tile brickTile = _view.TileSet[1];
            int yBoxOffset = 4;
            int coinsRemaining = _view.NumberOfCoins;


            for (int j = 0; j < _view.BrickLayers; j++)
            {
                for (int i = 0; i < _mapSize.x; i++)
                {
                    var randomValue = UnityEngine.Random.value;
                    if (randomValue <= _view.BrickFrequency / (j + 1))
                    {
                        Vector3Int TilePosition = new Vector3Int(i, floorLevel + 2 + j, 0);
                        _view.Enviroment.SetTile(TilePosition, brickTile);

                        if (coinsRemaining > 0)
                        {
                            GenerateCoin(TilePosition + new Vector3Int(0, yBoxOffset, 0));
                            coinsRemaining--;
                            Debug.Log(TilePosition);
                        }

                        if (UnityEngine.Random.value <= _view.BoxFrequency)
                        {
                            GameObject.Instantiate(_view.BoxPrefab, new Vector3Int(i, floorLevel + j + yBoxOffset, 0), Quaternion.identity, _view.BoxParentObject.transform);
                        }
                    }
                }
            }
        }

        private void GenerateFlag(int floorLevel)
        {
            Vector3Int flagPos = new Vector3Int(_mapSize.x - 2, floorLevel + 1, 0);
            Tile floorTile = _view.TileSet[22];
            GameObject flag = _view.FlagSceneObject;

            if (_view.Enviroment.GetTile(flagPos + Vector3Int.down) == floorTile)
            {
                flag.transform.SetPositionAndRotation(flagPos, Quaternion.identity);
                flag.SetActive(true);
            } 
            else
            {
                for (int j = 0; j < floorLevel; j++)
                {
                    _view.Floor.SetTile(new Vector3Int(flagPos.x - 1, j, 0), floorTile);
                }
                flag.transform.SetPositionAndRotation(flagPos, Quaternion.identity);
                flag.SetActive(true);
            }

        }

        private void GenerateCoin(Vector3Int position)
        {
            var randomValue = UnityEngine.Random.value;
            if (randomValue <= 0.33)
            {
                while (_view.Enviroment.GetTile(position) == _view.TileSet[1])
                {
                    position += Vector3Int.up;
                }
                GameObject.Instantiate(_view.CoinPrefab, position, Quaternion.identity, _view.CoinParentObject.transform);

            }
        }

        private void DebugDrawBoundaries()
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
