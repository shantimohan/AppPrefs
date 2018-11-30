using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XPA_FileOps_XFPU
{
	static class FileHelper
	{
		static IFileHelper fileHelper = DependencyService.Get<IFileHelper> ();

		public static string GetFilePath (string filename)
		{
			return fileHelper.GetFilePath (filename);
		}

		public static Task<bool> ExistsAsync(string filename)
		{
			return fileHelper.ExistsAsync (filename);
		}

		public static Task WriteTextAsync (string filename, string text)
		{
			return fileHelper.WriteTextAsync(filename, text);
		}

		public static Task<string> ReadTextAsync (string filename)
		{
			return fileHelper.ReadTextAsync (filename);
		}

        public static Task<IEnumerable<string>> GetFilesAsync()
        {
            return fileHelper.GetFilesAsync();
        }

        public static Task DeleteFileAsync(string filename)
        {
            return fileHelper.DeleteFileAsync(filename);
        }

  //      public static void CopyPhoto(string srcFilename, string destFilename)
  //      {
  //          fileHelper.CopyPhoto(srcFilename, destFilename);
  //      }

		//public static Task CopyPhoto(ImageSource imageSource, string destFilename, string destFileExt)
  //      {
		//	return fileHelper.CopyPhoto(imageSource, destFilename, destFileExt);
  //      }
	}
}

