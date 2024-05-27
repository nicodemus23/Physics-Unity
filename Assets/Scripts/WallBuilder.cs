using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    public GameObject snowBlockPrefab;
    public GameObject firstBlockPrefab;
    public GameObject secondBlockPrefab;

    public int numLayers = 10;
    public int numBlocksPerLayer = 20;

    public float blockWidth = 1f;
    public float blockHeight = 1f;
    public float blockDepth = 1f;

    public float blockOffsetX = 0.5f;
    public float blockOffsetY = 0.5f;
    public float blockOffsetZ = 0f;

    [Range(0f, 1f)]
    public float firstBlockRatio = 0.1f;
    [Range(0f, 1f)]
    public float secondBlockRatio = 0.1f;

    public float lightIntensityMin = 0.5f;
    public float lightIntensityMax = 1f;
    public float lightOscillationFrequency = 1f;

    public float horizontalSpread = 0.5f;
    public float verticalSpread = 0.5f;

    private void Start()
    {
        BuildWall();
    }

    private void BuildWall()
    {
        ClearWall();

        for (int i = 0; i < numLayers; i++)
        {
            float rowOffset = (i % 2 == 0) ? 0f : blockWidth * 0.5f;

            for (int j = 0; j < numBlocksPerLayer; j++)
            {
                GameObject prefab;
                float randomValue = Random.value;

                if (randomValue <= firstBlockRatio)
                {
                    prefab = firstBlockPrefab;
                }
                else if (randomValue <= firstBlockRatio + secondBlockRatio)
                {
                    prefab = secondBlockPrefab;
                }
                else
                {
                    prefab = snowBlockPrefab;
                }

                Vector3 position = transform.position + new Vector3(
                    j * (blockWidth + blockOffsetX + horizontalSpread) + rowOffset,
                    i * (blockHeight + blockOffsetY + verticalSpread),
                    Random.Range(-blockOffsetZ, blockOffsetZ)
                );

                GameObject block = Instantiate(prefab, position, Quaternion.identity, transform);
                block.transform.localScale = new Vector3(blockWidth, blockHeight, blockDepth);

                if (prefab == firstBlockPrefab || prefab == secondBlockPrefab)
                {
                    Light pointLight = block.GetComponentInChildren<Light>();
                    if (pointLight != null)
                    {
                        float randomOffset = Random.Range(0f, Mathf.PI * 2f);
                        LightOscillator oscillator = block.AddComponent<LightOscillator>();
                        oscillator.SetupOscillator(pointLight, lightIntensityMin, lightIntensityMax, lightOscillationFrequency, randomOffset);
                    }
                }
            }
        }
    }

    private void ClearWall()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

public class LightOscillator : MonoBehaviour
{
    private Light pointLight;
    private float intensityMin;
    private float intensityMax;
    private float frequency;
    private float offset;

    public void SetupOscillator(Light light, float min, float max, float freq, float offset)
    {
        pointLight = light;
        intensityMin = min;
        intensityMax = max;
        frequency = freq;
        this.offset = offset;
    }

    private void Update()
    {
        if (pointLight != null)
        {
            float t = Time.time * frequency + offset;
            float intensity = Mathf.Lerp(intensityMin, intensityMax, Mathf.Sin(t) * 0.5f + 0.5f);
            pointLight.intensity = intensity;
        }
    }
}



// FOR EDITOR USE:

//using UnityEngine;
//using UnityEditor;

////[ExecuteInEditMode]
//public class WallBuilder : MonoBehaviour
//{
//    public GameObject snowBlockPrefab;
//    public GameObject firstBlockPrefab;
//    public GameObject secondBlockPrefab;

//    public int numLayers = 10;
//    public int numBlocksPerLayer = 20;

//    public float blockWidth = 1f;
//    public float blockHeight = 1f;
//    public float blockDepth = 1f;

//    public float blockOffsetX = 0.5f;
//    public float blockOffsetY = 0.5f;
//    public float blockOffsetZ = 0f;

//    [Range(0f, 1f)]
//    public float firstBlockRatio = 0.1f;
//    [Range(0f, 1f)]
//    public float secondBlockRatio = 0.1f;

//    public float lightIntensityMin = 0.5f;
//    public float lightIntensityMax = 1f;
//    public float lightOscillationFrequency = 1f;

//    public bool regenerateWall = true;

//    public float horizontalSpread = 0.5f;
//    public float verticalSpread = 0.5f;

//    private void OnValidate()
//    {
//        if (regenerateWall)
//        {
//            MarkChildrenAsDirty();
//            EditorApplication.delayCall += BuildWall;
//        }
//    }

//    private void BuildWall()
//    {
//        if (regenerateWall)
//        {
//            ClearWall();

//            for (int i = 0; i < numLayers; i++)
//            {
//                float rowOffset = (i % 2 == 0) ? 0f : blockWidth * 0.5f;

//                for (int j = 0; j < numBlocksPerLayer; j++)
//                {
//                    GameObject prefab;
//                    float randomValue = Random.value;

//                    if (randomValue <= firstBlockRatio)
//                    {
//                        prefab = firstBlockPrefab;
//                    }
//                    else if (randomValue <= firstBlockRatio + secondBlockRatio)
//                    {
//                        prefab = secondBlockPrefab;
//                    }
//                    else
//                    {
//                        prefab = snowBlockPrefab;
//                    }

//                    Vector3 position = transform.position + new Vector3(
//                        j * (blockWidth + blockOffsetX + horizontalSpread) + rowOffset,
//                        i * (blockHeight + blockOffsetY + verticalSpread),
//                        Random.Range(-blockOffsetZ, blockOffsetZ)
//                    );

//                    GameObject block = Instantiate(prefab, position, Quaternion.identity, transform);
//                    block.transform.localScale = new Vector3(blockWidth, blockHeight, blockDepth);

//                    if (prefab == firstBlockPrefab || prefab == secondBlockPrefab)
//                    {
//                        Light pointLight = block.GetComponentInChildren<Light>();
//                        if (pointLight != null)
//                        {
//                            float randomOffset = Random.Range(0f, Mathf.PI * 2f);
//                            LightOscillator oscillator = block.AddComponent<LightOscillator>();
//                            oscillator.SetupOscillator(pointLight, lightIntensityMin, lightIntensityMax, lightOscillationFrequency, randomOffset);
//                        }
//                    }
//                }
//            }
//        }
//    }

//    private void MarkChildrenAsDirty()
//    {
//        foreach (Transform child in transform)
//        {
//            EditorUtility.SetDirty(child.gameObject);
//        }
//    }

//    private void ClearWall()
//    {
//        int childCount = transform.childCount;
//        for (int i = childCount - 1; i >= 0; i--)
//        {
//            DestroyImmediate(transform.GetChild(i).gameObject);
//        }
//    }
//}

//public class LightOscillator : MonoBehaviour
//{
//    private Light pointLight;
//    private float intensityMin;
//    private float intensityMax;
//    private float frequency;
//    private float offset;

//    public void SetupOscillator(Light light, float min, float max, float freq, float offset)
//    {
//        pointLight = light;
//        intensityMin = min;
//        intensityMax = max;
//        frequency = freq;
//        this.offset = offset;
//    }

//    private void Update()
//    {
//        if (pointLight != null)
//        {
//            float t = Time.time * frequency + offset;
//            float intensity = Mathf.Lerp(intensityMin, intensityMax, Mathf.Sin(t) * 0.5f + 0.5f);
//            pointLight.intensity = intensity;
//        }
//    }
//}