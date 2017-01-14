namespace Vsd.Common
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public static class ByteExtensions
    {
        private const int BufferSize = 64 * 1024;

        public static byte[] Compress(this byte[] inputData)
        {
            using (var compressIntoMs = new MemoryStream())
            {
                using (
                    var gzs = new BufferedStream(new GZipStream(compressIntoMs, CompressionMode.Compress), BufferSize))
                {
                    gzs.Write(inputData, 0, inputData.Length);
                }
                return compressIntoMs.ToArray();
            }
        }

        public static byte[] Decompress(this byte[] inputData)
        {
            using (var compressedMs = new MemoryStream(inputData))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (
                        var gzs = new BufferedStream(
                            new GZipStream(compressedMs, CompressionMode.Decompress),
                            BufferSize))
                    {
                        gzs.CopyTo(decompressedMs);
                    }
                    return decompressedMs.ToArray();
                }
            }
        }

        public static float[] ConvertToFloatArray(this byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
            {
                throw new ArgumentException();
            }

            var floats = new float[bytes.Length / 4];

            for (var i = 0; i < floats.Length; i++)
            {
                floats[i] = BitConverter.ToSingle(bytes, i * 4);
            }

            return floats;
        }
    }
}