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
using System.Text;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Deserializes color palettes into and from the images and palettes using the  ILBM (IFF Interleaved Bitmap) format.
    /// </summary>
    public class InterleavedBitmapPaletteSerializer : PaletteSerializer
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to write palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be written using this serializer; otherwise, <c>false</c>.</value>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        public override string DefaultExtension
        {
            get { return "bbm;lbm"; }
        }

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        public override string Name
        {
            get { return "Interleaved Bitmap Palette"; }
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
                byte[] formHeaderData;
                byte[] imageHeaderData;
                string formHeader;
                string imageHeader;

                formHeaderData = new byte[4];
                imageHeaderData = new byte[4];

                stream.Read(formHeaderData, 0, formHeaderData.Length);
                this.ReadInt32(stream);
                stream.Read(imageHeaderData, 0, imageHeaderData.Length);

                formHeader = Encoding.ASCII.GetString(formHeaderData);
                imageHeader = Encoding.ASCII.GetString(imageHeaderData);

                result = formHeader == "FORM" && (imageHeader == "PBM " || imageHeader == "ILBM");
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

            // read the FORM header that identifies the document as an IFF file
            var buffer = new byte[4];
            stream.Read(buffer, 0, buffer.Length);

            if (Encoding.ASCII.GetString(buffer) != "FORM")
                throw new InvalidDataException("Form header not found.");

            // the next value is the size of all the data in the FORM chunk
            // We don't actually need this value, but we have to read it
            // regardless to advance the stream
            this.ReadInt32(stream);

            // read either the PBM or ILBM header that identifies this document as an image file
            stream.Read(buffer, 0, buffer.Length);
            var header = Encoding.ASCII.GetString(buffer);
            if (header != "PBM " && header != "ILBM")
            {
                throw new InvalidDataException("Bitmap header not found.");
            }

            while (stream.Read(buffer, 0, buffer.Length) == buffer.Length)
            {
                var chunkLength = this.ReadInt32(stream);

                if (Encoding.ASCII.GetString(buffer) != "CMAP")
                {
                    // some other LBM chunk, skip it
                    if (stream.CanSeek)
                    {
                        stream.Seek(chunkLength, SeekOrigin.Current);
                    }
                    else
                    {
                        for (int i = 0; i < chunkLength; i++)
                        {
                            stream.ReadByte();
                        }
                    }
                }
                else
                {
                    // color map chunk!
                    for (int i = 0; i < chunkLength / 3; i++)
                    {
                        int r;
                        int g;
                        int b;

                        r = stream.ReadByte();
                        g = stream.ReadByte();
                        b = stream.ReadByte();

                        results.Add(System.Windows.Media.Color.FromRgb((byte) r, (byte) g, (byte) b));
                    }

                    // all done so stop reading the rest of the file
                    break;
                }

                // chunks always contain an even number of bytes even if the recorded length is odd
                // if the length is odd, then there's a padding byte in the file - just read and discard
                if (chunkLength % 2 != 0)
                {
                    stream.ReadByte();
                }
            }

            return results;
        }
        
        public override void Serialize(Stream stream, ColorCollection palette)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (palette == null)
                throw new ArgumentNullException(nameof(palette));

            throw new NotSupportedException();
        }

        #endregion
    }
}