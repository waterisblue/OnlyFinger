<template>
  <div class="page">
    <div class="header">
      <el-button
        class="addgroupbtn"
        type="success"
        @click="changeAddGroupDialog(true)"
        >增加组</el-button
      >
      <el-input v-model="searchStr"></el-input>
      <el-button class="searchbtn" type="primary" @click="toSearch()"
        >查询</el-button
      >
    </div>
    <div class="body">
      <el-table
        :data="groupData"
        stripe
        style="width: 100%"
        v-loading="isLoadingGroup"
        class="groupTable"
      >
        <el-table-column prop="id" label="ID" width="180"> </el-table-column>
        <el-table-column prop="groupName" label="组名" width="180">
        </el-table-column>
        <el-table-column prop="memberCount" label="组人数"> </el-table-column>
        <el-table-column
          prop="desc"
          :formatter="this.$transform.splitDesc"
          label="组描述"
        >
        </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button
              size="mini"
              @click="groupHandleEdit(scope.$index, scope.row)"
              >编辑</el-button
            >
            <el-button
              size="mini"
              type="danger"
              @click="groupHandleDelete(scope.$index, scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        background
        layout="prev, pager, next"
        :total="totalPage"
        @current-change="groupSizeChange"
      >
      </el-pagination>
    </div>
    <!-- 增加框 -->
    <el-dialog title="增加组" :visible.sync="addGroupFormVisible">
      <el-form ref="form" :model="addGroupData" label-width="80px">
        <el-form-item label="组名">
          <el-input
            v-model="addGroupData.groupName"
            placeholder="组名"
          ></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="addGroupData.desc" placeholder="描述"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="addGroup()">立即增加</el-button>
          <el-button @click="changeAddGroupDialog(false)">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
    <!-- 组编辑框 -->
    <el-dialog title="编辑组" :visible.sync="editGroupFromVisible">
      <el-form
        ref="form"
        :model="editGroupData"
        label-width="80px"
        :disabled="editGroupInputDisable"
      >
        <el-form-item label="组名">
          <el-input
            v-model="editGroupData.groupName"
            placeholder="组名"
          ></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="editGroupData.desc" placeholder=""></el-input>
        </el-form-item>
      </el-form>
      <el-row type="flex" justify="right">
        <el-col :span="1" offset="20"
          ><el-button
            type="text"
            v-if="editGroupInputDisable"
            @click="editGroupInputDisable = false"
            >编辑</el-button
          ></el-col
        >
      </el-row>
      <el-row type="flex">
        <el-col :span="1" offset="18">
          <el-button type="success" v-if="!editGroupInputDisable"
            >确定</el-button
          >
        </el-col>
        <el-col :span="1" offset="2">
          <el-button
            type="cancel"
            v-if="!editGroupInputDisable"
            @click="editGroupInputDisable = true"
            >取消</el-button
          >
        </el-col>
      </el-row>
      <el-divider></el-divider>
      <el-row type="flex">
        <el-col>
          <el-table
            :data="editUser2GroupData"
            height="200"
            border
            style="width: 100%"
          >
            <el-table-column prop="userid" label="id" width="80"> </el-table-column>
            <el-table-column prop="username" label="姓名" width="180">
            </el-table-column>
            <el-table-column
              prop="gender"
              label="性别"
              :formatter="this.$transform.transformGender"
              width="100"
            ></el-table-column>
            <el-table-column prop="createtime" label="加入时间" :formatter="this.$transform.transformDate"></el-table-column>
            <el-table-column label="操作">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="danger"
                  @click="groupHandleEdit(scope.$index, scope.row)"
                  >删除</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </el-col>
      </el-row>
      <el-row type="flex" style="margin-top: 1rem">
        <el-col offset="20"
          ><el-button type="primary" @click="addGroupPeople"
            >增加组员</el-button
          ></el-col
        >
      </el-row>
    </el-dialog>
  </div>
</template>


<style lang="less">
.page {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  .header {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    .addgroupbtn {
      margin-right: 1rem;
    }
    .searchbtn {
      width: 15rem;
      margin-left: 1rem;
    }
  }
  .body {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
    height: 100%;
    .el-loading-spinner {
      left: 38rem;
    }
  }
  .dialogGroupPeopleTitle {
    font-size: 1.3rem;
  }
}
</style>

