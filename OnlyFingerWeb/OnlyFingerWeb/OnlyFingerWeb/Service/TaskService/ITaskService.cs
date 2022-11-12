using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;

namespace OnlyFingerWeb.Service.TaskService
{
    public interface ITaskService
    {
        ReturnCode<string> addTask(TaskEntity task);
        ReturnCode<TaskWithGroup> findTaskById(int taskId);
        ReturnCode<string> deleteTask(int taskId);
        ReturnCode<string> updateTask(int taskId, string taskName, string desc);
        ReturnCode<int> addGroupToTask(int groupId, int taskId);
        ReturnCode<List<TaskEntity>> getTasksBySearch(string searchStr);
    }
}
