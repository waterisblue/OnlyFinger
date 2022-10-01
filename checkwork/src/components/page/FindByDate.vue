<template>
  <div class="page">
    <div class="block dateselect">
      <span>根据日期查询：</span>
      <el-date-picker
        v-model="value2"
        type="datetimerange"
        :picker-options="pickerOptions"
        range-separator="至"
        start-placeholder="开始日期"
        end-placeholder="结束日期"
        align="right"
      >
      </el-date-picker>
      <el-button type="primary" class="tofindbutton">查询</el-button>
    </div>
    <div class="findcharts">
        主题内容
    </div>
  </div>
</template>

<script>
export default {
  name: "FindByDate",
  data: () => {
    return {
      pickerOptions: {
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            },
          },
        ],
      },
      value1: [new Date(2000, 10, 10, 10, 10), new Date(2000, 10, 11, 10, 10)],
      value2: "",
    };
  },
};
</script>

<style lang="less" scoped>
.page {
  display: flex;
  flex-direction: column;
  .dateselect {
    flex: 1;
    text-align: center;
    margin-top: 2rem;
    span {
      font-size: 1.3rem;
    }
    .tofindbutton {
        margin-left: 2rem;
    }
  }
  .findcharts {
    display: flex;
    flex-direction: column;
    flex: 5;
  }
}
</style>
