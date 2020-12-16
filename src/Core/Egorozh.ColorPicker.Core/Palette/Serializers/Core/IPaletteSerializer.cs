using System.Collections.Generic;
using System.Drawing;
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
        string[] DefaultExtensions { get; }

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

        List<Color>? DeserializeNew(Stream stream);

        void Serialize(Stream stream, IEnumerable<Color> palette);

        #endregion
    }
}