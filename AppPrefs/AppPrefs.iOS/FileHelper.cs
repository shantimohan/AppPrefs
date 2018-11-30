using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(XPA_FileOps_XFPU.iOS.FileHelper))]

namespace XPA_FileOps_XFPU.iOS
{
	class FileHelper : IFileHelper
	{
		public string GetFilePath (string filename)
		{
			string docsPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			return Path.Combine (docsPath, filename);
		}

		string GetDocsFolder()
		{
			return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
		}

		public Task<bool> ExistsAsync (string filename)
		{
			bool exists = File.Exists (GetFilePath (filename));
			return Task<bool>.FromResult (exists);
		}

		public async Task WriteTextAsync (string filename, string text)
		{
			string filepath = GetFilePath (filename);
			using (StreamWriter writer = File.CreateText (filepath)) {
				await writer.WriteAsync (text);
			}
			
		}

		public async Task<string> ReadTextAsync (string filename)
		{
			string filepath = GetFilePath (filename);
			using (StreamReader reader = File.OpenText (filepath)) {
				return await reader.ReadToEndAsync ();
			}
		}

		public Task<IEnumerable<string>> GetFilesAsync()
		{
			// Sort the filenames
			IEnumerable<string> filenames =
				from filepath in Directory.EnumerateFiles (GetDocsFolder ())
				select Path.GetFileName (filepath);

			return Task<IEnumerable<string>>.FromResult (filenames);
		}

		public Task DeleteFileAsync(string filename)
		{
			File.Delete (GetFilePath (filename));
			return Task.FromResult (true);
		}

		//public void CopyPhoto(string srcFilename, string destFilename)
		//{

		//}

		//public async Task CopyPhoto(ImageSource imageSource, string destFilename, string destFileExt)
		//{
		//	await DeleteFileAsync (destFilename);
		//}
	}
}

