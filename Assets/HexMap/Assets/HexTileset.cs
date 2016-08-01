/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;

namespace HexMapEngine {

    /// <summary>
    /// Asset wrapper around Hex Tileset textures to allow access to single tiles.
    /// </summary>
    [CreateAssetMenu(fileName = "NewHexTileset.asset", menuName = "Hex Grid Tileset", order = 305)]
    public class HexTileset : ScriptableObject {

        public Texture texture;

        public int tilesHorizontal;

        public int tilesVertical;

        #region UV Calculation
        static float SQRT_3 = Mathf.Sqrt(3);

        static Vector2[] HEX_UVS = { new Vector2(0.5f, 0.5f), 
            new Vector2(0.5f + SQRT_3 / 4f, 0.25f), new Vector2(0.5f, 0), 
            new Vector2(0.5f - SQRT_3 / 4f, 0.25f), new Vector2(0.5f - SQRT_3 / 4f, 0.75f), 
            new Vector2(0.5f, 1), new Vector2(0.5f + SQRT_3 / 4f, 0.75f)
        };

        public Vector2[] GetUVs(int tileIndex) {
            int tileX = tileIndex % tilesHorizontal;
            int tileY = tileIndex / tilesHorizontal;

            float uFactor = 1.0f / tilesHorizontal;
            float vFactor = 1.0f / tilesVertical;
            var uvOffset = new Vector2(tileX * uFactor, (tilesVertical - 1 - tileY) * vFactor);

            var result = new Vector2[7];
            for (int i = 0; i < 7; i++) {
                result[i] = new Vector2(uvOffset.x + HEX_UVS[i].x * uFactor, uvOffset.y + HEX_UVS[i].y * vFactor);
            }

            return result;
        }
        #endregion

    }

}
