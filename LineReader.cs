using System.Collections.Generic;
using System.IO;
using System;

namespace LogViewer
{
    internal class LineReader : IDisposable
    {
        private StreamReader streamReader;

        public bool Disposed { get; private set; }

        public bool FileOpened { get; private set; }

        public void OpenFile(string filePath)
        {
            if (Disposed)
            {
                throw new InvalidOperationException("This resource has already been disposed.");
            }
            else if (FileOpened)
            {
                throw new InvalidOperationException($"A file is already open. Use {nameof(CloseFile)} to close the file before opening another.");
            }

            streamReader = new StreamReader(filePath);
            FileOpened = true;
        }

        public void CloseFile()
        {
            if (Disposed)
            {
                throw new InvalidOperationException("This resource has already been disposed.");
            }
            else if (!FileOpened)
            {
                throw new InvalidOperationException($"No file is currently open. Use {nameof(OpenFile)} to load a file.");
            }

            streamReader.Close();
            FileOpened = false;
        }

        public void Dispose()
        {
            if (Disposed)
            {
                throw new InvalidOperationException("This resource has already been disposed.");
            }

            streamReader.Dispose();
            Disposed = true;
        }

        public IEnumerable<string> ReadLines(int numLines)
        {
            if (Disposed)
            {
                throw new InvalidOperationException("This resource has already been disposed.");
            }
            else if (!FileOpened)
            {
                throw new InvalidOperationException($"No file is currently open. Use {nameof(OpenFile)} to load a file before reading lines.");
            }

            int count = 0;
            string line;
            while ((line = streamReader.ReadLine()) != null && count < numLines)
            {
                count++;
                yield return line;
            }
        }
    }
}
