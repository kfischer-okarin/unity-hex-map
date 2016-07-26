/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

namespace HexMapEngine {

    [System.Serializable]
    public struct HexCell {

        public int q, r;

        public int tileIndex;

        public HexCell(int q, int r, int tileIndex) {
            this.q = q;
            this.r = r;
            this.tileIndex = tileIndex;
        }

    }

}
