/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;

namespace HexMapEngine {

    [ExecuteInEditMode]
    [RequireComponent(typeof(HexMapRenderer))]
    public abstract class HexMapGenerator : MonoBehaviour {

        public int seed;

        System.Random _rng;
        protected System.Random RNG { get { return _rng; } }

        protected abstract HexMap GenerateMap();

        void Start() {
            OnValidate();
        }

        void OnValidate() {
            _rng = new System.Random(seed);
            GetComponent<HexMapRenderer>().HexMap = GenerateMap();
        }

    }

}
