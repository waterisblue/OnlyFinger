<template>
  <div class="page">
    <div class="search">
      <div class="searchinput">
        <el-input v-model="input" placeholder="请输入内容"></el-input>
      </div>
      <div class="searchbutton">
        <el-button type="primary">搜索员工</el-button>
      </div>
    </div>
    <div class="list">
      <el-table
        :data="tableData"
        style="width: 100%"
        :default-sort="{ prop: 'date', order: 'descending' }"
        @row-click="handle"
      >
        <el-table-column prop="id" label="id" sortable width="180"></el-table-column>
        <el-table-column prop="time" label="time" sortable width="180"></el-table-column>
        <el-table-column prop="name" label="name" :formatter="formatter"></el-table-column>
        <el-table-column prop="sign" label="sign" :formatter="formatter"></el-table-column>
        <el-table-column prop="sign" label="其他1" :formatter="formatter"></el-table-column>
        <el-table-column prop="sign" label="其他1" :formatter="formatter"></el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
export default {
  name: "AnyOneFind",
  mounted(){
    this.getData()
  },
  data: () => {
    return {
      tableData: [
        {
          time: "7:59:36",
          name: "王小虎",
          sign: "是",
        },
        {
          time: "7:39:26",
          name: "王二虎",
          sign: "否",
        },
        {
          time: "7:57:36",
          name: "王三虎",
          sign: "是",
        },
        {
          time: "7:19:16",
          name: "王四虎",
          sign: "是",
        },
      ],
    };
  },
  methods: {
      // getData(){
      //   this.$http.get('/db/DBTest/getAllData').then(res => {
      //     this.tableData = res.data
      //   })
      // },
      handle() {
        this.$router.push("/anyonechart")
      }
  },
};
</script>

<style lang="less" scoped>
.page {
  display: flex;
  flex-direction: column;
  .search {
    flex: 1;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    margin: 2rem 2rem 0rem 2rem;
    .searchinput {
      flex: 5;
      box-sizing: border-box;
      margin-left: 2rem;
    }
    .searchbutton {
      flex: 1;
      margin-left: 2rem;
    }
  }
  .list {
    flex: 8;
    margin-left: 4rem;
  }
}
</style>
