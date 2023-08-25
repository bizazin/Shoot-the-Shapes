using UnityEngine;

public class SpawnPyramid : MonoBehaviour
{
    public GameObject cubePrefab; // Префаб куба
    public GameObject spherePrefab; // Префаб шара
    public Transform centerPoint; // Точка трансформ-центра
    public int baseSize = 10; // Размер основания пирамиды

    void Start()
    {
        SpawnObjectsInPyramid();
    }

    void SpawnObjectsInPyramid()
    {
        for (int y = 0; y < baseSize; y++)
        {
            int currentBaseSize = baseSize - y;
            int halfBaseSize = currentBaseSize / 2;

            for (int x = -halfBaseSize; x < halfBaseSize; x++)
            {
                for (int z = -halfBaseSize; z < halfBaseSize; z++)
                {
                    Vector3 position = new Vector3(x + centerPoint.position.x, y + centerPoint.position.y, z + centerPoint.position.z);

                    // Случайным образом выбираем между кубом и шаром
                    GameObject prefabToSpawn = Random.value > 0.5f ? cubePrefab : spherePrefab;
                    Instantiate(prefabToSpawn, position, Quaternion.identity);
                }
            }
        }
    }
}