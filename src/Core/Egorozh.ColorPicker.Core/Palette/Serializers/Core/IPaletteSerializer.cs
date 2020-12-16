﻿/*
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
using System.IO;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Serializes and deserializes color palettes into and from other documents.
    /// </summary>
    public interface IPaletteSerializer
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to read palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be read using this serializer; otherwise, <c>false</c>.</value>
        bool CanRead { get; }

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to write palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be written using this serializer; otherwise, <c>false</c>.</value>
        bool CanWrite { get; }

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        string DefaultExtension { get; }

        /// <summary>
        /// Gets the maximum number of colors supported by this format.
        /// </summary>
        /// <value>
        /// The maximum value number of colors supported by this format.
        /// </value>
        int Maximum { get; }

        /// <summary>
        /// Gets the minimum numbers of colors supported by this format.
        /// </summary>
        /// <value>
        /// The minimum number of colors supported by this format.
        /// </value>
        int Minimum { get; }

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can read palette data from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if this instance can read palette data from the specified stream; otherwise, <c>false</c>.</returns>
        bool CanReadFrom(Stream stream);    
        
        ColorCollection? DeserializeNew(Stream stream);
            
        void Serialize(Stream stream, ColorCollection palette);

        #endregion
    }
}