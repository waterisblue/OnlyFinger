using OnlyFingerWeb.Entity;

namespace OnlyFingerWeb.Service.TaskService
{
    public interface ITaskService
    {
        ReturnCode<string> addTask(TaskEntity task);
    }
}
