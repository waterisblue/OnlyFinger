using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;

namespace OnlyFingerWeb.Service.DataAnalyService
{
    public interface IDataAnalyService
    {
        public ReturnCode<List<TaskEntity>> getCurrentTimeTask();
        public ReturnCode<Dictionary<string, int>> getGaugeData(int taskId);
        public ReturnCode<List<UserSign>> getSignUser(int taskId);
    }
}