<script>
export default {
  name: "GroupHandle",
  mounted() {
    this.getGroupListByPage(1, 10).then((res) => {
      this.groupData = res.data;
      this.isLoadingGroup = false;
    });
    this.getGroupCount().then((res) => {
      this.totalPage = res.data;
    });
  },
  data() {
    return {
      searchStr: "",
      groupData: [],
      isLoadingGroup: true,
      totalPage: 1000,
      addGroupFormVisible: false,
      editGroupFromVisible: false,
      addGroupData: {
        groupName: "",
        desc: "",
      },
      pageStatus: "all",
      searchData: [],
      editGroupData: {},
      editGroupInputDisable: true,
      editUser2GroupData: [],
    };
  },
  methods: {
    // 根据页码获取组
    async getGroupListByPage(start, limit) {
      let { data: groupList } = await this.$http.post(
        "/group/Group/getAllGroupByPage",
        null,
        { params: { start, limit } }
      );
      return groupList;
    },

    // 获取组数量
    async getGroupCount() {
      let { data: groupCount } = await this.$http.get(
        "/group/Group/getGroupCount"
      );
      return groupCount;
    },

    // 修改增加dialog状态
    changeAddGroupDialog(show) {
      this.addGroupFormVisible = show;
    },

    // 根据字符串查询组   DA
    async searchGroup(searchStr) {
      let { data: groupList } = await this.$http.post(
        "/group/Group/searchGroup",
        null,
        { params: { searchStr: searchStr } }
      );
      return groupList;
    },

    // 增加一个组
    async addGroup() {
      let { data: res } = await this.$http.post("/group/Group/addGroup", null, {
        params: {
          groupName: this.addGroupData.groupName,
          desc: this.addGroupData.desc,
        },
      });
      if (res.code != 200) {
        this.$notify({
          title: "错误",
          message: res.message,
          type: "error",
        });
      }
      this.$notify({
        title: "添加成功",
        message: res.message,
        type: "success",
      });
      this.addGroupData.groupName = "";
      this.addGroupData.desc = "";
      this.addGroupFormVisible = false;
      this.getGroupCount().then((res) => {
        this.totalPage = res.data;
      });
      this.create();
    },
    // 删除一个组
    async deleteGroup(groupId) {
      const { data: deleteData } = await this.$http.post(
        "/group/Group/deleteGroup",
        null,
        { params: { groupId } }
      );
      return deleteData;
    },
    // 点击查询
    toSearch() {
      if (this.isLoadingGroup) {
        return;
      }
      if (this.searchStr === "") {
        this.$notify({
          title: "错误",
          message: "查询不可为空",
          type: "error",
        });
        this.isLoadingGroup = false;
        return;
      }
      this.isLoadingGroup = true;
      this.searchGroup(this.searchStr).then((res) => {
        this.searchData = res.data;
        this.groupData = res.data.slice(0, 10);
        this.isLoadingGroup = false;
        this.totalPage = res.data.length;
        this.pageStatus = "search";
      });
    },
    // 页码改变调用
    groupSizeChange(currentPage) {
      this.isLoadingGroup = true;

      if (this.pageStatus === "all") {
        this.getGroupListByPage(currentPage, 10).then((res) => {
          this.groupData = res.data;
          this.isLoadingGroup = false;
        });
      } else if (this.pageStatus === "search") {
        currentPage = currentPage - 1;
        this.groupData = this.searchData.slice(
          currentPage * 10,
          currentPage * 10 + 10
        );
        this.isLoadingGroup = false;
      }
    },
    groupHandleEdit(index, row) {
      this.editGroupData = row;
      this.handleIndex = index;
      this.$http
        .post("/group/Group/getUserByGroupId", null, {
          params: { groupId: row.id },
        })
        .then((res) => {
          const resData = res.data;
          if (resData.code != 200) {
            this.$message({
              type: "error",
              message: res.message,
            });
            return
          }
          this.editUser2GroupData = resData.data
        });
      this.editGroupFromVisible = true;
    },
    // 点击删除按钮后执行操作
    groupHandleDelete(index, row) {
      this.$confirm("是否删除“" + row.groupName + "”？", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        this.deleteGroup(row.id).then((res) => {
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
          this.groupData.splice(index, 1);
        });
      });
    },
    // 增加组员
    addGroupPeople() {
      this.$prompt("请输入要增加的组员id", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
      }).then(({ value }) => {
        this.$http
          .post("/user/User/getUserById", null, { params: { userId: value } })
          .then((res) => {
            if (res.data.code != 200) {
              this.$message({
                type: "error",
                message: res.data.message,
              });
            }
            const userProfile = res.data.data;
            this.$confirm(
              "是否添加此用户：\nid：" +
                userProfile.id +
                "\n用户名：" +
                userProfile.username,
              "添加用户",
              {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
              }
            ).then(() => {
              // 添加用户
              const userId = userProfile.id;
              const groupId = this.editGroupData.id;
              this.$http
                .post("/group/Group/addUser2Group", null, {
                  params: { userId, groupId },
                })
                .then((res) => {
                  const resData = res.data;
                  if (resData.code != 200) {
                    this.$message({
                      type: "error",
                      message: res.data.message,
                    });
                    return;
                  }
                  this.$message({
                    type: "success",
                    message: res.data.message,
                  });
                });
            });
          });
      });
    },
  },
};
</script>