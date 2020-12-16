/*
﻿The MIT License (MIT)

Copyright © 2013-2017 Cyotek Ltd.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using System.IO;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Serializes and deserializes 8bit color palettes from raw byte data.
    /// </summary>
    public class RawPaletteSerializer : PaletteSerializer
    {
        #region Properties

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        public override string DefaultExtension => "pal";

        /// <summary>
        /// Gets the maximum number of colors supported by this format.
        /// </summary>
        /// <value>
        /// The maximum value number of colors supported by this format.
        /// </value>
        public override int Maximum => 256;

        /// <summary>
        /// Gets the minimum numbers of colors supported by this format.
        /// </summary>
        /// <value>
        /// The minimum number of colors supported by this format.
        /// </value>
        public override int Minimum => 256;

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        public override string Name => "Raw Palette";

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can read palette from data the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if this instance can read palette data from the specified stream; otherwise, <c>false</c>.</returns>
        public override bool CanReadFrom(Stream stream)
        {
            bool result;

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                result = stream.Length % 3 == 0;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        
        public override ColorCollection DeserializeNew(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var results = new ColorCollection();


            for (var i = 0; i < stream.Length / 3; i++)
            {
                int r;
                int g;
                int b;

                r = stream.ReadByte();
                g = stream.ReadByte();
                b = stream.ReadByte();

                results.Add(Color.FromArgb((byte) r, (byte) g, (byte) b));
            }

            return results;
        }
        
        public override void Serialize(Stream stream, ColorCollection palette)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (palette == null)
                throw new ArgumentNullException(nameof(palette));

            foreach (var color in palette)
            {
                stream.WriteByte(color.R);
                stream.WriteByte(color.G);
                stream.WriteByte(color.B);
            }

            stream.Flush();
        }

        #endregion
    }
}