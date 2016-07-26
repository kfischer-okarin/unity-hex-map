/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;

namespace HexMapEngine {

    public class HexMap : ScriptableObject {

        List<HexData> _hexData;

        public HexData[] HexData {
            get { return _hexData.ToArray(); }
        }

        Dictionary<Hex, HexData> _map;

        public void SetHexData(List<HexData> hexData) {
            _hexData = hexData;
            _map = new Dictionary<Hex, HexData>();
            foreach (HexData data in hexData) {
                _map.Add(data.position, data);
            }
        }

        public HexData Get(Hex position) {
            HexData result;
            _map.TryGetValue(position, out result);
            return result;
        }

    }

}
