using UnityEngine;

public class HUDControl : MonoBehaviour {
    RectTransform rectTransform;
 
    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }
 
    void Update() {
        // 自身の向きをカメラに向ける
        rectTransform.LookAt(Camera.main.transform);
    }
}
