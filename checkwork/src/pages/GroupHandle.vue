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
        <el-table-column prop="desc" label="组描述"> </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button size="mini" @click="handleEdit(scope.$index, scope.row)"
              >编辑</el-button
            >
            <el-button
              size="mini"
              type="danger"
              @click="handleDelete(scope.$index, scope.row)"
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
      addGroupData: {
        groupName: "",
        desc: "",
      },
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

    // 修改dialog状态
    changeAddGroupDialog(show) {
      this.addGroupFormVisible = show;
    },

    // 根据字符串查询组   DA
    async searchGroup(searchStr) {
      if (searchStr === "") {
        this.$notify({
          title: "错误",
          message: "查询不可为空",
          type: "error",
        });
        this.isLoadingGroup = false;
        return;
      }

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
      this.create();
    },
    // 点击查询
    toSearch() {
      if (this.isLoadingGroup) {
        return;
      }
      this.isLoadingGroup = true;
      this.searchGroup(this.searchStr).then((res) => {
        this.groupData = res.data;
        this.isLoadingGroup = false;
      });
    },
    groupSizeChange(currentPage) {
      this.isLoadingGroup = true;

      this.getGroupListByPage(currentPage, 10).then((res) => {
        this.groupData = res.data;
        this.isLoadingGroup = false;
      });
    },
  },
};
</script>