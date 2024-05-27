using UnityEngine;

public class DomeBuilder : MonoBehaviour
{
    public GameObject blockPrefab;
    public int numLayers = 10;
    public float layerHeightMultiplier = 1f;
    public float layerHeightMultiplierIncrease = 0.1f;
    public float initialRadius = 5f;
    public float radiusDecrease = 0.4f;
    public float layerRotationOffset = 15f;
    public float domeAngle = 30f;
    public float blockYScale = 1f;
    public float blockYScaleDecrease = 0.1f;
    public float blockSpacing = 0.1f;
    public float constraintBreakForce = 1000f;
    public float constraintBreakTorque = 1000f;
    public float constraintSpring = 1000f;
    public float constraintDamper = 50f;

    public bool regenerateIgloo = true;

    private void Start()
    {
        if (regenerateIgloo)
        {
            BuildIgloo();
        }
    }

    private void BuildIgloo()
    {
        ClearIgloo();

        float currentRadius = initialRadius;
        float currentHeight = 0f;
        float currentRotationOffset = 0f;
        float currentBlockYScale = blockYScale;
        float currentLayerHeightMultiplier = layerHeightMultiplier;

        GameObject previousBlock = null;

        for (int i = 0; i < numLayers; i++)
        {
            float t = (float)i / (numLayers - 1);
            float domeAngleRadians = Mathf.Deg2Rad * domeAngle * t;
            float domeRadius = currentRadius * Mathf.Cos(domeAngleRadians);
            float domeHeight = currentHeight + currentRadius * Mathf.Sin(domeAngleRadians);

            int numBlocks = Mathf.RoundToInt(2f * Mathf.PI * domeRadius);
            float angleIncrement = 360f / numBlocks;

            GameObject firstBlockInLayer = null;

            for (int j = 0; j < numBlocks; j++)
            {
                float angle = j * angleIncrement + currentRotationOffset;
                Vector3 localPosition = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * (domeRadius + blockSpacing);
                localPosition.y = domeHeight;

                Vector3 worldPosition = transform.TransformPoint(localPosition);

                GameObject block = Instantiate(blockPrefab, worldPosition, Quaternion.identity);
                block.transform.parent = transform;

                Vector3 directionToCenter = (transform.position - block.transform.position).normalized;
                block.transform.rotation = Quaternion.LookRotation(-directionToCenter);
                block.transform.rotation *= Quaternion.Euler(domeAngleRadians * Mathf.Rad2Deg, 0f, 0f);

                block.transform.localScale = new Vector3(1f, currentBlockYScale, 1f);

                if (previousBlock != null)
                {
                    ConfigurableJoint joint = block.AddComponent<ConfigurableJoint>();
                    joint.connectedBody = previousBlock.GetComponent<Rigidbody>();
                    joint.breakForce = constraintBreakForce;
                    joint.breakTorque = constraintBreakTorque;
                    joint.xMotion = ConfigurableJointMotion.Limited;
                    joint.yMotion = ConfigurableJointMotion.Limited;
                    joint.zMotion = ConfigurableJointMotion.Limited;
                    joint.angularXMotion = ConfigurableJointMotion.Limited;
                    joint.angularYMotion = ConfigurableJointMotion.Limited;
                    joint.angularZMotion = ConfigurableJointMotion.Limited;
                    joint.linearLimit = new SoftJointLimit { limit = 0.01f };
                    joint.lowAngularXLimit = new SoftJointLimit { limit = 5f };
                    joint.highAngularXLimit = new SoftJointLimit { limit = 5f };
                    joint.angularYLimit = new SoftJointLimit { limit = 5f };
                    joint.angularZLimit = new SoftJointLimit { limit = 5f };
                    joint.linearLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
                    joint.angularXLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
                    joint.angularYZLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
                }

                if (firstBlockInLayer == null)
                {
                    firstBlockInLayer = block;
                }

                previousBlock = block;
            }

            if (firstBlockInLayer != null && previousBlock != null)
            {
                ConfigurableJoint joint = firstBlockInLayer.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousBlock.GetComponent<Rigidbody>();
                joint.breakForce = constraintBreakForce;
                joint.breakTorque = constraintBreakTorque;
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;
                joint.linearLimit = new SoftJointLimit { limit = 0.01f };
                joint.lowAngularXLimit = new SoftJointLimit { limit = 5f };
                joint.highAngularXLimit = new SoftJointLimit { limit = 5f };
                joint.angularYLimit = new SoftJointLimit { limit = 5f };
                joint.angularZLimit = new SoftJointLimit { limit = 5f };
                joint.linearLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
                joint.angularXLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
                joint.angularYZLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
            }

            currentRadius -= radiusDecrease;
            currentHeight += currentLayerHeightMultiplier * currentBlockYScale;
            currentLayerHeightMultiplier += layerHeightMultiplierIncrease;
            currentRotationOffset += layerRotationOffset;
            currentBlockYScale -= blockYScaleDecrease;
        }
    }

