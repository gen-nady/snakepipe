using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private PathTile _endCapTile;
        [SerializeField] private PathTile _head;
        [SerializeField] private PathTile _curveTile;
        [SerializeField] private PathTile _straightTile;
        [SerializeField] private PathTile _halfEndCapTile;
        [SerializeField] private PathTile _halfStraightTile;
        [SerializeField] private TileJoin _tileJoin;
        public float Size { get; set; }

        private PathTile _cachedLastTile;
        private PathTile _currentPathTile;

        public IEnumerable<GridTile> GridTiles
        {
            get => _gridTiles;
            set
            {
                _gridTiles.Clear();
                _gridTiles.AddRange(value);
                UpdatePath();
            }
        }

        public bool Closed
        {
            get => _closed;
            set
            {
                _closed = value;
                UpdatePath();
            }
        }

        private readonly List<GridTile> _gridTiles = new List<GridTile>();
        private readonly List<PathTile> _tiles = new List<PathTile>();
        private readonly List<TileJoin> _joins = new List<TileJoin>();
        private bool _closed;

        private void UpdatePath()
        {
            var gridTiles = GridTiles.ToList();
            if (gridTiles.Count<=1)
            {
                return;
            }

            _tiles.ForEach(tile => Destroy(tile.gameObject));
            _tiles.Clear();

            _joins.ForEach(join => Destroy(join.gameObject));
            _joins.Clear();

            Vector2Int lastDirection = Vector2Int.right;
            Vector2Int? lastCurveCoordinate = null;

            var color = ColorGroup.Default.ElementAt(gridTiles[0].Color);

            for (var i = 0; i < gridTiles.Count-1; i++)
            {

                var direction = gridTiles[i + 1].Coordinate - gridTiles[i].Coordinate;
                if (i != 0 && Vector2.Angle(lastDirection,direction)>30)
                {
                    DestroyImmediate(_tiles.Last().gameObject);
                    _tiles.RemoveAt(_tiles.Count-1);

                    if (lastCurveCoordinate == null ||
                        ((Vector2Int) lastCurveCoordinate - gridTiles[i].Coordinate).magnitude > 1.1f)
                    {
                        var startHalfTile = Instantiate(i == 1 ? _halfEndCapTile : _halfStraightTile, transform);
                        var typeTile=i == 1 ? PathTileType.HalfEndCapTile : PathTileType.HalfStraightTile;
                        startHalfTile.PathTileType = typeTile;
                        startHalfTile.transform.position = gridTiles[i - 1].transform.position;
                        startHalfTile.Direction = lastDirection;

                        startHalfTile.Color = color;
                        startHalfTile.Size = Size;
                        _tiles.Add(startHalfTile);
                    }

                    var curveTile = Instantiate(_curveTile,transform);
                    curveTile.PathTileType = PathTileType.Curve;
                    curveTile.transform.position = gridTiles[i].transform.position;
                    curveTile.Direction = Vector3.Cross(-(Vector2) lastDirection, (Vector2) direction).z > 0
                        ? -(Vector2) lastDirection
                        : direction;
                    curveTile.Color = color;
                    curveTile.Size = Size;

                    _tiles.Add(curveTile);
                    lastCurveCoordinate = gridTiles[i].Coordinate;

                    var endHalfTile = Instantiate( !Closed || i < gridTiles.Count-2? _halfStraightTile : _head, transform);
                    var type = !Closed || i < gridTiles.Count - 2 ? PathTileType.HalfStraightTile : PathTileType.Head;
                    endHalfTile.PathTileType = type;
                    var dir = !Closed || i < gridTiles.Count - 2 ? -(Vector2)direction : direction; //End tile logic



                    if ( endHalfTile.PathTileType == PathTileType.Head)
                    {
                        endHalfTile.GetComponent<Head>().EnableShortHead();
                        Debug.Log(_cachedLastTile.PathTileType);
                    }
    


                    endHalfTile.transform.position = gridTiles[i +1].transform.position;
                    endHalfTile.Direction = dir;  
                    endHalfTile.Color = color;
                    endHalfTile.Size = Size;
                    _tiles.Add(endHalfTile);
                    _currentPathTile = endHalfTile;
                }
                else
                {
                    PathTile pathTile = null;

                    if (!Closed || i < gridTiles.Count - 2)
                    {
                        pathTile = Instantiate(i == 0 ? _endCapTile : _straightTile, transform);
                        var type = i == 0  ? PathTileType.EndCapTile : PathTileType.StraightTile;
                        pathTile.PathTileType = type;
                        pathTile.transform.position = gridTiles[i].transform.position;
                        pathTile.Direction = direction;
                    }
                    else
                    {
                        if (gridTiles.Count <= 2)
                        {
                            var firstHalf = Instantiate(_halfEndCapTile, transform);
                            firstHalf.PathTileType = PathTileType.HalfEndCapTile;
                            firstHalf.transform.position = gridTiles[0].transform.position;
                            firstHalf.Direction = direction;
                            firstHalf.Size = Size;
                            firstHalf.Color = color;
                            _tiles.Add(firstHalf);
                        }

                        pathTile = Instantiate( gridTiles.Count <=2? _halfEndCapTile: _head, transform);
                        var type = gridTiles.Count <= 2 ? PathTileType.HalfEndCapTile : PathTileType.Head;
                        pathTile.PathTileType = type;


                        if (pathTile.PathTileType == PathTileType.Head)
                        {
                            pathTile.GetComponent<Head>().EnableTallHead();
                            Debug.Log(_cachedLastTile.PathTileType);
                        }

                        pathTile.transform.position = gridTiles[i+1].transform.position;
                        pathTile.Direction = direction;  // End tile logic
                    }


                    pathTile.Color = color;
                    pathTile.Size = Size;
                    _tiles.Add(pathTile);
                    _currentPathTile = pathTile;
                }

                if (_tiles.Contains(_cachedLastTile))  //Snake head logic
                {
                    _cachedLastTile.DisableHead();
                }

                if(_currentPathTile.GetComponent<Head>()==null)
                {
                    _currentPathTile.EnableHead();
                }
             
                _cachedLastTile = _currentPathTile;

                lastDirection = _gridTiles[i + 1].Coordinate - _gridTiles[i].Coordinate;
            }

            for (var i = 0; i < _tiles.Count; i++)
            {
                var pathTile = _tiles[i];
                var point = pathTile.OpenPoints.FirstOrDefault(trans => i==0 || _joins.All(join => (join.transform.position-trans.position).sqrMagnitude>0.005f));

                if(point==null)
                    continue;
                var tileJoin = Instantiate(_tileJoin,transform);
                tileJoin.Color =color;
                tileJoin.transform.position = point.position;
                tileJoin.Direction = point.right;
                tileJoin.Size = Size ;
//                    i == 0 || i == _tiles.Count - 1
//                    ? (Vector3)pathTile.Direction
//                    : (_tiles[i + 1].transform.position - _tiles[i].transform.position);
                _joins.Add(tileJoin);
            }

        }

    }
}