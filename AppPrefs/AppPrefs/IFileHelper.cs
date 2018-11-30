using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

namespace XPA_FileOps_XFPU
{
	public interface IFileHelper
	{
		string GetFilePath (string filename);

		Task<bool> ExistsAsync (string filename);

		Task WriteTextAsync (string filename, string text);

		Task<string> ReadTextAsync (string filename);

        Task<IEnumerable<string>> GetFilesAsync();

        Task DeleteFileAsync(string filename);

  //      void CopyPhoto(string srcFilename, string destFilename);

		//Task CopyPhoto(ImageSource imageSource, string destFilename, string destFileExt);
	}
}

