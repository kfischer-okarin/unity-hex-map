/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;

namespace HexMapEngine {

    /// <summary>
    /// Base class for HexMap generators. Extend to create a new generator.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(HexMapRenderer))]
    public abstract class HexMapGenerator : MonoBehaviour {

        /// <summary>Should the map be generated with a random seed when the script is loaded?</summary>
        public bool generateOnLoad = true;

        public int seed;

        System.Random _rng;

        /// <summary>
        /// The Random Number Generator that should be used for map generation
        /// to guarantee the same result for a given random seed.
        /// </summary>
        protected System.Random RNG { get { return _rng; } }

        /// <summary>
        /// Generates the map. 
        /// Only use the protected RNG variable for random number generation to guarantee always the same result for a
        /// given random seed.
        /// </summary>
        protected abstract HexMap GenerateMap();

        void Start() {
            if (generateOnLoad)
                Generate(true);
        }

        void OnValidate() {
            Generate();
        }

        /// <summary>
        /// (Re-)Generate the HexMap using either the current random seed or a new one.
        /// </summary>
        /// <param name="newSeed">If set to <c>true</c> use new seed (current system time).</param>
        public void Generate(bool newSeed = false) {
            if (newSeed)
                seed = (int) System.DateTime.Now.ToFileTimeUtc();

            _rng = new System.Random(seed);

            HexMap newMap = GenerateMap();
            newMap.hideFlags = HideFlags.HideAndDontSave;
            GetComponent<HexMapRenderer>().HexMap = newMap;
        }

    }

}
