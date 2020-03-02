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
using System.Globalization;
using System.IO;
using System.Text;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Serializes and deserializes color palettes into and from the Paint.NET palette format.
    /// </summary>
    public class PaintNetPaletteSerializer : PaletteSerializer
    {
        #region Properties

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        public override string DefaultExtension
        {
            get { return "txt"; }
        }

        /// <summary>
        /// Gets the maximum number of colors supported by this format.
        /// </summary>
        /// <value>
        /// The maximum value number of colors supported by this format.
        /// </value>
        public override int Maximum
        {
            get { return 96; }
        }

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        public override string Name
        {
            get { return "Paint.NET Palette"; }
        }

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
                using (StreamReader reader = new StreamReader(stream))
                {
                    string firstLine;

                    firstLine = reader.ReadLine();

                    result = !string.IsNullOrEmpty(firstLine) && firstLine[0] == ';';
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Deserializes the <see cref="ColorCollection" /> contained by the specified <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="Stream" /> that contains the palette to deserialize.</param>
        /// <returns>The <see cref="ColorCollection" /> being deserialized.</returns>
        public override ColorCollection Deserialize(Stream stream)
        {
            ColorCollection results;

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            results = new ColorCollection();

            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line;

                    line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith(";") && line.Length == 8)
                    {
                        int a;
                        int r;
                        int g;
                        int b;

                        a = int.Parse(line.Substring(0, 2), NumberStyles.HexNumber);
                        r = int.Parse(line.Substring(2, 2), NumberStyles.HexNumber);
                        g = int.Parse(line.Substring(4, 2), NumberStyles.HexNumber);
                        b = int.Parse(line.Substring(6, 2), NumberStyles.HexNumber);

                        results.Add(Color.FromArgb(a, r, g, b));
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Serializes the specified <see cref="ColorCollection" /> and writes the palette to a file using the specified <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="Stream" /> used to write the palette.</param>
        /// <param name="palette">The <see cref="ColorCollection" /> to serialize.</param>
        public override void Serialize(Stream stream, ColorCollection palette)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (palette == null)
            {
                throw new ArgumentNullException(nameof(palette));
            }

            // TODO: Not writing 96 colors, but the entire contents of the palette, wether that's less than 96 or more

            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine(@"; Paint.NET Palette File
; Lines that start with a semicolon are comments
; Colors are written as 8-digit hexadecimal numbers: aarrggbb
; For example, this would specify green: FF00FF00
; The alpha ('aa') value specifies how transparent a color is. FF is fully opaque, 00 is fully transparent.
; A palette must consist of ninety six (96) colors. If there are less than this, the remaining color
; slots will be set to white (FFFFFFFF). If there are more, then the remaining colors will be ignored.");
                foreach (Color color in palette)
                {
                    writer.WriteLine("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
                }
            }
        }

        #endregion
    }
}