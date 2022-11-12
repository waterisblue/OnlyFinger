<template>
  <div class="page">
    <div class="header">
      <el-input v-model="searchName"></el-input>
      <el-button class="searchbtn" type="primary" @click="searchPeopleByStr()"
        >查询</el-button
      >
    </div>
    <div class="body">
      <el-table
        :data="userData"
        stripe
        style="width: 100%"
        v-loading="isLoadingUserTable"
      >
        <el-table-column prop="id" label="id" width="180"> </el-table-column>
        <el-table-column prop="username" label="姓名" width="180">
        </el-table-column>
        <el-table-column
          prop="gender"
          :formatter="this.$transform.transformGender"
          label="性别"
        >
        </el-table-column>
        <el-table-column
          prop="createdate"
          :formatter="this.$transform.transformDate"
          label="加入日期"
        >
        </el-table-column>
        <el-table-column prop="desc" label="描述"> </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button
              size="mini"
              type="danger"
              @click="peopleEdit(scope.$index, scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <el-pagination background layout="prev, pager, next" :total="totalPage">
      </el-pagination>
    </div>
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
  }
}
</style>

<script>
export default {
  name: "PeopleFind",
  mounted() {
    this.getUserByPage(1, 10).then((res) => {
      this.userData = res.data;
      this.isLoadingUserTable = false;
    });
    this.getUserCount().then((res) => {
      this.totalPage = res.data;
    });
  },
  data() {
    return {
      searchName: "",
      userData: [],
      totalPage: 1000,
      searchData: [],
      pageStatus: "all",
      userProfileEdit: false,
      isLoadingUserTable: true,
    };
  },
  methods: {
    async getUserByPage(start, end) {
      let { data: userList } = await this.$http.post(
        "/user/User/getUserByPage",
        null,
        { params: { start, end } }
      );
      return userList;
    },

    async getUserCount() {
      let { data: userCount } = await this.$http.get("/user/User/getUserCount");
      return userCount;
    },
    // 页码改变调用
    groupSizeChange(currentPage) {
      this.isLoadingGroup = true;

      if (this.pageStatus === "all") {
        this.getUserByPage(currentPage, 10).then((res) => {
          this.userData = res.data;
          this.isLoadingGroup = false;
        });
      } else if (this.pageStatus === "search") {
        currentPage = currentPage - 1;
        this.userData = this.searchData.slice(
          currentPage * 10,
          currentPage * 10 + 10
        );
        this.isLoadingGroup = false;
      }
    },
    // 用户查询
    async searchPeopleByStr() {
      if (this.searchName === "") {
        this.$notify({
          title: "错误",
          message: "查询不可为空",
          type: "error",
        });
        this.isLoadingGroup = false;
        return;
      }
      let { data: userList } = await this.$http.post(
        "/user/User/searchUser",
        null,
        { params: { searchStr: this.searchName } }
      );
      if (this.isLoadingGroup) {
        return;
      }
      this.isLoadingGroup = true;

      this.searchData = userList.data;
      this.userData = userList.data.slice(0, 10);
      this.totalPage = userList.data.length;
      this.pageStatus = "search";

      this.isLoadingGroup = false;
    },
    // TODO：用户删除
    async peopleEdit(index, row) {
      const isConfirm = await this.$confirm("是否删除“" + row.username + "”？", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })

      if(isConfirm != "confirm") return;
      let {data:res} = await this.$http.post("/user/User/deleteUserById", null, {params: {userId: row.id}})
      let type = "error"
      if (res.code == 200) {
        type = "success";
        this.userData.splice(index, 1)
      }
      this.$message({
        type: type,
        message: res.message,
      });
    },
  },
};
</script>