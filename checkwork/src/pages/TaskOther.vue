<template>
  <div class="page">
    <el-button
      class="top"
      type="primary"
      @click="
        addTaskDialogVisible = true;
        editTaskDialogVisible = false;
        removeTaskDialogVisible = false;
      "
    >
      <i class="el-icon-circle-plus"></i>增加任务
    </el-button>
    <el-button
      class="top"
      type="success"
      @click="
        searchTaskDialogVisible = true
      "
    >
      <i class="el-icon-search"></i>查询任务
    </el-button>
    <div class="bottom">
      <el-button
        class="edittask"
        type="info"
        @click="
          editTaskDialogVisible = true;
          addTaskDialogVisible = false;
          removeTaskDialogVisible = false;
        "
        ><i class="el-icon-edit"></i>编辑任务</el-button
      >
      <el-button
        class="removetask"
        type="danger"
        @click="
          removeTaskDialogVisible = true;
          addTaskDialogVisible = false;
          editTaskDialogVisible = false;
        "
        ><i class="el-icon-circle-close"></i>删除任务</el-button
      >
    </div>
    <el-dialog title="添加" :visible.sync="addTaskDialogVisible" width="50%">
      <el-form ref="form" :model="addTaskData" label-width="80px">
        <el-form-item label="任务名">
          <el-input
            v-model="addTaskData.taskname"
            placeholder="任务名"
          ></el-input>
        </el-form-item>
        <el-form-item label="任务描述">
          <el-input v-model="addTaskData.desc" placeholder="描述"></el-input>
        </el-form-item>
        <el-form-item label="任务时间">
          <el-date-picker
            v-model="addTaskData.time"
            type="datetimerange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
          >
          </el-date-picker>
          <el-button type="primary" class="addbtn" @click="addTaskFun"
            >添加</el-button
          >
        </el-form-item>
      </el-form>
    </el-dialog>
    <el-dialog title="编辑" :visible.sync="editTaskDialogVisible" width="50%">
      <el-form label-width="80px">
        <el-form-item label="任务id：">
          <el-input
            v-model="editTaskId"
            placeholder="请输入欲编辑的任务id"
          ></el-input>
        </el-form-item>
        <el-row type="flex">
          <el-col :span="1" offset="18">
            <el-button type="success" @click="editFindGroup">确定</el-button>
          </el-col>
          <el-col :span="1" offset="2">
            <el-button type="cancel" @click="editTaskDialogVisible = false"
              >取消</el-button
            >
          </el-col>
        </el-row>
      </el-form>
    </el-dialog>
    <el-dialog title="删除" :visible.sync="removeTaskDialogVisible" width="50%">
      <el-form label-width="80px">
        <el-form-item label="任务id：">
          <el-input
            v-model="removeTaskId"
            placeholder="请输入欲删除的任务id"
          ></el-input>
        </el-form-item>
        <el-row type="flex">
          <el-col :span="1" offset="18">
            <el-button type="success" @click="removeTaskFun">确定</el-button>
          </el-col>
          <el-col :span="1" offset="2">
            <el-button type="cancel" @click="removeTaskDialogVisible = false"
              >取消</el-button
            >
          </el-col>
        </el-row>
      </el-form>
    </el-dialog>
    <el-dialog
      title="编辑组别"
      :visible.sync="editTaskDetailVisible"
      width="50%"
    >
      <el-form
        ref="form"
        :model="editTaskData"
        label-width="80px"
        :disabled="editTaskInputDisable"
      >
        <el-form-item label="任务名">
          <el-input
            v-model="editTaskData.taskname"
            placeholder="任务名"
          ></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="editTaskData.desc" placeholder=""></el-input>
        </el-form-item>
        <el-form-item label="任务时间">
          <el-date-picker
            v-model="editTaskData.time"
            type="datetimerange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
          >
          </el-date-picker>
        </el-form-item>
      </el-form>
      <el-row type="flex" justify="right">
        <el-col :span="1" offset="20"
          ><el-button
            type="text"
            v-if="editTaskInputDisable"
            @click="editTaskInputDisable = false"
            >编辑</el-button
          ></el-col
        >
      </el-row>
      <el-row type="flex">
        <el-col :span="1" offset="18">
          <el-button
            type="success"
            v-if="!editTaskInputDisable"
            @click="updateTaskData"
            >确定</el-button
          >
        </el-col>
        <el-col :span="1" offset="2">
          <el-button
            type="cancel"
            v-if="!editTaskInputDisable"
            @click="editTaskInputDisable = true"
            >取消</el-button
          >
        </el-col>
      </el-row>
      <el-divider></el-divider>
      <el-row type="flex">
        <el-col>
          <el-table
            :data="editGroup2TaskData"
            height="200"
            border
            style="width: 100%"
          >
            <el-table-column prop="id" label="id" width="80"> </el-table-column>
            <el-table-column prop="groupName" label="组名" width="180">
            </el-table-column>
            <el-table-column label="操作">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="danger"
                  @click="deleteGroupTask(scope.$index, scope.row)"
                  >删除</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </el-col>
      </el-row>
      <el-row type="flex" style="margin-top: 1rem">
        <el-col offset="20"
          ><el-button type="primary" @click="addTaskGroup"
            >增加组</el-button
          ></el-col
        >
      </el-row>
    </el-dialog>
    <el-dialog title="添加" :visible.sync="searchTaskDialogVisible" width="50%">
      <el-table :data="searchTaskList" style="width: 100%">
        <el-table-column prop="id" label="id" width="180">
        </el-table-column>
        <el-table-column prop="taskName" label="任务名" width="180">
        </el-table-column>
        <el-table-column prop="startTime" label="开始时间" :formatter="this.$transform.transformDate"> </el-table-column>
        <el-table-column prop="endTime" label="结束时间" :formatter="this.$transform.transformDate"> </el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>

