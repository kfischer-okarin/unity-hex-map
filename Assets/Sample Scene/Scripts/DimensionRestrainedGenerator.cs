/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;
using HexMapEngine;

public class DimensionRestrainedGenerator : HexMapGenerator {

    public int qMax = 0;
    public int rMax = 0;
    public int sMax = 0;

    protected override HexMap GenerateMap() {
        HexMap result = ScriptableObject.CreateInstance<HexMap>();
        result.hideFlags = HideFlags.HideAndDontSave;

        var hexData = new List<HexData>();
        foreach (Hex h in Hex.CoordinateRestrainedGroup(qMax, rMax, sMax)) {
            hexData.Add(new HexData(h, RNG.Next(4)));
        }
        result.SetHexData(hexData);

        return result;
    }

}
