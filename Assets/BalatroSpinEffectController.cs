using UnityEngine;

public class BalatroSpinEffectController : MonoBehaviour
{
    public Material mat;
    public bool isRotating = true;

    void Update()
    {
        if (mat != null)
        {
            mat.SetFloat("_IsRotating", isRotating ? 1f : 0f);
            mat.SetVector("_Offset", new Vector4(Mathf.Sin(Time.time) * 0.1f, Mathf.Cos(Time.time) * 0.1f, 0, 0));
        }
    }
}
