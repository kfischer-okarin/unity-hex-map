/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using System;

namespace HexMapEngine {

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
