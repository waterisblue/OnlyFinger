<template>
  <el-container>
    <el-header>
        <el-row>
            <el-col :span="2">标题</el-col>
            <el-col :span="2" offset="9" class="title">标题</el-col>
        </el-row>
    </el-header>
    <el-container>
      <el-aside width="200px">
        <el-menu
          :default-openeds="['1', '2', '3']"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#ffd04b"
          default-active="1-1"
        >
          <el-submenu index="1">
            <template slot="title"
              ><i class="el-icon-message"></i>任务可视化</template>
            <el-menu-item-group>
              <el-menu-item index="1-1">仪表盘</el-menu-item>
              <el-menu-item index="1-2">日期查询</el-menu-item>
              <el-menu-item index="1-3">人员细查</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
          <el-submenu index="2">
            <template slot="title"><i class="el-icon-menu"></i>任务自定义</template>
            <el-menu-item-group>
              <el-menu-item index="2-1">查询</el-menu-item>
              <el-menu-item index="2-2">其他</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
          <el-submenu index="3">
            <template slot="title"
              ><i class="el-icon-setting"></i>人员管理</template
            >
            <el-menu-item-group>
              <el-menu-item index="3-1">人员查询</el-menu-item>
              <el-menu-item index="3-2">分组管理</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>


<script>
export default {
  name: "Index",
  methods: {
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
  },
};
</script>

<style lang="less" scoped>
.el-header {
  background: #545c64;
  color: rgb(227, 215, 215);
  text-align: center;
  line-height: 60px;
  .title {
    font-size: 1.4rem;
    color: #fefeff;
  }
}
.el-aside {
  background-color: #d3dce6;
  color: #333;
  text-align: center;
}
.el-main {
  text-align: center;
}
</style>