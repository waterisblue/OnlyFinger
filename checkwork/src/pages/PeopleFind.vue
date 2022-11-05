<template>
  <div class="page">
    <div class="header">
      <el-input v-model="searchname"></el-input>
      <el-button class="searchbtn" type="primary">查询</el-button>
    </div>
    <div class="body">
      <el-table :data="userData" stripe style="width: 100%" v-loading="isLoadingUserTable">
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
      <el-pagination background layout="prev, pager, next" :total="totalPage">
      </el-pagination>
    </div>
    <!-- 增加框 -->
    <el-dialog title="用户编辑" :visible.sync="userProfileEdit">
      
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
      searchname: "",
      userData: [],
      totalPage: 1000,
      searchData: [],
      pageStatus: "all",
      userProfileEdit: false,
      isLoadingUserTable: true
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
};
</script>