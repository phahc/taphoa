using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace CMN.DataAccessLayer
{
    /// <summary>
    /// BlobBase.vb
    /// 各種Blobコンテナにアクセスするための基底クラス
    /// </summary>
    /// <remarks>
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    public abstract class BlobBase
    {
        /// <summary>
        /// Blobコンテナ取得
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// MustOverrideとSharedが同時に指定できないのでShared+Shared Shadowsで対応
        /// </remarks>
        public static CloudBlobContainer GetBlobContainer()
        {
            throw new Exception("GetBlobContainerを実装してください。");
        }

        /// <summary>
        /// Blobコンテナ取得
        /// </summary>
        /// <param name="containerName">コンテナ名</param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static CloudBlobContainer GetBlobContainerFromName(string containerName)
        {
            // 構成情報の接続文字列 "StorageConnectionString" からアカウントを取得
            CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            //BlobClientを取得
            CloudBlobClient client = account.CreateCloudBlobClient();
            return client.GetContainerReference(containerName);
        }

        /// <summary>
        /// Profile Blobコンテナへのパブリックなアクセス権限の設定
        /// </summary>
        /// <param name="container"></param>
        /// <remarks></remarks>
        protected static void SetPermissions(ref CloudBlobContainer container)
        {
            dynamic permissions = container.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            container.SetPermissions(permissions);
        }

    }
}
