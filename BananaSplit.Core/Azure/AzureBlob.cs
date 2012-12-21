using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using BananaSplit.Core.Utility;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BananaSplit.Core.Azure
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class AzureBlob : AzureBase
    {
        private CloudBlobClient cloudBlobClient;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public AzureBlob()
        {
            this.InitClient();
        }

        private void InitClient()
        {
            this.cloudBlobClient = GetCloudStorageAccount().CreateCloudBlobClient();
        }

        /// <summary>
        /// Retrieves the Container (think of it as a folder) containing all your blobs (files)
        /// </summary>
        /// <param name="containerNameOrAddress">The name of URI of the Azure Container (folder)</param>
        /// <param name="createIfContainerDoesNotExist">Set this to true if you want to create the container at the time you access it the 1st time</param>
        /// <returns>The container (folder) that has all the blobs (files)</returns>
        /// 
        public CloudBlobContainer GetBlobContainer(string containerNameOrAddress, bool createIfContainerDoesNotExist = false)
        {
            var container = this.cloudBlobClient.GetContainerReference(containerNameOrAddress);
            if (createIfContainerDoesNotExist)
            {
                container.CreateIfNotExist();
            }
            return container;
        }

        /// <summary>
        /// Gets an enumerated list of blobs (files) contained within a specific container (folder)
        /// </summary>
        /// <param name="containerNameOrAddress">The name of URI of the Azure Container (folder)</param>
        /// <returns>Enumerated list of blobs (files)</returns>
        /// 
        public IEnumerable<IListBlobItem> GetListofBlobsInContainer(string containerNameOrAddress)
        {
            var container = this.GetBlobContainer(containerNameOrAddress);
            return (null != container)
                        ? container.ListBlobs()
                        : null;
        }

        /// <summary>
        /// Uploads a new file to Azure Blob Storage
        /// </summary>
        /// <param name="containerNameOrAddress">The name of the container (folder) to put this file into</param>
        /// <param name="blobName">The name you want the file to be in blob storage</param>
        /// <param name="filePath">The local file path to the file you wish to upload</param>
        /// <returns>true if successful, false if failed to upload</returns>
        /// 
        public Boolean UploadBlob(string containerNameOrAddress, string blobName, string filePath)
        {
            try
            {
                //Get a reference to an existing blob to overwrite or create the blob now
                var blob = this.GetBlobFromContainer(containerNameOrAddress, blobName, true);

                using (var fs = File.OpenRead(filePath))
                {
                    blob.UploadFromStream(fs);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Uploads a blob asynchronously
        /// </summary>
        /// <param name="containerNameOrAddress"></param>
        /// <param name="blobName"></param>
        /// <param name="filePath"></param>
        /// 
        public void UploadBlobAsync(string containerNameOrAddress, string blobName, string filePath, Action<IAsyncResult> completedAction = null)
        {
            //Get a reference to an existing blob to overwrite or create the blob now
            var blob = this.GetBlobFromContainer(containerNameOrAddress, blobName, true);

            using (var fs = File.OpenRead(filePath))
            {
                AsyncCallback callback = (IAsyncResult r) =>
                {
                    ((CloudBlob)r.AsyncState).EndUploadFromStream(r);
                    if (null != completedAction)
                    {
                        completedAction(r);
                    }
                };
                blob.BeginUploadFromStream(fs, new AsyncCallback(callback), null);
            }
        }

        /// <summary>
        /// Downloads a blob synchronously
        /// </summary>
        /// <param name="containerNameOrAddress"></param>
        /// <param name="blobName"></param>
        /// <param name="filePath"></param>
        /// 
        public void DownloadBlob(string containerNameOrAddress, string blobName, string filePath)
        {
            var blob = this.GetBlobFromContainer(containerNameOrAddress, blobName);
            using (var fs = File.OpenWrite(filePath))
            {
                blob.DownloadToStream(fs);
            }
        }


        /// <summary>
        /// Downloads a blob (file) asynchronously
        /// </summary>
        /// <param name="containerNameOrAddress"></param>
        /// <param name="blobName"></param>
        /// <param name="callback"></param>
        /// <param name="filePath"></param>
        /// 
        public void DownloadBlobAsync(string containerNameOrAddress, string blobName, string filePath, Action<IAsyncResult> completedAction = null)
        {
            //Get a reference to an existing blob to overwrite or create the blob now
            var blob = this.GetBlobFromContainer(containerNameOrAddress, blobName);
            using (var fs = File.OpenWrite(filePath))
            {
                AsyncCallback callback = (IAsyncResult r) =>
                {
                    ((CloudBlob)r.AsyncState).EndDownloadToStream(r);
                    //Call end user's action
                    if (null != completedAction)
                    {
                        completedAction(r);
                    }
                };
                blob.BeginDownloadToStream(fs, new AsyncCallback(callback), null);
            }
        }


        /// <summary>
        /// Deletes an existing blob
        /// </summary>
        /// <param name="containerNameOrAddress"></param>
        /// <param name="blobName"></param>
        /// 
        public void DeleteBlob(string containerNameOrAddress, string blobName)
        {
            var container = this.GetBlobContainer(containerNameOrAddress);
            var blob = container.GetBlobReference(blobName);
            // Delete the blob if it exists
            blob.DeleteIfExists();
        }


        /// <summary>
        /// Deletes an existing blob
        /// </summary>
        /// <param name="containerNameOrAddress"></param>
        /// <param name="blobName"></param>
        /// 
        public void DeleteBlobAsync(string containerNameOrAddress, string blobName, Action<IAsyncResult> completedAction = null)
        {
            var container = this.GetBlobContainer(containerNameOrAddress);
            var blob = container.GetBlobReference(blobName);
            // Delete the blob if it exists
            AsyncCallback callback = (IAsyncResult r) =>
            {
                ((CloudBlob)r.AsyncState).EndDeleteIfExists(r);
                //Call end user's action
                if (null != completedAction)
                {
                    completedAction(r);
                }
            };
            blob.BeginDeleteIfExists(callback, null);
        }


        /// <summary>
        /// Gets info about a single blob
        /// </summary>
        /// <param name="containerNameOrAddress"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        /// 
        public CloudBlob GetBlobFromContainer(string containerNameOrAddress, string blobName, bool createContainerIfNotExists = false)
        {
            var container = this.GetBlobContainer(containerNameOrAddress, createContainerIfNotExists);
            //Get a reference to an existing blob to overwrite or create the blob now
            var blob = container.GetBlobReference(blobName);

            return blob;
        }


    }
}
