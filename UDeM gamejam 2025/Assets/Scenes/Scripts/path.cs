using System.Collections;
using UnityEngine;

public class MazePathManager : MonoBehaviour
{
    public GameObject pathPrefab;  // Reference to the path prefab
    public GameObject backPrefab;  // Reference to the back prefab
    // private Mazegen mazegen;  // Reference to the Mazegen script

    // void Start() {
    //     mazegen = GetComponent<Mazegen>();  // Get the Mazegen script attached to the same GameObject
    // }

    public void MakePath(Vector2 position) {
        // Ensure parent is assigned correctly
        DeleteExistingObjectAtPosition(position);

        // Instantiate the path object in the scene
        Instantiate(pathPrefab, position, Quaternion.identity);
    }

    public void MakeBack(Vector2 position) {
        // Ensure parent is assigned correctly
        DeleteExistingObjectAtPosition(position);

        // Instantiate the back object in the scene
        Instantiate(backPrefab, position, Quaternion.identity);
    }

    private void DeleteExistingObjectAtPosition(Vector2 position) {
        // Find and destroy any existing objects at the specified position
        foreach (Transform child in transform) {
            if (child.position == new Vector3(position.x, position.y, child.position.z) && 
                (child.gameObject.name == pathPrefab.name || child.gameObject.name == backPrefab.name)) {
                Destroy(child.gameObject);
            }
        }
    }
}
