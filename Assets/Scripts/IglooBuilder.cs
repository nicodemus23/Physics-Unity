//using UnityEngine;
//using UnityEditor;

//[ExecuteInEditMode]
//public class IglooBuilder : MonoBehaviour
//{
//    public GameObject blockPrefab;
//    public int numLayers = 10;
//    public float layerHeight = 1f;
//    public float initialRadius = 5f;
//    public float radiusDecrease = 0.4f;
//    public float layerRotationOffset = 15f;

//    private void OnValidate()
//    {
//        MarkChildrenAsDirty();
//        EditorApplication.delayCall += BuildIgloo;
//    }

//    private void BuildIgloo()
//    {
//        ClearIgloo();

//        float currentRadius = initialRadius;
//        float currentHeight = 0f;
//        float currentRotationOffset = 0f;

//        for (int i = 0; i < numLayers; i++)
//        {
//            int numBlocks = Mathf.RoundToInt(2f * Mathf.PI * currentRadius);
//            float angleIncrement = 360f / numBlocks;

//            for (int j = 0; j < numBlocks; j++)
//            {
//                float angle = j * angleIncrement + currentRotationOffset;
//                Vector3 position = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * currentRadius;
//                position.y = currentHeight;

//                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);
//                block.transform.parent = transform;

//                Vector3 directionToCenter = (transform.position - block.transform.position).normalized;
//                block.transform.rotation = Quaternion.LookRotation(-directionToCenter);
//            }

//            currentRadius -= radiusDecrease;
//            currentHeight += layerHeight;
//            currentRotationOffset += layerRotationOffset;
//        }
//    }

//    private void MarkChildrenAsDirty()
//    {
//        foreach (Transform child in transform)
//        {
//            EditorUtility.SetDirty(child.gameObject);
//        }
//    }

//    private void ClearIgloo()
//    {
//        int childCount = transform.childCount;
//        for (int i = childCount - 1; i >= 0; i--)
//        {
//            DestroyImmediate(transform.GetChild(i).gameObject);
//        }
//    }
//}