<style lang="less">
.page {
  display: flex;
  height: 100%;
  flex-direction: column;
  .top {
    flex: 1;
    border: 0.1rem solid rgb(184, 172, 172);
    border-radius: 1rem;
    margin: 1rem;
    color: white;
    font-size: 3rem;
  }
  .bottom {
    flex: 1;
    display: flex;
    flex-direction: row;
    .edittask {
      flex: 1;
      border: 0.1rem solid rgb(184, 172, 172);
      border-radius: 1rem;
      margin: 1rem;
      color: white;
      font-size: 3rem;
    }
    .removetask {
      flex: 1;
      border: 0.1rem solid rgb(184, 172, 172);
      border-radius: 1rem;
      margin: 1rem;
      color: white;
      font-size: 3rem;
    }
  }
  .addbtn {
    width: 14rem;
    margin-left: 1rem;
  }
}
</style>

<script>
export default {
  name: "TaskOther",
  data() {
    return {
      addTaskData: {
        taskname: "",
        time: [],
        desc: "",
      },
      editTaskData: {
        id: "",
        taskname: "",
        time: [],
        desc: "",
      },
      searchTaskList: [],
      editGroup2TaskData: [],
      addTaskDialogVisible: false,
      editTaskDialogVisible: false,
      removeTaskDialogVisible: false,
      searchTaskDialogVisible: false,
      editTaskDetailVisible: false,
      removeTaskId: "",
      editTaskId: "",
      editTaskInputDisable: true,
    };
  },
  mounted(){
    this.$http.post("/task/Task/getCurrentTask").then(res => {
      this.searchTaskList = res.data.data
      console.log(this.searchTaskList)
    })
  },
  methods: {
    async addTaskFun() {
      let starttime = this.addTaskData.time[0].getTime();
      let endtime = this.addTaskData.time[1].getTime();
      const { data: addRes } = await this.$http.post(
        "/task/Task/addTask",
        null,
        {
          params: {
            taskName: this.addTaskData.taskname,
            startTime: starttime,
            endTime: endtime,
            desc: this.addTaskData.desc,
          },
        }
      );
      if (addRes.code != 200) {
        this.$notify({
          title: "错误",
          message: addRes.message,
          type: "error",
        });
        this.addTaskDialogVisible = false;
        return;
      }
      this.$notify({
        title: "添加成功",
        message: addRes.message,
        type: "success",
      });
      this.editTaskId = addRes.data;
      this.editFindGroup();
      this.addTaskDialogVisible = false;
    },
    async removeTaskFun() {
      let removeId = this.removeTaskId;
      const { data: res } = await this.$http.post(
        "task/Task/deleteTask",
        null,
        { params: { taskId: removeId } }
      );
      if (res.code != 200) {
        this.$notify({
          title: "错误",
          message: res.message,
          type: "error",
        });
        return;
      }
      this.$notify({
        title: "删除成功",
        message: res.message,
        type: "success",
      });
      this.removeTaskDialogVisible = false;
    },
    async editFindGroup() {
      let editId = this.editTaskId;
      const { data: res } = await this.$http.post(
        "/task/Task/findTaskWithGroup",
        null,
        { params: { taskId: editId } }
      );
      if (res.code != 200) {
        this.$notify({
          title: "失败",
          message: res.message,
          type: "error",
        });
        this.editTaskDialogVisible = false;
        return;
      }
      if (Date.now() >= res.data.startTime) {
        this.$notify({
          title: "失败",
          message: "无法编辑已经结束的任务",
          type: "error",
        });
        this.editTaskDialogVisible = false;
        return;
      }
      this.editTaskDialogVisible = false;
      this.editTaskDetailVisible = true;
      this.editTaskData.taskname = res.data.taskName;
      this.editTaskData.desc = res.data.desc;
      this.editTaskData.time = [res.data.startTime, res.data.endTime];
      this.editGroup2TaskData = res.data.groupEntities;
    },
    async addTaskGroup() {
      const isAddPrompt = await this.$prompt("请输入要增加的组id", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
      });
      if (isAddPrompt.action != "confirm") return;
      let addGroupId = isAddPrompt.value;
      const { data: res } = await this.$http.post(
        "task/Task/addGroupToTask",
        null,
        { params: { taskId: this.editTaskId, groupId: addGroupId } }
      );
      if (res.code != 200) {
        this.$message({
          type: "error",
          message: res.message,
        });
        return;
      }
      this.$message({
        type: "success",
        message: res.message,
      });
      this.editTaskDialogVisible = false;
    },
  },
};
</script>