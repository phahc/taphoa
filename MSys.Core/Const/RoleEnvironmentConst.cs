namespace MSys.Core.Const
{
    public class RoleEnvironmentConst
    {
        public const string httpConnectionLimit = "20";
        public const string EngineCallIntervalKmhn = "-5";
        public const string EngineCallIntervalPrnt = "-15";
        public const string MaintenanceFlg = "0";
        public const string NotInStockMatteFlg = "1";
        public const string UserIdp = "1";
      

        public static string GetConfigurationSettingValue(string value)
        {
            return typeof(RoleEnvironmentConst).GetField(value).GetValue(null).ToString();
        }
    }
}
