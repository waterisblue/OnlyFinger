<template>
  <div class="page">
    <div class="header">
      <div class="header">
        <div class="selectbutton">
          <router-link to="/index" class="waterbutton">仪表盘</router-link>
          <router-link to="/anyonefind" class="waterbutton">员工细查</router-link>
          <router-link to="/findbydate" class="waterbutton">日期详查</router-link>
        </div>
        <div class="title">3组考勤系统</div>
      </div>
    </div>
    <router-view />
  </div>
</template>

<script>
export default {
  name: "Index",
};
</script>

<style lang="less" scoped>
.page {
  display: flex;
  flex-direction: column;
  height: 100%;
  .header {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    width: 100%;
    flex: 1;
    box-sizing: border-box;
    background: linear-gradient(rgb(191, 170, 170), rgb(255, 255, 255));
    .selectbutton {
      margin-top: 1rem;
      flex: 5;
    }
    .title {
      color: rgb(149, 50, 80);
      margin-top: 1rem;
      font-size: 1.35rem;
      flex: 1;
    }
  }
}
</style>