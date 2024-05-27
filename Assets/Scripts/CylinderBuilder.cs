//using UnityEngine;
//using UnityEditor;

//[ExecuteInEditMode]
//public class CylinderBuilder : MonoBehaviour
//{
//    public GameObject blockPrefab;
//    public int numLayers = 10;
//    public float layerHeight = 1f;
//    public float radius = 5f;
//    public float layerRotationOffset = 15f;
//    public float blockYScale = 1f;
//    public float blockSpacing = 0.1f;

//    private void OnValidate()
//    {
//        MarkChildrenAsDirty();
//        EditorApplication.delayCall += BuildCylinder;
//    }

//    private void BuildCylinder()
//    {
//        ClearCylinder();

//        float currentHeight = 0f;
//        float currentRotationOffset = 0f;

//        for (int i = 0; i < numLayers; i++)
//        {
//            int numBlocks = Mathf.RoundToInt(2f * Mathf.PI * radius);
//            float angleIncrement = 360f / numBlocks;

//            for (int j = 0; j < numBlocks; j++)
//            {
//                float angle = j * angleIncrement + currentRotationOffset;
//                Vector3 localPosition = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * (radius + blockSpacing);
//                localPosition.y = currentHeight;

//                Vector3 worldPosition = transform.TransformPoint(localPosition);

//                GameObject block = Instantiate(blockPrefab, worldPosition, Quaternion.identity);
//                block.transform.parent = transform;

//                Quaternion blockRotation = Quaternion.Euler(0f, angle, 0f);
//                block.transform.rotation = blockRotation;

//                block.transform.localScale = new Vector3(1f, blockYScale, 1f);
//            }

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

//    private void ClearCylinder()
//    {
//        int childCount = transform.childCount;
//        for (int i = childCount - 1; i >= 0; i--)
//        {
//            DestroyImmediate(transform.GetChild(i).gameObject);
//        }
//    }
//}