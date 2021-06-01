using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Helpers
{
    public class UploadService
    {
        //PathProvider pathProvider;
        CloudStorageAccount account;
        //private String bucketName;
        //private IAmazonS3 awsClient;
        //public UploadService(PathProvider pathProvider, IConfiguration configuration, IAmazonS3 awsclient)
        public UploadService()
        {
            //this.awsClient = awsclient;
            //this.bucketName = configuration["AWSS3:BucketName"];
            //this.pathProvider = pathProvider;
            //this.account =
            //    CloudStorageAccount.Parse(configuration["storageaccount"]);
            this.account =
                CloudStorageAccount.Parse
                ("DefaultEndpointsProtocol=https;AccountName=onlyfood;AccountKey=lNivzNxf5mimZBUXW61LcuWLipIslVE0Q0TyYS/HOSbEQAPbT+MngiO6fwb5bhLxLQIG+6Z/upNpdTXcm/TuWw==;EndpointSuffix=core.windows.net");
        }
        //public async Task<String> UploadFileAsycn(IFormFile fichero, Folders folder)
        //{
        //    String name = HelperToolkit.NormalizeName(fichero.FileName);
        //    String path =
        //        this.pathProvider.MapPath(name
        //            , folder);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await fichero.CopyToAsync(stream);
        //    }
        //    return name;
        //}
        public async Task<String> UploadImageBlobAzureAsycn(IFormFile fichero)
        {
            String name = HelperToolkit.NormalizeName(fichero.FileName).Substring(0, 20);
            var blobClient = this.account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("imagenes");
            //await container.CreateIfNotExistsAsync();
            //container.SetPermissionsAsync(new BlobContainerPermissions()
            //{
            //    PublicAccess = BlobContainerPublicAccessType.Blob
            //});
            var blob = container.GetBlockBlobReference(name);
            using (var stream = fichero.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }
            return name;
        }

        public async Task<String> UploadImageBlobAzureAsycn(Stream stream, String nombre)
        {
            String name = HelperToolkit.NormalizeName(nombre);
            var blobClient = this.account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("imagenes");
            //await container.CreateIfNotExistsAsync();
            //container.SetPermissionsAsync(new BlobContainerPermissions()
            //{
            //    PublicAccess = BlobContainerPublicAccessType.Blob
            //});
            var blob = container.GetBlockBlobReference(name);
                await blob.UploadFromStreamAsync(stream);
            //using (var stream = fichero.OpenReadStream())
            //{
            //    await blob.UploadFromStreamAsync(stream);
            //}
            return name;
        }

        //public async Task<String> UploadImageBlobAzureAsycn(Stream stream, String name)
        //{
        //    //String name = HelperToolkit.NormalizeName(fichero.FileName);
        //    var blobClient = this.account.CreateCloudBlobClient();
        //    var container = blobClient.GetContainerReference("imagenes");
        //    //await container.CreateIfNotExistsAsync();
        //    //container.SetPermissionsAsync(new BlobContainerPermissions()
        //    //{
        //    //    PublicAccess = BlobContainerPublicAccessType.Blob
        //    //});
        //    var blob = container.GetBlockBlobReference(name);
        //    await blob.UploadFromStreamAsync(stream);
        //    return name;
        //}
        //public async Task<bool> UploadFileAWSAsync(Stream stream, String filename)
        //{
        //    PutObjectRequest request = new PutObjectRequest()
        //    {
        //        InputStream = stream,
        //        Key = filename,
        //        BucketName = this.bucketName,
        //        CannedACL = S3CannedACL.PublicRead
        //    };
        //    PutObjectResponse response =
        //        await this.awsClient.PutObjectAsync(request);
        //    if (response.HttpStatusCode == HttpStatusCode.OK)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
