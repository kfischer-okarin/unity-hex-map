/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using System;

namespace HexMapEngine {

    /// <summary>
    /// Data that are stored at a Hex position. 
    /// In this base class only the index in the Tileset is stored. Actual games would of course extend this class and
    /// add game-specific data (like Terrain type etc) to the subclass.
    /// </summary>
    [System.Serializable]
    public class HexData {

        public Hex position;

        public int tileIndex;

        public int q { get { return position.q; } }

        public int r { get { return position.r; } }

        public int s { get { return position.s; } }

        public HexData(Hex position, int tileIndex) {
            this.position = position;
            this.tileIndex = tileIndex;
        }

    }

}
