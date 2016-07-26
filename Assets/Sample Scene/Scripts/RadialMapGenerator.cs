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
        result.hideFlags = HideFlags.HideAndDontSave;
        var hexCells = new List<HexCell>();
        for (int q = -radius; q <= radius; q++) {
            for (int r = Mathf.Max(-radius, -q - radius); r <= Mathf.Min(radius, -q + radius); r++) {
                hexCells.Add(new HexCell(q, r, RNG.Next(4)));
            }
        }
        result.SetHexCells(hexCells);

        return result;
    }

}
