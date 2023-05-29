using Microsoft.WindowsAzure.Storage.Blob;

namespace CMN.DataAccessLayer
{
    /// <summary>
    /// ProfileBlob.vb
    /// </summary>
    /// <remarks>
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    public class ProfileBlob : BlobBase
    {
        /// <summary>
        /// Profile Blobコンテナ初期化
        /// </summary>
        static ProfileBlob()
        {
            //Profile Blobコンテナの初期化
            dynamic container = GetBlobContainer();
            container.CreateIfNotExists();

            //Profile Blobコンテナへのパブリックなアクセス権限の設定
            SetPermissions(container);
        }

        /// <summary>
        /// FPGridViewで使用するprofile情報のBlobコンテナを取得
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static new CloudBlobContainer GetBlobContainer()
        {
            return GetBlobContainerFromName("profile");
        }
    }
}
