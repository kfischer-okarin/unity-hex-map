/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using UnityEngine;
using System;
using System.Collections.Generic;

namespace HexMapEngine {

    [System.Serializable]
    public struct Hex : IEquatable<Hex> {

        public int q, r;

        public int s { get { return -(q + r); } }

        public Hex(int q, int r) {
            this.q = q;
            this.r = r;
        }

        public int Length { get { return (Mathf.Abs(q) + Mathf.Abs(r) + Mathf.Abs(s)) / 2; } }

        public int DistanceTo(Hex other) {
            return (this - other).Length;
        }

        #region Related Hexes
        /// <summary>
        /// Adds this Hex to all of the specified Hexes and returns the resulting Hexes.
        /// Useful for centering a calculated area of hexes around an arbitrary Hex.
        /// </summary>
        public Hex[] CalcOffsetHexes(IEnumerable<Hex> offsets) {
            var result = new List<Hex>();
            foreach (Hex h in offsets)
                result.Add(this + h);
            return result.ToArray();
        }

        static Hex[] NEIGHBOR_OFFSETS = { 
            new Hex(1, 0), new Hex(0, 1), new Hex(-1, 1),
            new Hex(-1, 0), new Hex(0, -1), new Hex(1, -1)
        };

        public Hex[] GetNeighbors() {
            return CalcOffsetHexes(NEIGHBOR_OFFSETS);
        }

        /// <summary>
        /// Returns a set of all Hexes that satisfy the specified coordinate restraints.
        /// </summary>
        public static Hex[] CoordinateRestrainedGroup(int qmin, int qmax, int rmin, int rmax, int smin, int smax) {
            var result = new List<Hex>();
            for (int i = qmin; i <= qmax; i++) {
                for (int j = rmin; j <= rmax; j++) {
                    if (-(i + j) >= smin && -(i + j) <= smax)
                        result.Add(new Hex(i, j));
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Returns a set of all Hexes that satisfy the specified symmetrical coordinate restraints.
        /// </summary>
        public static Hex[] CoordinateRestrainedGroup(int qmax, int rmax, int smax) {
            return CoordinateRestrainedGroup(-qmax, qmax, -rmax, rmax, -smax, smax);
        }

        public static Hex CalcCenter(IEnumerable<Hex> hexes) {
            var result = new Hex(0, 0);
            int count = 0;
            foreach (Hex h in hexes) {
                result += h;
                count++;
            }
            return result / count;
        }
        #endregion

        #region Operators
        // Implements IEquatable
        public bool Equals(Hex other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (obj is Hex)
                return Equals((Hex) obj);
            return false;
        }

        public override int GetHashCode() {
            return (51 + q.GetHashCode()) * 51 + r.GetHashCode();
        }

        public override string ToString() {
            return string.Format("Hex({0}, {1})", q, r);
        }

        public static bool operator==(Hex a, Hex b) {
            return a.q == b.q && a.r == b.r;
        }

        public static bool operator!=(Hex a, Hex b) {
            return !(a == b);
        }

        public static Hex operator+(Hex a, Hex b) {
            return new Hex(a.q + b.q, a.r + b.r);
        }

        public static Hex operator-(Hex a, Hex b) {
            return new Hex(a.q - b.q, a.r - b.r);
        }

        public static Hex operator-(Hex a) {
            return new Hex(-a.q, -a.r);
        }

        public static Hex operator*(Hex a, int k) {
            return new Hex(a.q * k, a.r * k);
        }

        public static Hex operator/(Hex a, int k) {
            return new Hex(a.q / k, a.r / k);
        }

        public static Hex Round(float q, float r) {
            int rq = Mathf.RoundToInt(q);
            int rr = Mathf.RoundToInt(r);
            int rs = Mathf.RoundToInt(-(q + r));

            float qDiff = Mathf.Abs(q - rq);
            float rDiff = Mathf.Abs(r - rr);
            float sDiff = Mathf.Abs(-(q + r) - rs);

            if (qDiff > rDiff && qDiff > sDiff)
                rq = -(rr + rs);
            else if (rDiff > sDiff)
                rr = -(rq + rs);

            return new Hex(rq, rr);
        }
        #endregion

    }

}
