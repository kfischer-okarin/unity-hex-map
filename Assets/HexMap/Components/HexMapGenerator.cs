/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;

namespace HexMapEngine {

    [ExecuteInEditMode]
    [RequireComponent(typeof(HexMapRenderer))]
    public class HexMapGenerator : MonoBehaviour {

        HexMapRenderer _renderer;

        public static HexMap StandardMap() {
            HexMap result = ScriptableObject.CreateInstance<HexMap>();
            result.hideFlags = HideFlags.HideAndDontSave;
            var hexCells = new List<HexCell>();
            hexCells.Add(new HexCell(0, 0));
            result.SetHexCells(hexCells);

            return result;
        }

        void Start() {
            _renderer = GetComponent<HexMapRenderer>();

            _renderer.HexMap = StandardMap();
        }

    }

}
