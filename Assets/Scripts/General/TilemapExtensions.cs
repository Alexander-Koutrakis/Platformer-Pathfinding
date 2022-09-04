using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public static class TilemapExtensions 
{
    public static Vector2[] GroundedTilePos(this Tilemap tileMap)
    {
        List<Vector2> emptyTiles = tileMap.EmptyTilePos();
        List<Vector2> groundedTiles = new List<Vector2>();
        List<Vector2> results = new List<Vector2>();
        tileMap.CompressBounds();
        Bounds bounds = tileMap.localBounds;
        RaycastHit2D lookdown;
        LayerMask whatIsGround = 1 << LayerMask.NameToLayer("Ground");
        for (int i = 0; i < emptyTiles.Count; i++)
        {
            lookdown = Physics2D.Raycast(emptyTiles[i], Vector2.down, (tileMap.cellSize.y / 2) + 1f, whatIsGround);
            if (lookdown)
            {

                groundedTiles.Add(emptyTiles[i]);
            }
        }

        for (int i = 0; i < groundedTiles.Count; i++)
        {
            if (bounds.Contains(groundedTiles[i]))
            {

                results.Add(groundedTiles[i]);
            }
        }
        return results.ToArray();
    }

    public static List<Vector2> EmptyTilePos(this Tilemap tileMap)
    {
        List<Vector2> emptyTiles = new List<Vector2>();
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int tile_position = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                if (!tileMap.HasTile(tile_position))
                {
                    Vector3 position = tileMap.CellToWorld(tile_position);
                    position = new Vector3(position.x + tileMap.cellSize.x / 2, position.y + tileMap.cellSize.y / 2, 0);
                    emptyTiles.Add(position);
                }
            }
        }

        return emptyTiles;
    }
}
