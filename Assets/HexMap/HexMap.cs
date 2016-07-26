/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;

namespace HexMapEngine {

    public class HexMap : ScriptableObject {

        List<HexCell> _hexCells;

        public HexCell[] HexCells {
            get { return _hexCells.ToArray(); }
        }

        Dictionary<string, HexCell> _map;

        public void SetHexCells(List<HexCell> hexCells) {
            _hexCells = hexCells;
            _map = new Dictionary<string, HexCell>();
            foreach (HexCell cell in hexCells) {
                _map.Add(cell.q + "," + cell.r, cell);
            }
        }

        public HexCell Get(int q, int r) {
            return _map[q + "," + r];
        }

    }

}
