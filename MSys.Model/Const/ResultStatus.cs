namespace MSys.Model.ConstData
{

    #region "処理結果区分"
    /// <summary>
    /// 処理結果区分
    /// </summary>
    /// <remarks></remarks>
    public class ResultStatus
    {

        //【更新成功】

        public const string EditSuccess = "U001";
        //【更新失敗】

        public const string EditFailed = "U002";
        //【登録成功】

        public const string RegistSuccess = "R001";
        //【登録失敗】

        public const string RegistFailed = "R002";
        //【削除成功】

        public const string DeleteSuccess = "D001";
        //【削除失敗】

        public const string DeleteFailed = "D002";
        //【一時的なエラーによる失敗(回復可能なのでユーザーにリトライを促す)】

        public const string FailedByTemporaryError = "E000";
        //【データが存在しない】

        public const string DataNotExists = "E001";
        //【データがすでに存在する】

        public const string DataDuplicate = "E002";
        //【更新済みの時】

        public const string DataAlreadyUpdated = "E003";
        //【削除不可データ】

        public const string DeleteNotTargetData = "D003";
        //'【正常】

        public const string NSuccess = "N000";
        //'【外部サービス連携エラー】（組版エンジンなどの外部サービスでエラーが発生した場合）

        public const string ExternalServiseError = "E099";
        //'【回復不能のエラー】(catchしたもの)

        public const string ECritical = "E001";
        //'【論理エラー】(プリント対象外ファイルエラー)

        public const string LNotTargetFile = "L001";
        //'【論理エラー】（プリント対象外バージョンのOfficeファイル）

        public const string LNotTargetOffice = "L002";
        //'【論理エラー】（複数ページ指定が行われているOfficeファイル）

        public const string LMultiPageOffice = "L003";
        //'【論理エラー】（未対応フォントが利用されている）

        public const string LNotTargetFont = "L004";
        //'【論理エラー】（PDFバージョン不一致）

        public const string LNotTargetPdfVer = "L005";
        //'【論理エラー】（PDFプロテクト）

        public const string LProtectedPdf = "L006";
        //'【論理エラー】（PDF変換失敗）

        public const string LComvertErr = "L007";
        //'【論理エラー】（画像抽出不可能）

        public const string LImgPickErr = "L008";
        //'【論理エラー】（フォント破損）

        public const string LFontCrushedErr = "L009";
        //'【論理エラー】（中身なしExcel）

        public const string LBlankExcelErr = "L010";
        //'【論理エラー】（ページ設定なしExcel）

        public const string LPageSettingLessExcelErr = "L011";
        //'【論理エラー】（A3サイズ超Wordファイル）

        public const string LSizeOverWordErr = "L012";
        //'【論理エラー】（不定形Wordファイル）

        public const string LUndefinedWordErr = "L013";
        //'【論理エラー】（余白サイズが小さいWordファイル）

        public const string LBlankSmallWordErr = "L014";
        //'【論理エラー】（プロパティが存在しないPDF）

        public const string LPropertyNonePdfErr = "L015";
        //'【論理エラー】（白黒ファイルサイズエラー）

        public const string LMonoSizeErr = "L016";
        //'【論理エラー】（白黒ファイルページ数エラー）

        public const string LMonoPageErr = "L017";
        //'【論理エラー】(チェックエラー)
        //'Update時などに処理対象データがない
        //Public Const LDataNotFound As String = "L001"

        //'Insert時などにすでにキーがある
        //Public Const LDuplicateKey As String = "L002"

        //'クライアント設定エラー：DBでは数値なのにNumericでない引数がきた。日付なのに…NotNullなのに…（略
        //Public Const LClientError As String = "L003"

        //'リスト数が上限数以上
        //Public Const LListOverflow As String = "L004"

        //'無効データ
        //Public Const LInvalidData As String = "L005"

        //'期限切れ
        //Public Const LDataExpired As String = "L006"

        // ''' <summary>
        // ''' エラーコード
        // ''' </summary>
        // ''' <remarks></remarks>
        //Public Enum ErrorCode
        //    NoError                  'エラーなし
        //    ErrMsgInputDuplicateKey  'キー登録済
        //End Enum

    }
    #endregion

}