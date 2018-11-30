using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Xamarin.Forms;

[assembly: Dependency(typeof(XPA_FileOps_XFPU.UWP.FileHelper))]

namespace XPA_FileOps_XFPU.UWP
{
    class FileHelper : IFileHelper
    {
        public string GetFilePath(string filename)
        {
            return filename;
        }

        public async Task<bool> ExistsAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            bool rtn_value = true;

            try
            {
                await localFolder.GetFileAsync(filename);
            }
            catch
            {
                rtn_value = false;
            }

            return rtn_value;
        }

        public async Task WriteTextAsync(string filename, string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            IStorageFile storageFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter dataWriter = new DataWriter(stream))
                {
                    dataWriter.WriteString(text);
                    await dataWriter.StoreAsync();
                }
            }
        }

        public async Task<string> ReadTextAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IStorageFile storageFile = await localFolder.GetFileAsync(filename);

            using (IRandomAccessStream stream = await storageFile.OpenReadAsync())
            {
                using (DataReader dataReader = new DataReader(stream))
                {
                    uint length = (uint)stream.Size;
                    await dataReader.LoadAsync(length);
                    return dataReader.ReadString(length);
                }
            }
        }

        public async Task<IEnumerable<string>> GetFilesAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            IEnumerable<string> filenames =
                from storageFile in await localFolder.GetFilesAsync()
                select storageFile.Name;

            return filenames;
        }

        public async Task DeleteFileAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.GetFileAsync(filename);
            await storageFile.DeleteAsync();
        }

        //public void CopyPhoto(string srcFilename, string destFilename)
        //{
        //    // TODO: copy pic from Pictures folderr to appdata folderr
        //    try
        //    {
        //        StorageFolder picsFolder = KnownFolders.PicturesLibrary;
        //        // string picsPath = "";
        //        string picsPath = picsFolder.Path;
        //        string picsFile = picsPath + srcFilename;

        //        StorageFolder localFoler = ApplicationData.Current.LocalFolder;
        //        string destPath = localFoler.Path;
        //        string destFile = destPath + destFilename;

        //        File.Copy(picsFile, destFile, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        //await DisplayAlert("Copy Picture", ex.Message, "OK");
        //    }
        //}

        //public async Task CopyPhoto(ImageSource imageSource, string destFilename, string destFileExt)
        //{
        //    await DeleteFileAsync(destFilename);
        //}
    }
}
