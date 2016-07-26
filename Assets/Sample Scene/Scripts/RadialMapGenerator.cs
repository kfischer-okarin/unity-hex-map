/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;
using HexMapEngine;

public class RadialMapGenerator : HexMapGenerator {

    public int radius = 0;

    protected override HexMap GenerateMap() {
        HexMap result = ScriptableObject.CreateInstance<HexMap>();

        var hexData = new List<HexData>();
        foreach (Hex h in Hex.CoordinateRestrainedGroup(radius, radius, radius)) {
            hexData.Add(new HexData(h, RNG.Next(4)));
        }
        result.SetHexData(hexData);

        return result;
    }

}
