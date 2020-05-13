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
using System.IO;

#if USEEXTERNALCYOTEKLIBS
using Cyotek.Drawing;

#endif

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Deserializes color palettes into and from the images and palettes using the  ILBM (IFF Interleaved Bitmap) format.
    /// </summary>
    public class AdobePhotoshopColorSwatchSerializer : PaletteSerializer
    {
        #region Properties

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        public override string DefaultExtension
        {
            get { return "aco"; }
        }

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        public override string Name
        {
            get { return "Adobe Photoshop Color Swatch"; }
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
                int version;

                // read the version, which occupies two bytes
                // first byte is 0, the second 1 so I assume this is added to make 1
                //version = this.ReadShort(stream);
                version = stream.ReadByte() + stream.ReadByte();

                result = version == 1 || version == 2;
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
            {
                throw new ArgumentNullException(nameof(stream));
            }

            // read the version, which occupies two bytes
            var version = (AdobePhotoshopColorSwatchFileVersion) ReadInt16(stream);

            if (version != AdobePhotoshopColorSwatchFileVersion.Version1 &&
                version != AdobePhotoshopColorSwatchFileVersion.Version2)
            {
                throw new InvalidDataException("Invalid version information.");
            }

            // the specification states that a version2 palette follows a version1
            // the only difference between version1 and version2 is the inclusion
            // of a name property. Perhaps there's addtional color spaces as well
            // but we can't support them all anyway
            // I noticed some files no longer include a version 1 palette

            var results = ReadPaletteNew(stream, version);

            if (version == AdobePhotoshopColorSwatchFileVersion.Version1)
            {
                version = (AdobePhotoshopColorSwatchFileVersion) ReadInt16(stream);

                if (version == AdobePhotoshopColorSwatchFileVersion.Version2)
                    results = ReadPaletteNew(stream, version);
            }

            return results;
        }
        

        public override void Serialize(Stream stream, ColorCollection palette)
        {
            Serialize(stream, palette, AdobePhotoshopColorSwatchColorSpace.Rgb);
        }
        
        public void Serialize(Stream stream, ColorCollection palette, AdobePhotoshopColorSwatchColorSpace colorSpace)
        {
            Serialize(stream, palette, AdobePhotoshopColorSwatchFileVersion.Version2, colorSpace);
        }
        
        public void Serialize(Stream stream, ColorCollection palette, AdobePhotoshopColorSwatchFileVersion version,
            AdobePhotoshopColorSwatchColorSpace colorSpace)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (palette == null)
                throw new ArgumentNullException(nameof(palette));

            if (version == AdobePhotoshopColorSwatchFileVersion.Version2)
                WritePaletteNew(stream, palette, AdobePhotoshopColorSwatchFileVersion.Version1, colorSpace);

            WritePaletteNew(stream, palette, version, colorSpace);
        }
        
        protected virtual ColorCollection ReadPaletteNew(Stream stream, AdobePhotoshopColorSwatchFileVersion version)
        {
            var results = new ColorCollection();

            // read the number of colors, which also occupies two bytes
            var colorCount = ReadInt16(stream);

            for (int i = 0; i < colorCount; i++)
            {
                int value1;
                int value2;
                int value3;
                string name;

                // again, two bytes for the color space
                var colorSpace = (AdobePhotoshopColorSwatchColorSpace) ReadInt16(stream);

                value1 = ReadInt16(stream);
                value2 = ReadInt16(stream);
                value3 = ReadInt16(stream);
                ReadInt16(
                    stream); // only CMYK supports this field. As we can't handle CMYK colors, we read the value to advance the stream but don't do anything with it

                if (version == AdobePhotoshopColorSwatchFileVersion.Version2)
                {
                    int length;

                    // need to read the name even though currently our colour collection doesn't support names
                    length = ReadInt32(stream);
                    name = ReadString(stream, length);
                }
                else
                {
                    name = string.Empty;
                }

                switch (colorSpace)
                {
                    case AdobePhotoshopColorSwatchColorSpace.Rgb:
                        int red;
                        int green;
                        int blue;

                        // RGB.
                        // The first three values in the color data are red , green , and blue . They are full unsigned
                        //  16-bit values as in Apple's RGBColor data structure. Pure red = 65535, 0, 0.

                        red = value1 / 256;
                        green = value2 / 256;
                        blue = value3 / 256;

                        results.Add(System.Windows.Media.Color.FromRgb((byte) red, (byte) green, (byte) blue));
                        break;

                    case AdobePhotoshopColorSwatchColorSpace.Hsb:
                        double hue;
                        double saturation;
                        double brightness;

                        // HSB.
                        // The first three values in the color data are hue , saturation , and brightness . They are full
                        // unsigned 16-bit values as in Apple's HSVColor data structure. Pure red = 0,65535, 65535.

                        hue = value1 / 182.04;
                        saturation = value2 / 655.35;
                        brightness = value3 / 655.35;

                        results.Add(new HslColor(hue, saturation, brightness).ToRgbColorNew());
                        break;

                    case AdobePhotoshopColorSwatchColorSpace.Grayscale:

                        int gray;

                        // Grayscale.
                        // The first value in the color data is the gray value, from 0...10000.
                        gray = (int) (value1 / 39.0625);

                        results.Add(System.Windows.Media.Color.FromRgb((byte) gray, (byte) gray, (byte) gray));
                        break;

                    default:
                        throw new InvalidDataException($"Color space '{colorSpace}' not supported.");
                }

#if USENAMEHACK
        results.SetName(i, name);
#endif
            }

            return results;
        }

        protected virtual void WritePaletteNew(Stream stream, ColorCollection palette,
            AdobePhotoshopColorSwatchFileVersion version, AdobePhotoshopColorSwatchColorSpace colorSpace)
        {
            WriteInt16(stream, (short) version);
            WriteInt16(stream, (short) palette.Count);

            var swatchIndex = 0;

            foreach (var color in palette)
            {
                short value1;
                short value2;
                short value3;
                short value4;

                swatchIndex++;

                switch (colorSpace)
                {
                    case AdobePhotoshopColorSwatchColorSpace.Rgb:
                        value1 = (short) (color.R * 256);
                        value2 = (short) (color.G * 256);
                        value3 = (short) (color.B * 256);
                        value4 = 0;
                        break;
                    case AdobePhotoshopColorSwatchColorSpace.Hsb:
                        value1 = (short) (color.GetHue() * 182.04);
                        value2 = (short) (color.GetSaturation() * 655.35);
                        value3 = (short) (color.GetBrightness() * 655.35);
                        value4 = 0;
                        break;
                    case AdobePhotoshopColorSwatchColorSpace.Grayscale:
                        if (color.R == color.G && color.R == color.B)
                        {
                            // already grayscale
                            value1 = (short) (color.R * 39.0625);
                        }
                        else
                        {
                            // color is not grayscale, convert
                            value1 = (short) ((color.R + color.G + color.B) / 3.0 * 39.0625);
                        }

                        value2 = 0;
                        value3 = 0;
                        value4 = 0;
                        break;
                    default:
                        throw new InvalidOperationException("Color space not supported.");
                }

                WriteInt16(stream, (short) colorSpace);
                WriteInt16(stream, value1);
                WriteInt16(stream, value2);
                WriteInt16(stream, value3);
                WriteInt16(stream, value4);

                if (version == AdobePhotoshopColorSwatchFileVersion.Version2)
                {
#if USENAMEHACK
          name = palette.GetName(swatchIndex - 1);
          if (string.IsNullOrEmpty(name))
          {
            name = string.Format("Swatch {0}", swatchIndex);
          }
#else
                    var name = color.IsNamedColor() ? color.Name() : $"Swatch {swatchIndex}";
#endif

                    WriteInt32(stream, name.Length);
                    WriteString(stream, name);
                }
            }
        }

        #endregion
    }
}