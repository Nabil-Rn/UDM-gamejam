using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Mazegen : MonoBehaviour {
    const string WALL = "wall";
    const string CELL = "cell";
    const string UNVISITED = "unvisited";
    public int maze_width = 10;
    public int maze_height = 10;
    public int cellSize = 1;
    public string[,] maze;
    public List<int[]> walls;
    public GameObject wallPrefab;  // Reference to the wall prefab
    public GameObject pathPrefab;  // Reference to the path prefab
    public GameObject playerPrefab;  // Reference to the path prefab
    public int[] startingPoint;

    void Start() {
        maze = new string[maze_height, maze_width];
        walls = new List<int[]>();

        // Denote all cells as unvisited.
        for (int i = 0; i < maze_height; i++) {
            for (int j = 0; j < maze_width; j++) {
                maze[i, j] = UNVISITED;
            }
        }

        // Randomize starting point and set it a cell.
        int starting_height = (int)(UnityEngine.Random.value * maze_height);
        int starting_width = (int)(UnityEngine.Random.value * maze_width);

        if (starting_height == 0) {
            starting_height += 1;
        }
        if (starting_height == maze_height - 1) {
            starting_height -= 1;
        }
        if (starting_width == 0) {
            starting_width += 1;
        }
        if (starting_width == maze_width - 1) {
            starting_width -= 1;
        }

        startingPoint = new int[] {starting_height, starting_width};

        // Mark it as cell and add surrounding walls to the list.
        maze[starting_height, starting_width] = CELL;
        walls.Add(new int[] {starting_height - 1, starting_width});
        walls.Add(new int[] {starting_height, starting_width - 1});
        walls.Add(new int[] {starting_height, starting_width + 1});
        walls.Add(new int[] {starting_height + 1, starting_width});

        // Denote walls in maze.
        maze[starting_height - 1, starting_width] = WALL;
        maze[starting_height, starting_width - 1] = WALL;
        maze[starting_height, starting_width + 1] = WALL;
        maze[starting_height + 1, starting_width] = WALL;

        while (walls.Count > 0) {
            // Pick a random wall.
            int[] rand_wall = walls[UnityEngine.Random.Range(0, walls.Count)];

            // Check if it is a left wall.
            if (rand_wall[1] != 0) {
                if (maze[rand_wall[0], rand_wall[1] - 1] == UNVISITED && maze[rand_wall[0], rand_wall[1] + 1] == CELL) {
                    // Find the number of surrounding cells.
                    int s_cells = Surrounding_cells(rand_wall);

                    if (s_cells < 2) {
                        // Denote the new path.
                        maze[rand_wall[0], rand_wall[1]] = CELL;

                        // Mark the new walls.
                        // Upper cell.
                        if (rand_wall[0] != 0) {
                            if (maze[rand_wall[0] - 1, rand_wall[1]] != CELL) {
                                maze[rand_wall[0] - 1, rand_wall[1]] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == (rand_wall[0] + 1) && wall[1] == rand_wall[1])) {
                                walls.Add(new int[] {rand_wall[0] - 1, rand_wall[1]});
                            }
                        }

                        // Bottom cell.
                        if (rand_wall[0] != maze_height - 1) {
                            if (maze[rand_wall[0] + 1, rand_wall[1]] != CELL) {
                                maze[rand_wall[0] + 1, rand_wall[1]] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == (rand_wall[0] + 1) && wall[1] == rand_wall[1])) {
                                walls.Add(new int[] {rand_wall[0] + 1, rand_wall[1]});
                            }
                        }

                        // Leftmost cell.
                        if (rand_wall[1] != 0) {
                            if (maze[rand_wall[0], rand_wall[1] - 1] != CELL) {
                                maze[rand_wall[0], rand_wall[1] - 1] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == rand_wall[0] && wall[1] == (rand_wall[1] - 1))) {
                                walls.Add(new int[] {rand_wall[0], rand_wall[1] - 1});
                            }
                        }
                    }

                    // Delete wall.
                    List<int[]> wallsCopy = new(walls);
                    foreach (var wall in wallsCopy) {
                        if (wall[0] == rand_wall[0] && wall[1] == rand_wall[1]) {
                            walls.Remove(wall);
                        }
                    }

                    continue;
                }
            }

            // Check if it is an upper wall.
            if (rand_wall[0] != 0) {
                if (maze[rand_wall[0] - 1, rand_wall[1]] == UNVISITED && maze[rand_wall[0] + 1, rand_wall[1]] == CELL) {
                    int s_cells = Surrounding_cells(rand_wall);
                    if (s_cells < 2) {
                        // Denote the new path.
                        maze[rand_wall[0], rand_wall[1]] = CELL;

                        // Mark the new walls.
                        // Upper cell.
                        if (rand_wall[0] != 0) {
                            if (maze[rand_wall[0] - 1, rand_wall[1]] != CELL) {
                                maze[rand_wall[0] - 1, rand_wall[1]] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == (rand_wall[0] - 1) && wall[1] == rand_wall[1])) {
                                walls.Add(new int[] {rand_wall[0] - 1, rand_wall[1]});
                            }
                        }

                        // Leftmost cell.
                        if (rand_wall[1] != 0) {
                            if (maze[rand_wall[0], rand_wall[1] - 1] != CELL) {
                                maze[rand_wall[0], rand_wall[1] - 1] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == rand_wall[0] && wall[1] == (rand_wall[1] - 1))) {
                                walls.Add(new int[] {rand_wall[0], rand_wall[1] - 1});
                            }
                        }

                        // Rightmost cell.
                        if (rand_wall[1] != maze_width - 1) {
                            if (maze[rand_wall[0], rand_wall[1] + 1] != CELL) {
                                maze[rand_wall[0], rand_wall[1] + 1] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == rand_wall[0] && wall[1] == (rand_wall[1] + 1))) {
                                walls.Add(new int[] {rand_wall[0], rand_wall[1] + 1});
                            }
                        }
                    }

                    // Delete wall.
                    List<int[]> wallsCopy = new(walls);
                    foreach (var wall in wallsCopy) {
                        if (wall[0] == rand_wall[0] && wall[1] == rand_wall[1]) {
                            walls.Remove(wall);
                        }
                    }

                    continue;
                }
            }

            // Check the bottom wall.
            if (rand_wall[0] != maze_height - 1) {
                if (maze[rand_wall[0] + 1, rand_wall[1]] == UNVISITED && maze[rand_wall[0] - 1, rand_wall[1]] == CELL) {
                    int s_cells = Surrounding_cells(rand_wall);
                    if (s_cells < 2) {
                        // Denote the new path.
                        maze[rand_wall[0], rand_wall[1]] = CELL;

                        // Mark the new walls.
                        if (rand_wall[0] != maze_height - 1) {
                            if (maze[rand_wall[0] + 1, rand_wall[1]] != CELL) {
                                maze[rand_wall[0] + 1, rand_wall[1]] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == (rand_wall[0] + 1) && wall[1] == rand_wall[1])) {
                                walls.Add(new int[] {rand_wall[0] + 1, rand_wall[1]});
                            }
                        }
                        if (rand_wall[1] != 0) {
                            if (maze[rand_wall[0], rand_wall[1] - 1] != CELL) {
                                maze[rand_wall[0], rand_wall[1] - 1] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == rand_wall[0] && wall[1] == (rand_wall[1] - 1))) {
                                walls.Add(new int[] {rand_wall[0], rand_wall[1] - 1});
                            }
                        }
                        if (rand_wall[1] != maze_width - 1) {
                            if (maze[rand_wall[0], rand_wall[1] + 1] != CELL) {
                                maze[rand_wall[0], rand_wall[1] + 1] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == rand_wall[0] && wall[1] == (rand_wall[1] + 1))) {
                                walls.Add(new int[] {rand_wall[0], rand_wall[1] + 1});
                            }
                        }
                    }

                    // Delete wall.
                    List<int[]> wallsCopy = new(walls);
                    foreach (var wall in wallsCopy) {
                        if (wall[0] == rand_wall[0] && wall[1] == rand_wall[1]) {
                            walls.Remove(wall);
                        }
                    }

                    continue;
                }
            }

            // Check the right wall.
            if (rand_wall[1] != maze_width - 1) {
                if (maze[rand_wall[0], rand_wall[1] + 1] == UNVISITED && maze[rand_wall[0], rand_wall[1] - 1] == CELL) {
                    int s_cells = Surrounding_cells(rand_wall);
                    if (s_cells < 2) {
                        // Denote the new path.
                        maze[rand_wall[0], rand_wall[1]] = CELL;

                        // Mark the new walls.
                        if (rand_wall[1] != maze_width - 1) {
                            if (maze[rand_wall[0], rand_wall[1] + 1] != CELL) {
                                maze[rand_wall[0], rand_wall[1] + 1] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == rand_wall[0] && wall[1] == (rand_wall[1] + 1))) {
                                walls.Add(new int[] {rand_wall[0], rand_wall[1] + 1});
                            }
                        }
                        if (rand_wall[0] != maze_height - 1) {
                            if (maze[rand_wall[0] + 1, rand_wall[1]] != CELL) {
                                maze[rand_wall[0] + 1, rand_wall[1]] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == (rand_wall[0] + 1) && wall[1] == rand_wall[1])) {
                                walls.Add(new int[] {rand_wall[0] + 1, rand_wall[1]});
                            }
                        }
                        if (rand_wall[0] != 0) {
                            if (maze[rand_wall[0] - 1, rand_wall[1]] != CELL) {
                                maze[rand_wall[0] - 1, rand_wall[1]] = WALL;
                            }
                            if (!walls.Any(wall => wall[0] == (rand_wall[0] - 1) && wall[1] == rand_wall[1])) {
                                walls.Add(new int[] {rand_wall[0] - 1, rand_wall[1]});
                            }
                        }
                    }

                    // Delete wall.
                    List<int[]> wallsCopy = new(walls);
                    foreach (var wall in wallsCopy) {
                        if (wall[0] == rand_wall[0] && wall[1] == rand_wall[1]) {
                            walls.Remove(wall);
                        }
                    }

                    continue;
                }
            }

            // Delete the wall from the list anyway.
            List<int[]> wallsCopyAgain = new(walls);
            foreach (var wall in wallsCopyAgain) {
                if (wall[0] == rand_wall[0] && wall[1] == rand_wall[1]) {
                    walls.Remove(wall);
                }
            }
        }

        // Mark the remaining unvisited cells as walls.
        for (int i = 0; i < maze_height; i++) {
            for (int j = 0; j < maze_width; j++) {
                if (maze[i, j] == UNVISITED) {
                    maze[i, j] = WALL;
                }
            }
        }

        // Set entrance and exit.
        for (int i = 0; i < maze_width; i++) {
            if (maze[1, i] == CELL) {
                maze[0, i] = CELL;
                break;
            }
        }

        for (int i = maze_width - 1; i >= 0; i--) {
            if (maze[maze_height - 2, i] == CELL) {
                maze[maze_height - 1, i] = CELL;
                break;
            }
        }

        VisualizeMaze();
    }

    int Surrounding_cells(int[] rand_wall) {
        int s_cells = 0;
        if (maze[rand_wall[0] - 1, rand_wall[1]] == CELL) {
            s_cells += 1;
        }
        if (maze[rand_wall[0] + 1, rand_wall[1]] == CELL) {
            s_cells += 1;
        }
        if (maze[rand_wall[0], rand_wall[1] - 1] == CELL) {
            s_cells += 1;
        }
        if (maze[rand_wall[0], rand_wall[1] + 1] == CELL) {
            s_cells += 1;
        }

        return s_cells;
    }

    void VisualizeMaze() {
        // Clear any previous maze objects
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        // Visualize the maze with blue and white squares
        for (int i = 0; i < maze_height; i++) {
            for (int j = 0; j < maze_width; j++) {
                GameObject prefabToInstantiate = maze[i, j] == WALL ? wallPrefab : pathPrefab;
                Vector3 position = new(j * cellSize, -i * cellSize, 0);
                Instantiate(prefabToInstantiate, position, Quaternion.identity, transform);
            }
        }

        // Instantiate the player at the entrance
        for (int i = 0; i < maze_width; i++) {
            if (maze[0, i] == CELL) {
                Vector3 playerPosition = new(i * cellSize, -0 * cellSize, 0);
                Instantiate(playerPrefab, playerPosition, Quaternion.identity, transform);
                break;
            }
        }
    }
}
