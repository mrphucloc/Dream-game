using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu nhấn phím B
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Lấy index của scene hiện tại
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Tính toán scene tiếp theo (nếu là scene cuối cùng thì quay về scene 0)
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

            // Load scene tiếp theo
            SceneManager.LoadScene(nextSceneIndex);
        }

        // Hoặc nếu bạn muốn chuyển qua lại giữa chỉ 2 scene cụ thể:
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
}