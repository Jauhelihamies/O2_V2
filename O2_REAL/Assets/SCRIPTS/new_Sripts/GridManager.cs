using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int columns = 5;
    public int rows = 5;
    public Vector2 spacing = new Vector2(1.1f, 1.1f);
    public GameObject gridItemPrefab;

    

    private GameObject[,] gridMatrix;

    void Start()
    {
        gridMatrix = new GameObject[columns, rows];
        GenerateGrid();
    }


    void GenerateGrid()
    {

        Vector3 offset = new Vector3(
            (columns - 1) * spacing.x / 2f,
            (rows - 1) * spacing.y / 2f,
            0f
        );

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {

                Vector3 spawnPosition = new Vector3(x * spacing.x, y * spacing.y, 0f) - offset;
                Vector3 finalPosition = transform.TransformPoint(spawnPosition);

                // Luodaan aloitusruutu
                GameObject newItem = Instantiate(gridItemPrefab, finalPosition, Quaternion.identity);
                newItem.transform.SetParent(transform);
                newItem.name = $"Grid_Cell_{x}_{y}";


                gridMatrix[x, y] = newItem;
            }
        }
    }
}