using UnityEngine.Tilemaps;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public Tilemap map;
    public Tilemap mapl2;
    public Tilemap copymap;
    public TileBase oceanTile;
    public TileBase IslandTile;
    public TileBase IslandOceanTransistionTileTop;
    public TileBase IslandOceanTransistionTileBottom;
    public TileBase IslandOceanTransistionTileLeft;
    public TileBase IslandOceanTransistionTileRight;
    public TileBase IslandOceanTransistionTileCornerNO;
    public TileBase IslandOceanTransistionTileCornerOZ;
    public TileBase IslandOceanTransistionTileCornerZW;
    public TileBase IslandOceanTransistionTileCornerNW;
    public TileBase IslandOceanTransistionTileCornerInNO;
    public TileBase IslandOceanTransistionTileCornerInOZ;
    public TileBase IslandOceanTransistionTileCornerInZW;
    public TileBase IslandOceanTransistionTileCornerInNW;

    [SerializeField] int mapWidth = 100;
    [SerializeField] int mapHeight = 100;

    [SerializeField] float scale = 10f;
    [SerializeField] float waterline = 0.6f;

    [SerializeField, Tooltip("Set noiseXOffset and noiseYOffset to -1 for random noise map positions")]
    float noiseXOffset = 0;
    [SerializeField]
    float noiseYOffset = 0;

    void Start()
    {
        if (noiseXOffset == -1 && noiseYOffset == -1) {
            noiseXOffset = Random.Range(0, 10000);
            noiseYOffset = Random.Range(0, 10000);
        }
        GenerateMap();
    }

    void GenerateMap() {

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                float PosX = (float)x / mapWidth * scale;
                float PosY = (float)y / mapHeight * scale;

                float noiseValue = Mathf.PerlinNoise(PosX + noiseXOffset, PosY + noiseYOffset);

                if (noiseValue > waterline )
                {
                    copymap.SetTile(new Vector3Int(x, y, 0), IslandTile);
                    mapl2.SetTile(new Vector3Int(x, y, 0), IslandTile);
                }
                else {
                    map.SetTile(new Vector3Int(x, y, 0), oceanTile);
                    copymap.SetTile(new Vector3Int(x, y, 0), oceanTile);
                }
            }
        }
        ShapeMap();
    }

    void ShapeMap() {
        
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
               
                //Top
                if (GetTileBase(x, y + 1) == oceanTile && GetTileBase(x, y) != IslandTile)
                {
                    Top(x, y);
                }
                //Bottom
                if (GetTileBase(x,y - 1) == oceanTile && GetTileBase(x,y) != IslandTile)
                {
                    Bottom(x,y);
                }
            }
        }
    }

    void Top(int x, int y) {

        if (GetTileBase(x - 1, y) == oceanTile && GetTileBase(x + 1, y) == oceanTile && GetTileBase(x, y - 1) == oceanTile && GetTileBase(x + 1, y - 1) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerInNW);
        }
        else if (GetTileBase(x - 1, y) == oceanTile && GetTileBase(x + 1, y) == oceanTile && GetTileBase(x, y - 1) == oceanTile && GetTileBase(x - 1, y - 1) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerInNO);
        }

        else if (GetTileBase(x, y - 1) == IslandTile && GetTileBase(x - 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerNO);
        }
        else if (GetTileBase(x, y - 1) == IslandTile && GetTileBase(x + 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerNW);
        }

        else if (GetTileBase(x, y - 1) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileTop);
        }
        else if (GetTileBase(x - 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileLeft);
        }
        else if (GetTileBase(x + 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileRight);
        }

    }





    void Bottom(int x, int y)
    {
        if (GetTileBase(x - 1, y) == oceanTile && GetTileBase(x + 1, y) == oceanTile && GetTileBase(x, y + 1) == oceanTile && GetTileBase(x + 1, y + 1) == IslandTile) {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerInZW);
        }
        else if (GetTileBase(x - 1, y) == oceanTile && GetTileBase(x + 1, y) == oceanTile && GetTileBase(x, y + 1) == oceanTile && GetTileBase(x -1, y + 1) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerInOZ);
        }
        else if (GetTileBase(x, y + 1) == IslandTile && GetTileBase(x - 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerZW);
        }
        else if (GetTileBase(x, y + 1) == IslandTile && GetTileBase(x + 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileCornerZW);
        }
        else if (GetTileBase(x, y + 1) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileBottom);
        }
        else if (GetTileBase(x -1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileLeft);
        }
        else if (GetTileBase(x + 1, y) == IslandTile)
        {
            mapl2.SetTile(new Vector3Int(x, y, 0), IslandOceanTransistionTileRight);
        }




    }




    public TileBase GetTileBase(int x, int y) {

        return copymap.GetTile(new Vector3Int(x, y, 0));
    }
}
