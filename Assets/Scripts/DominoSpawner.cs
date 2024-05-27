using UnityEngine;

//[ExecuteInEditMode]
public class DominoSpawner : MonoBehaviour
{
    public GameObject dominoPrefab;
    public int numDominos = 100;
    public float startRadius = 1f;
    public float endRadius = 5f;
    public float startSpacing = 0.1f;
    public float endSpacing = 0.5f;
    public float tiltAngle = 15f;
    public float spiralFactor = 0.1f;
    public float yRotation = 0f;
    public int numSpirals = 3;
    public bool enableSpawning = true;
    public float firstDominoTiltAngle = 0f;
    public float lastDominoTiltAngle = 0f;
    [SerializeField] private int totalDominoesCreated = 0;

    private bool hasSpawnedDominoes = false;

    private void OnValidate()
    {
        if (enableSpawning && !hasSpawnedDominoes)
        {
            SpawnDominoes();
            hasSpawnedDominoes = true;
        }
    }

    private void ClearDominos()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void SpawnDominoes()
    {
        ClearDominos();

        float angleStep = 360f / numDominos;
        float radiusStep = (endRadius - startRadius) / (numSpirals * numDominos);
        float spacingStep = (endSpacing - startSpacing) / (numSpirals * numDominos);

        totalDominoesCreated = 0;

        for (int i = 0; i < numSpirals * numDominos; i++)
        {
            float angle = i * angleStep;
            float radius = startRadius + i * radiusStep;
            float spacing = startSpacing + i * spacingStep;

            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

            Vector3 position = transform.position + new Vector3(x, 0f, z);

            float tiltAngleToUse = tiltAngle;
            if (i == 0)
            {
                tiltAngleToUse = firstDominoTiltAngle;
            }
            else if (i == numSpirals * numDominos - 1)
            {
                tiltAngleToUse = lastDominoTiltAngle;
            }

            Quaternion rotation = Quaternion.Euler(0f, yRotation + angle, tiltAngleToUse);
            GameObject domino = Instantiate(dominoPrefab, position, rotation, transform);

            if (i == 0)
            {
                tiltAngleToUse = firstDominoTiltAngle;
                domino.transform.rotation = Quaternion.Euler(0f, yRotation + angle, tiltAngleToUse);
            }

            // Adjust the position based on the spacing
            Vector3 direction = (position - transform.position).normalized;
            domino.transform.position += direction * spacing;

            // Set the tag for the spawned domino
            domino.tag = "Domino";

            totalDominoesCreated++;
        }

        Debug.Log("Total dominos created: " + totalDominoesCreated);
    }
}