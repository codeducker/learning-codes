using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace FileIoSystemConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // CompressFile(@"C:\Users\PCSW015--PC\Desktop\testWrite.txt", @"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹\testWrite.dfl");
            //
            // DecompressFile( @"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹\testWrite.dfl",@"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹\testWrite.txt");

            // CompressString("Hello World");

            // CompressFile(@"C:\Users\PCSW015--PC\Desktop\testWrite.txt", @"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹\testWrite.zip");

            // DecompressFile(@"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹\testWrite.zip");

            // WriteFileUsingBinaryWriter(@"C:\Users\PCSW015--PC\Desktop\bin.data");
            //
            // ReadFileUsingBinaryReader(@"C:\Users\PCSW015--PC\Desktop\bin.data");

            // RandomAccessSample();

            // ReadFileUsingReader(@"C:\Users\PCSW015--PC\Desktop\samplefile.data");

            // CreateSampleFile(10000000);


            // CopyStream(true,@"C:\Users\PCSW015--PC\Desktop\testWrite.txt", @"C:\Users\PCSW015--PC\Desktop\testWriteCopy.txt");

            // WriteStream(@"C:\Users\PCSW015--PC\Desktop\testWrite1.txt","你好");

            // ReadStream(@"C:\Users\PCSW015--PC\Desktop\testWrite.txt");

            // var enumerateFiles = Directory.EnumerateFiles(@"C:\Users\PCSW015--PC\Desktop","ta*",SearchOption.AllDirectories);
            // foreach (var file in enumerateFiles)
            // {
            //     Console.WriteLine(file);
            // }
            // var enumerateDirectories = Directory.EnumerateDirectories(@"C:\Users\PCSW015--PC\Desktop","*");
            // foreach (var directory in enumerateDirectories)
            // {
            //     Console.WriteLine(directory);
            // }

            // File.AppendAllLines(Path.Combine(@"C:\Users\PCSW015--PC\Desktop", @"testWrite.txt"), new string[] { "Append Line" });

            // File.WriteAllLines(Path.Combine(@"C:\Users\PCSW015--PC\Desktop", @"testWrite.txt"),new string[]{"new Line"});

            // ReadFileLines(Path.Combine(@"C:\Users\PCSW015--PC\Desktop", @"testWrite.txt"),false);

            // PrintFileInfo(Path.Combine(@"C:\Users\PCSW015--PC\Desktop", @"testWrite.txt"));

            // WriteFile(Path.Combine(@"C:\Users\PCSW015--PC\Desktop", @"testWrite.txt"),"Hello World File");

            // GetDocumentsFolder();

            // Console.WriteLine(Path.Combine(@"c:","readme.txt"));

            // CopyFile(@"C:\Users\PCSW015--PC\Desktop\testWrite.txt", @"C:\Users\PCSW015--PC\Desktop\copyTestWrite.txt",false);
            // CopyFile(@"C:\Users\PCSW015--PC\Desktop\testWrite.txt", @"C:\Users\PCSW015--PC\Desktop\copyTestWrite.txt",true);

            // ListDriverInfos();

            // CreateZipFile(@"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹",
                // @"C:\Users\PCSW015--PC\Desktop\zipper\testWrite.zip");

            // WatchGuard(@"C:\Users\PCSW015--PC\Desktop\zipper\新建文件夹");

            Task.Run(() => WriterAsync());
            Task.Run(() => Reader());
            Console.ReadLine();
        }

        private static readonly ManualResetEventSlim MapCreated =
            new ManualResetEventSlim(initialState: false);
        private static readonly ManualResetEventSlim DataWrittenEvent =
            new ManualResetEventSlim(initialState: false);

        private const string MapName = "SimpleMap";

        private static  void Reader()
        {
            try
            {
                Console.WriteLine("reader");
                MapCreated.Wait();
                Console.WriteLine("reader starting");
                using (MemoryMappedFile mappedFile = MemoryMappedFile.OpenExisting(
                           MapName, MemoryMappedFileRights.Read))
                {
                    using (MemoryMappedViewAccessor accessor = mappedFile.CreateViewAccessor(
                               0, 10000, MemoryMappedFileAccess.Read))
                    {
                        DataWrittenEvent.Wait();
                        Console.WriteLine("reading can start now");
                        for (int i = 0; i < 400; i += 4)
                        {
                            int result = accessor.ReadInt32(i);
                            Console.WriteLine($"reading {result} from position {i}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"reader {ex.Message}");
            }
        }

        private static async Task WriterAsync()
        {
            try
            {
                using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateOrOpen(
                           MapName, 10000, MemoryMappedFileAccess.ReadWrite))
                {
                    MapCreated.Set(); // signal shared memory segment created
                    Console.WriteLine("shared memory segment created");
                    using (MemoryMappedViewAccessor accessor = mappedFile.CreateViewAccessor(
                               0, 10000, MemoryMappedFileAccess.Write))
                    {
                        for (int i = 0, pos = 0; i < 100; i++, pos += 4)
                        {
                            accessor.Write(pos, i);
                            Console.WriteLine($"written {i} at position {pos}");
                            await Task.Delay(10);
                        }
                        DataWrittenEvent.Set(); // signal all data written
                        Console.WriteLine("data written");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"writer {ex.Message}");
            }
        }

        private static void WatchGuard(string path)
        {
           var watcher = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = true
            };
           watcher.Changed += Change;
           Console.ReadLine();
        }

        private static void Change(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"file : {e.Name} , {e.FullPath} ,{e.ChangeType}");
        }

        private static void CreateZipFile(string directory, string zipFile)
        {
            FileStream zipStream = File.OpenWrite(zipFile);
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                IEnumerable<string> files = Directory.EnumerateFiles(
                    directory, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                    using (FileStream inputStream = File.OpenRead(file))
                    using (Stream outputStream = entry.Open())
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }


        private static void CompressFile(string originalFileName,string compressedFileName)
        {
            using FileStream originalFileStream = File.Open(originalFileName, FileMode.Open);
            using FileStream compressedFileStream = File.Create(compressedFileName);
            using var compressor = new DeflateStream(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);
        }

        private static void DecompressFile(string compressedFileName, string decompressedFileName)
        {
            using FileStream compressedFileStream = File.Open(compressedFileName, FileMode.Open);
            using FileStream outputFileStream = File.Create(decompressedFileName);
            using var deCompressor = new DeflateStream(compressedFileStream, CompressionMode.Decompress);
            deCompressor.CopyTo(outputFileStream);
        }

        private static byte[] CompressString(string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return CompressBytes(bytes);
        }

        private static byte[] CompressBytes(byte[] str)
        {
            var ms = new MemoryStream(str) { Position = 0 };
            var outms = new MemoryStream();
            using (var deflateStream = new DeflateStream(outms, CompressionMode.Compress, true))
            {
                var buf = new byte[1024];
                int len;
                while ((len = ms.Read(buf, 0, buf.Length)) > 0)
                    deflateStream.Write(buf, 0, len);
            }

            byte[] bytes = outms.ToArray();
            return bytes;
        }

        private static void DecompressFile(string fileName)
        {
            FileStream inputStream = File.OpenRead(fileName);
            using (MemoryStream outputStream = new MemoryStream())
            using (var compressStream = new DeflateStream(inputStream,
                       CompressionMode.Decompress))
            {
                compressStream.CopyTo(outputStream);
                outputStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outputStream, Encoding.UTF8,
                           detectEncodingFromByteOrderMarks: true, bufferSize: 4096,
                           leaveOpen: true))
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
        }

        public static void ListFiles(string fileName)
        {
            if (Directory.Exists(fileName))
            {
                //当前传入的为文件夹,此时需要将该文件夹下的所有文件进行压缩
                var enumerateFileSystemEntries = Directory.EnumerateFileSystemEntries(fileName);
                foreach (var entry in enumerateFileSystemEntries)
                {
                    Console.WriteLine(entry);
                }
            }
            else if (File.Exists(fileName))
            {
                //当前传入的为单文件
            }
            else
            {
                Console.WriteLine("Illegal Path");
            }
        }


        //读取二进制文件
        private static void ReadFileUsingBinaryReader(string binFile)
        {
            var inputStream = File.Open(binFile, FileMode.Open);
            using var reader = new BinaryReader(inputStream);
            double d = reader.ReadDouble();
            int i = reader.ReadInt32();
            long l = reader.ReadInt64();
            string s = reader.ReadString();
            Console.WriteLine($"d: {d}, i: {i}, l: {l}, s: {s}");
        }


        //写入二进制文件
        public static void WriteFileUsingBinaryWriter(string binFile)
        {
            var outputStream = File.Create(binFile);
            using var writer = new BinaryWriter(outputStream);
            double d = 47.47;
            int i = 42;
            long l = 987654321;
            string s = "sample";
            writer.Write(d);
            writer.Write(i);
            writer.Write(l);
            writer.Write(s);
        }

        private static void WriteFileUsingWriter(string fileName, string[] lines)
        {
            var outputStream = File.OpenWrite(fileName);
            using var writer = new StreamWriter(outputStream);
            byte[] preamble = Encoding.UTF8.GetPreamble();
            outputStream.Write(preamble, 0, preamble.Length);
            writer.Write(lines);
        }

        public static void ReadFileUsingReader(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                FileShare.Read);
            using var reader = File.OpenText(fileName);
            // using var reader = new StreamReader(stream);//这里可以指定字符编码
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                Console.WriteLine(line);
            }
        }

        public static void RandomAccessSample()
        {
            try
            {
                const int recordSize = 4096;
                string sampleFilePath = Path.Combine(@"C:\Users\PCSW015--PC\Desktop", "samplefile.data");
                using FileStream stream = File.OpenRead(sampleFilePath);
                byte[] buffer = new byte[recordSize];
                do
                {
                    try
                    {
                        Console.Write("record number (or 'bye' to end): ");
                        string? line = Console.ReadLine();
                        if (String.Compare(line?.ToUpper(), "BYE", StringComparison.Ordinal) == 0) break;
                        if (int.TryParse(line, out var record))
                        {
                            stream.Seek((record - 1) * recordSize, SeekOrigin.Begin);
                            var read = stream.Read(buffer, 0, recordSize);
                            string s = Encoding.UTF8.GetString(buffer);
                            Console.WriteLine($"record: {s}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (true);
                Console.WriteLine("finished");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Create the sample file using the option -sample first");
            }
        }


        public static async Task CreateSampleFile(int nRecords)
        {
            string SampleFilePath = Path.Combine(@"C:\Users\PCSW015--PC\Desktop","samplefile.data");
            FileStream stream = File.Create(SampleFilePath);
            using (var writer = new StreamWriter(stream))
            {
                var r = new Random();
                var records = Enumerable.Range(0, nRecords).Select(x => new
                {
                    Number = x,
                    Text = $"Sample text {r.Next(200)}",
                    Date = new DateTime(Math.Abs((long)((r.NextDouble() * 2 - 1) *
                                                        DateTime.MaxValue.Ticks)))
                });
                foreach (var rec in records)
                {
                    string date = rec.Date.ToString("d", CultureInfo.InvariantCulture);
                    string s =
                        $"#{rec.Number,8}; {rec.Text,-20}; {date}#{Environment.NewLine}";
                    await writer.WriteAsync(s);
                }
            }
        }

        private static void CopyStream(bool isMethod,string source, string target)
        {
            using var fs = File.OpenRead(source);
            using var fw = File.OpenWrite(target);
            if (!isMethod)
            {
                const int bufferSize = 4096;
                bool completed = false;
                byte[] buffer = new byte[bufferSize];
                while (!completed)
                {
                    var read = fs.Read(buffer, 0, bufferSize);
                    if (0 == read) completed = true;
                    if (read > 0) fw.Write(buffer, 0, bufferSize);
                }
            }
            else
            {
                fs.CopyTo(fw);
            }
        }

        private static void WriteStream(string fileName, string content)
        {
            using var fw = File.OpenWrite(fileName);
            fw.Write(Encoding.UTF8.GetBytes(content));
        }

        private static void ReadStream(string fileName)
        {

            using (FileStream fs = File.OpenRead(fileName))
            {
                GetEncoding(fs);
            }

            using(var fs = new FileStream(fileName,FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                var encoding = GetEncoding(fs);
                // Console.WriteLine(encoding);
                const int bufferSize = 4096;
                var bytes = new byte[bufferSize];
                bool completed = false;
                do
                {
                    var read = fs.Read(bytes, 0, bufferSize);
                    if (0 == read) completed = true;
                    if (read < bufferSize)
                    {
                        Array.Clear(bytes, read, (bufferSize - read));
                    }
                    var content = encoding.GetString(bytes, 0, read);
                    Console.WriteLine(content);
                } while (!completed);
            }

       
        }

        private static Encoding GetEncoding(Stream stream)
        {
            if (!stream.CanSeek) throw new ArgumentException(
                "require a stream that can seek");
            Encoding encoding = Encoding.ASCII;
            byte[] bom = new byte[5];
            int nRead = stream.Read(bom, offset: 0, count: 5);
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0)
            {
                Console.WriteLine("UTF-32");
                stream.Seek(4, SeekOrigin.Begin);
                return Encoding.UTF32;
            }
            else if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                Console.WriteLine("UTF-16, little Ending");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.Unicode;
            }
            else if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                Console.WriteLine("UTF-16, big Ending");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.BigEndianUnicode;
            }
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                Console.WriteLine("UTF-8");
                stream.Seek(3, SeekOrigin.Begin);
                return Encoding.UTF8;
            }
            else
            {
                Console.WriteLine("Nothing!");
            }

            stream.Seek(0, SeekOrigin.Begin);
            return encoding;
        }

        private static void ReadFileLines(string fileName,bool isHoldFile)
        {
            IEnumerable<string> readAllLines;

            if (isHoldFile)
            {
                readAllLines = File.ReadAllLines(fileName);
            }
            else
            {
                readAllLines = File.ReadLines(fileName);
            }

            foreach (var readAllLine in readAllLines)
            {
                Console.WriteLine(readAllLine);
            }
        }

        private static void WriteFile(string fileName,string content)
        {
            var combine = Path.Combine(fileName);
            File.WriteAllText(combine, content);
        }

        private static void PrintFileInfo(string fileName)
        {
            var file = new FileInfo(fileName);
            Console.WriteLine($"Name: {file.Name}");
            Console.WriteLine($"Directory: {file.DirectoryName}");
            Console.WriteLine($"Read only: {file.IsReadOnly}");
            Console.WriteLine($"Extension: {file.Extension}");
            Console.WriteLine($"Length: {file.Length}");
            Console.WriteLine($"Creation time: {file.CreationTime:F}");
            Console.WriteLine($"Access time: {file.LastAccessTime:F}");
            Console.WriteLine($"File attributes: {file.Attributes}");
        }

        private static void CopyFile(string sourceFile,string targetFile,bool staticMethod)
        {
            if (staticMethod)
            {
                File.Copy(sourceFile, targetFile);
            }
            else
            {
                var fileInfo = new FileInfo(sourceFile);
                fileInfo.CopyTo(targetFile);
            }
        }

        private static void ListDriverInfos()
        {
            var driveInfos = DriveInfo.GetDrives();
            foreach (var driveInfo in driveInfos)
            {
                if (driveInfo.IsReady)
                {
                    Console.WriteLine(driveInfo.Name);
                    Console.WriteLine($"Format: {driveInfo.DriveFormat}");
                    Console.WriteLine($"Type: {driveInfo.DriveType}");
                    Console.WriteLine($"Root directory: {driveInfo.RootDirectory}");
                    Console.WriteLine($"Volume label: {driveInfo.VolumeLabel}");
                    Console.WriteLine($"Free space: {driveInfo.TotalFreeSpace}");
                    Console.WriteLine($"Available space: {driveInfo.AvailableFreeSpace}");
                    Console.WriteLine($"Total size: {driveInfo.TotalSize}");
                }
            }
        }

        private static string GetDocumentsFolder()
        {
#if NET46
          return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#else
            string? drive = Environment.GetEnvironmentVariable("HOMEDRIVE");
            string? path = Environment.GetEnvironmentVariable("HOMEPATH");
            return Path.Combine(drive??"", path??"", "documents");
#endif
        }
    }
}