    private void ClearIgloo()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
// FOR USE IN EDITOR: 

//using UnityEngine;
//using UnityEditor;

////[ExecuteInEditMode]
//public class DomeBuilder : MonoBehaviour
//{
//    public GameObject blockPrefab;
//    public int numLayers = 10;
//    public float layerHeightMultiplier = 1f;
//    public float layerHeightMultiplierIncrease = 0.1f;
//    public float initialRadius = 5f;
//    public float radiusDecrease = 0.4f;
//    public float layerRotationOffset = 15f;
//    public float domeAngle = 30f;
//    public float blockYScale = 1f;
//    public float blockYScaleDecrease = 0.1f;
//    public float blockSpacing = 0.1f;
//    public float constraintBreakForce = 1000f;
//    public float constraintBreakTorque = 1000f;
//    public float constraintSpring = 1000f;
//    public float constraintDamper = 50f;

//    public bool regenerateIgloo = true;

//    private void OnValidate()
//    {
//        if (regenerateIgloo && transform.childCount == 0) // Check if igloo doesn't exist
//        {
//            MarkChildrenAsDirty();
//            EditorApplication.delayCall += BuildIgloo;
//        }
//    }

//    private void BuildIgloo()
//    {
//        ClearIgloo();

//        float currentRadius = initialRadius;
//        float currentHeight = 0f;
//        float currentRotationOffset = 0f;
//        float currentBlockYScale = blockYScale;
//        float currentLayerHeightMultiplier = layerHeightMultiplier;

//        GameObject previousBlock = null;

//        for (int i = 0; i < numLayers; i++)
//        {
//            float t = (float)i / (numLayers - 1);
//            float domeAngleRadians = Mathf.Deg2Rad * domeAngle * t;
//            float domeRadius = currentRadius * Mathf.Cos(domeAngleRadians);
//            float domeHeight = currentHeight + currentRadius * Mathf.Sin(domeAngleRadians);

//            int numBlocks = Mathf.RoundToInt(2f * Mathf.PI * domeRadius);
//            float angleIncrement = 360f / numBlocks;

//            GameObject firstBlockInLayer = null;

//            for (int j = 0; j < numBlocks; j++)
//            {
//                float angle = j * angleIncrement + currentRotationOffset;
//                Vector3 localPosition = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * (domeRadius + blockSpacing);
//                localPosition.y = domeHeight;

//                Vector3 worldPosition = transform.TransformPoint(localPosition);

//                GameObject block = Instantiate(blockPrefab, worldPosition, Quaternion.identity);
//                block.transform.parent = transform;

//                Vector3 directionToCenter = (transform.position - block.transform.position).normalized;
//                block.transform.rotation = Quaternion.LookRotation(-directionToCenter);
//                block.transform.rotation *= Quaternion.Euler(domeAngleRadians * Mathf.Rad2Deg, 0f, 0f);

//                block.transform.localScale = new Vector3(1f, currentBlockYScale, 1f);

//                if (previousBlock != null)
//                {
//                    ConfigurableJoint joint = block.AddComponent<ConfigurableJoint>();
//                    joint.connectedBody = previousBlock.GetComponent<Rigidbody>();
//                    joint.breakForce = constraintBreakForce;
//                    joint.breakTorque = constraintBreakTorque;
//                    joint.xMotion = ConfigurableJointMotion.Limited;
//                    joint.yMotion = ConfigurableJointMotion.Limited;
//                    joint.zMotion = ConfigurableJointMotion.Limited;
//                    joint.angularXMotion = ConfigurableJointMotion.Limited;
//                    joint.angularYMotion = ConfigurableJointMotion.Limited;
//                    joint.angularZMotion = ConfigurableJointMotion.Limited;
//                    joint.linearLimit = new SoftJointLimit { limit = 0.01f };
//                    joint.lowAngularXLimit = new SoftJointLimit { limit = 5f };
//                    joint.highAngularXLimit = new SoftJointLimit { limit = 5f };
//                    joint.angularYLimit = new SoftJointLimit { limit = 5f };
//                    joint.angularZLimit = new SoftJointLimit { limit = 5f };
//                    joint.linearLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
//                    joint.angularXLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
//                    joint.angularYZLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
//                }

//                if (firstBlockInLayer == null)
//                {
//                    firstBlockInLayer = block;
//                }

//                previousBlock = block;
//            }

//            if (firstBlockInLayer != null && previousBlock != null)
//            {
//                ConfigurableJoint joint = firstBlockInLayer.AddComponent<ConfigurableJoint>();
//                joint.connectedBody = previousBlock.GetComponent<Rigidbody>();
//                joint.breakForce = constraintBreakForce;
//                joint.breakTorque = constraintBreakTorque;
//                joint.xMotion = ConfigurableJointMotion.Limited;
//                joint.yMotion = ConfigurableJointMotion.Limited;
//                joint.zMotion = ConfigurableJointMotion.Limited;
//                joint.angularXMotion = ConfigurableJointMotion.Limited;
//                joint.angularYMotion = ConfigurableJointMotion.Limited;
//                joint.angularZMotion = ConfigurableJointMotion.Limited;
//                joint.linearLimit = new SoftJointLimit { limit = 0.01f };
//                joint.lowAngularXLimit = new SoftJointLimit { limit = 5f };
//                joint.highAngularXLimit = new SoftJointLimit { limit = 5f };
//                joint.angularYLimit = new SoftJointLimit { limit = 5f };
//                joint.angularZLimit = new SoftJointLimit { limit = 5f };
//                joint.linearLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
//                joint.angularXLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
//                joint.angularYZLimitSpring = new SoftJointLimitSpring { spring = constraintSpring, damper = constraintDamper };
//            }

//            currentRadius -= radiusDecrease;
//            currentHeight += currentLayerHeightMultiplier * currentBlockYScale;
//            currentLayerHeightMultiplier += layerHeightMultiplierIncrease;
//            currentRotationOffset += layerRotationOffset;
//            currentBlockYScale -= blockYScaleDecrease;
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