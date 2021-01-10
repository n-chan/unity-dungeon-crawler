using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour {
    int width;
    int height;

    public Tilemap groundMap, collisionMap;
    public Tile dirt, grass, stone;
    public Transform ladder;

    // Start is called before the first frame update
    void Start() {
        width = 10;
        height = 10;
        Generate();
        
    }

    void Generate() {
        float scale = Random.Range(0.2f, 1.5f);

        for (int x = -width; x < width; x++) {
            for (int y = -height; y < height; y++) {
                if (x == -width || x == width - 1 || y == -height || y == height - 1) {
                    groundMap.SetTile(new Vector3Int(x, y, 0), stone);
                }
                else {
                    float n = Mathf.PerlinNoise((float)x * scale, (float)y * scale);
                    if (n > 0.3f) {
                        groundMap.SetTile(new Vector3Int(x, y, 0), dirt);
                    }
                    else {
                        collisionMap.SetTile(new Vector3Int(x, y, 0), stone);
                    }
                }
            }
        }
        //Loop until a suitable spot to put the ladder is found.
        while (true) {
            int randomX = Random.Range(-width + 2, width - 2);
            int randomY = Random.Range(-height + 2, height - 2);
            //Debug.Log(randomX + " " + randomY);
            if (!collisionMap.GetTile(new Vector3Int(randomX, randomY, 0))) {
                Instantiate(ladder, new Vector3(randomX, randomY, 0), Quaternion.identity);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
