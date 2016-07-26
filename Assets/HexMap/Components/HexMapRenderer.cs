/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System.Collections.Generic;

namespace HexMapEngine {

    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class HexMapRenderer : MonoBehaviour {

        [SerializeField] HexMap _hexMap;

        public HexMap HexMap {
            get { return _hexMap; }
            set {
                _hexMap = value;
                GenerateMesh();
            }
        }

        public float gridSize = 1;

        void Start() {
            if (_hexMap != null)
                GenerateMesh();
        }

        float _lastGridSize;

        void OnValidate() {
            if (gridSize != _lastGridSize && _hexMap != null) {
                _lastGridSize = gridSize;
                GenerateMesh();
            }
        }

        #region Mesh Generation
        static float SQRT_3 = Mathf.Sqrt(3);
        const float TO_RAD_FACTOR = Mathf.PI / 180;

        // Pointy top, q right, r bottom-right
        Vector2 GetHexCenter(HexCell cell) {
            return new Vector2(gridSize * SQRT_3 * (cell.q + cell.r / 2f), gridSize * (-1.5f) * cell.r);
        }

        // 0 bottom right, 1 bottom, .. 5 top right
        Vector2 GetHexCorner(Vector2 center, int corner) {
            var angle_deg = 60 * corner + 30;
            var angle_rad = TO_RAD_FACTOR * angle_deg;
            return new Vector2(center.x + gridSize * Mathf.Cos(angle_rad), center.y - gridSize * Mathf.Sin(angle_rad));
        }

        public void GenerateMesh() {
            var newMesh = new Mesh();
            newMesh.hideFlags = HideFlags.HideAndDontSave;

            _gridPoints.Clear();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            var cornerOffsets = new Vector2[6];
            for (int i = 0; i < 6; i++) {
                cornerOffsets[i] = GetHexCorner(new Vector2(0, 0), i);
            }

            foreach (HexCell c in _hexMap.HexCells) {
                Vector2 center = GetHexCenter(c);
                vertices.Add(center);
                uvs.Add(Vector2.zero);

                int centerIndex = vertices.Count - 1;
                var corners = new int[6];

                for (int i = 0; i < 6; i++) {
                    Vector2 corner = center + cornerOffsets[i];
                    if (vertices.Contains(corner)) {
                        corners[i] = vertices.IndexOf(corner);
                    } else {
                        vertices.Add(corner);
                        uvs.Add(Vector2.zero);
                        corners[i] = vertices.Count - 1;
                    }

                    _gridPoints.Add(corner);
                }

                for (int i = 0; i < 6; i++) {
                    triangles.AddRange(new int[] { centerIndex, corners[i], corners[i == 5 ? 0 : i + 1] });
                }
            }

            newMesh.vertices = vertices.ToArray();
            newMesh.uv = uvs.ToArray();
            newMesh.triangles = triangles.ToArray();
            GetComponent<MeshFilter>().sharedMesh = newMesh;
        }
        #endregion

        #region Drawing Hex Grid outlines
        public Material lineMaterial;

        List<Vector2> _gridPoints = new List<Vector2>();

        void OnRenderObject() {
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);
            {
                for (int i = 0; i < _gridPoints.Count / 6; i++) {
                    for (int j = 0; j < 6; j++) {
                        GL.Vertex(_gridPoints[i * 6 + j]);
                        GL.Vertex(_gridPoints[i * 6 + (j == 5 ? 0 : j + 1)]);
                    }
                }
            }
            GL.End();

            GL.PopMatrix();
        }
        #endregion

    }

}
