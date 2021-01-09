using System.IO;
using BookClient;

namespace MyTunes
{
    class StreamLoader : IStreamLoader
    {
        public Stream GetStreamForFilename(string filename)
        {
            return File.OpenRead(filename);
        }
    }
}