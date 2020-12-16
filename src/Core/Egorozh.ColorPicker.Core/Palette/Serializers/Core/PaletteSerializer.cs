using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Serializes and deserializes color palettes into and from other documents.
    /// </summary>
    public abstract class PaletteSerializer : IPaletteSerializer
    {
        #region Private Static Fields

        private static List<(string, List<string>)>? _defaultOpenFilterForAvalonia;
        private static List<(string, List<string>)>? _defaultSaveFilterForAvalonia;

        private static readonly List<IPaletteSerializer> SerializerCache = new();

        #endregion

        #region Static Properties

        /// <summary>
        /// Gets all loaded serializers.
        /// </summary>
        /// <value>The loaded serializers.</value>
        public static IEnumerable<IPaletteSerializer> AllSerializers => SerializerCache.AsReadOnly();

        /// <summary>
        /// Returns a filter suitable for use with the <see cref="System.Windows.OpenFileDialog"/>.
        /// </summary>
        /// <value>A filter suitable for use with the <see cref="System.Windows.OpenFileDialog"/>.</value>
        /// <remarks>This filter does not include any serializers that cannot read source data.</remarks>
        public static string? DefaultOpenFilterForWpf
        {
            get
            {
                if (_defaultOpenFilterForAvalonia == null)
                    CreateFilters();

                return CreateWpfFilters(_defaultOpenFilterForAvalonia);
            }
        }

        public static List<(string, List<string>)>? DefaultOpenFilterForAvalonia
        {
            get
            {
                if (_defaultOpenFilterForAvalonia == null)
                    CreateFilters();

                return _defaultOpenFilterForAvalonia;
            }
        }

        public static string? DefaultSaveFilterForWpf
        {
            get
            {
                if (_defaultSaveFilterForAvalonia == null)
                    CreateFilters();

                return CreateWpfFilters(_defaultSaveFilterForAvalonia);
            }
        }

        public static List<(string, List<string>)>? DefaultSaveFilterForAvalonia
        {
            get
            {
                if (_defaultSaveFilterForAvalonia == null)
                    CreateFilters();

                return _defaultSaveFilterForAvalonia;
            }
        }

        #endregion

        #region Static Methods

        public static IPaletteSerializer? GetSerializer(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException($"Cannot find file '{fileName}'.", fileName);

            if (SerializerCache.Count == 0)
                LoadSerializers();

            IPaletteSerializer? result = null;

            foreach (IPaletteSerializer checkSerializer in AllSerializers)
            {
                using FileStream file = File.OpenRead(fileName);

                if (checkSerializer.CanReadFrom(file))
                {
                    result = checkSerializer;
                    break;
                }
            }

            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to read palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be read using this serializer; otherwise, <c>false</c>.</value>
        public virtual bool CanRead => true;

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to write palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be written using this serializer; otherwise, <c>false</c>.</value>
        public virtual bool CanWrite => true;

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        public abstract string[] DefaultExtension { get; }

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        public abstract string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can read palette from data the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if this instance can read palette data from the specified stream; otherwise, <c>false</c>.</returns>
        public abstract bool CanReadFrom(Stream stream);

        public abstract List<Color> DeserializeNew(Stream stream);

        public abstract void Serialize(Stream stream, IEnumerable<Color> palette);

        /// <summary>
        /// Reads a 16bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The unsigned 16bit integer cast to an <c>Int32</c>.</returns>
        protected int ReadInt16(Stream stream)
        {
            return (stream.ReadByte() << 8) | (stream.ReadByte() << 0);
        }

        /// <summary>
        /// Reads a 32bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The unsigned 32bit integer cast to an <c>Int32</c>.</returns>
        protected int ReadInt32(Stream stream)
        {
            // big endian conversion: http://stackoverflow.com/a/14401341/148962

            return ((byte) stream.ReadByte() << 24) | ((byte) stream.ReadByte() << 16) |
                   ((byte) stream.ReadByte() << 8) | (byte) stream.ReadByte();
        }

        /// <summary>
        /// Reads a unicode string of the specified length.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="length">The number of characters in the string.</param>
        /// <returns>The string read from the stream.</returns>
        protected string ReadString(Stream stream, int length)
        {
            byte[] buffer;

            buffer = new byte[length * 2];

            stream.Read(buffer, 0, buffer.Length);

            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        /// <summary>
        /// Writes a 16bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to write the data to.</param>
        /// <param name="value">The value to write</param>
        protected void WriteInt16(Stream stream, short value)
        {
            stream.WriteByte((byte) (value >> 8));
            stream.WriteByte((byte) (value >> 0));
        }

        /// <summary>
        /// Writes a 32bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to write the data to.</param>
        /// <param name="value">The value to write</param>
        protected void WriteInt32(Stream stream, int value)
        {
            stream.WriteByte((byte) ((value & 0xFF000000) >> 24));
            stream.WriteByte((byte) ((value & 0x00FF0000) >> 16));
            stream.WriteByte((byte) ((value & 0x0000FF00) >> 8));
            stream.WriteByte((byte) ((value & 0x000000FF) >> 0));
        }

        protected void WriteString(Stream stream, string value)
        {
            stream.Write(Encoding.BigEndianUnicode.GetBytes(value), 0, value.Length * 2);
        }

        #endregion

        #region IPaletteSerializer Interface

        /// <summary>
        /// Determines whether this instance can read palette data from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if this instance can read palette data from the specified stream; otherwise, <c>false</c>.</returns>
        bool IPaletteSerializer.CanReadFrom(Stream stream) => CanReadFrom(stream);

        /// <summary>
        /// Deserializes the <see cref="List<Color>" /> contained by the specified <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="Stream" /> that contains the palette to deserialize.</param>
        /// <returns>The <see cref="List<Color>" /> being deserialized.</returns>
        List<Color>? IPaletteSerializer.DeserializeNew(Stream stream) => DeserializeNew(stream);

        /// <summary>
        /// Serializes the specified <see cref="List<Color>" /> and writes the palette to a file using the specified Stream.
        /// </summary>
        /// <param name="stream">The <see cref="Stream" /> used to write the palette.</param>
        /// <param name="palette">The <see cref="List<Color>" /> to serialize.</param>
        void IPaletteSerializer.Serialize(Stream stream, IEnumerable<Color> palette)
        {
            Serialize(stream, palette);
        }

        /// <summary>
        /// Gets the maximum number of colors supported by this format.
        /// </summary>
        /// <value>
        /// The maximum value number of colors supported by this format.
        /// </value>
        public virtual int Maximum => int.MaxValue;

        /// <summary>
        /// Gets the minimum numbers of colors supported by this format.
        /// </summary>
        /// <value>
        /// The minimum number of colors supported by this format.
        /// </value>
        public virtual int Minimum => 1;

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to read palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be read using this serializer; otherwise, <c>false</c>.</value>
        bool IPaletteSerializer.CanRead => this.CanRead;

        /// <summary>
        /// Gets a value indicating whether this serializer can be used to write palettes.
        /// </summary>
        /// <value><c>true</c> if palettes can be written using this serializer; otherwise, <c>false</c>.</value>
        bool IPaletteSerializer.CanWrite => this.CanWrite;

        /// <summary>
        /// Gets the default extension for files generated with this palette format.
        /// </summary>
        /// <value>The default extension for files generated with this palette format.</value>
        string[] IPaletteSerializer.DefaultExtensions => this.DefaultExtension;

        /// <summary>
        /// Gets a descriptive name of the palette format
        /// </summary>
        /// <value>The descriptive name of the palette format.</value>
        string IPaletteSerializer.Name => this.Name;

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Creates the Open and Save As filters.
        /// </summary>
        private static void CreateFilters()
        {
            List<(string, List<string>)> openFilterA = new();
            List<(string, List<string>)> saveFilterA = new();

            if (SerializerCache.Count == 0)
                LoadSerializers();

            foreach (IPaletteSerializer serializer in SerializerCache.Where(serializer =>
                serializer.DefaultExtensions.Length > 0))
            {
                var filter = (serializer.Name, serializer.DefaultExtensions.ToList());

                if (serializer.CanRead)
                    openFilterA.Add(filter);

                if (serializer.CanWrite)
                    saveFilterA.Add(filter);
            }

            if (openFilterA.Count != 0)
            {
                List<string> allExts = openFilterA.SelectMany(f => f.Item2).Distinct().ToList();
                openFilterA.Insert(0, ("All Supported Palettes", allExts));
            }

            openFilterA.Add(("All Files", new() {"*"}));

            _defaultOpenFilterForAvalonia = openFilterA;
            _defaultSaveFilterForAvalonia = saveFilterA;
        }

        private static string CreateWpfFilters(List<(string, List<string>)> filters)
        {
            StringBuilder wpfFilterBuilder = new();

            foreach (var (filterName, extensions) in filters)
            {
                StringBuilder extensionMask = new();

                foreach (var extension in extensions)
                {
                    string mask = "*." + extension;

                    if (extensionMask.Length != 0)
                        extensionMask.Append(";");

                    extensionMask.Append(mask);
                }

                string wpfFilter = $"{filterName} Files ({extensionMask})|{extensionMask}";

                if (wpfFilterBuilder.Length != 0)
                    wpfFilterBuilder.Append("|");

                wpfFilterBuilder.Append(wpfFilter);
            }

            return wpfFilterBuilder.ToString();
        }

        /// <summary>
        /// Gets the loadable types from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly to load types from.</param>
        /// <returns>Available types</returns>
        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }

        /// <summary>
        /// Loads the serializers.
        /// </summary>
        private static void LoadSerializers()
        {
            SerializerCache.Clear();
            
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly =>
                GetLoadableTypes(assembly).Where(type =>
                    !type.IsAbstract && type.IsPublic && typeof(IPaletteSerializer).IsAssignableFrom(type))))
            {
                try
                {
                    SerializerCache.Add((IPaletteSerializer) Activator.CreateInstance(type));
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch
                    // ReSharper restore EmptyGeneralCatchClause
                {
                    // ignore errors
                }
            }

            // sort the cache by name, that way the open/save filters won't need independant sorting
            // and can easily map FileDialog.FilterIndex to an item in this collection
            SerializerCache.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));
        }

        #endregion
    }
}