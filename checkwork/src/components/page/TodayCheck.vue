<template>
  <div class="page">
    <div class="dashboard">
      <div class="gaugechart" ref="gaugechart"></div>
      <div class="count">
        <div class="innertitle">今日到达人数</div>
        <div class="innertitle">签到：32</div>
        <div class="innertitle">迟到：26</div>
      </div>
    </div>
    <div class="charts">
      <div class="todaytable">
        <el-table :data="tableData" style="width: 100%" border=true>
          <el-table-column
            prop="time"
            label="时间"
            width="180"
          ></el-table-column>
          <el-table-column
            prop="name"
            label="姓名"
            width="180"
          ></el-table-column>
          <el-table-column prop="sign" label="签到"> </el-table-column>
          <el-table-column prop="sign" label="其他标签1"> </el-table-column>
          <el-table-column prop="sign" label="其他标签2"> </el-table-column>
          <el-table-column prop="sign" label="其他标签3"> </el-table-column>
        </el-table>
      </div>
      <div class="piechart" ref="piechart"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TodayCheck",
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
  mounted() {
    this.getPieChart();
    this.getGaugeChart();
  },
  methods: {
    //#region 获取饼图
    getPieChart() {
      const piechart = this.$refs.piechart;
      if (piechart) {
        const myPieChart = this.$echarts.init(piechart);
        const option = {
          title: {
            text: "今日XX人数",
          },
          tooltip: {
            trigger: "item",
          },
          series: [
            {
              name: "Access From",
              type: "pie",
              radius: ["40%", "70%"],
              avoidLabelOverlap: false,
              itemStyle: {
                borderRadius: 10,
                borderColor: "#fff",
                borderWidth: 2,
              },
              label: {
                show: false,
                position: "center",
              },
              emphasis: {
                label: {
                  show: true,
                  fontSize: "40",
                  fontWeight: "bold",
                },
              },
              labelLine: {
                show: false,
              },
              data: [
                { value: 1048, name: "签到" },
                { value: 735, name: "签退" },
                { value: 580, name: "迟到" },
                { value: 484, name: "早退" },
                { value: 300, name: "缺勤" },
              ],
            },
          ],
        };
        myPieChart.setOption(option);
        window.addEventListener("resize", () => {
          myPieChart.resize();
        });
        this.$on("hook:destroyed", () => {
          window.removeEventListener("resize", function () {
            myPieChart.resize();
          });
        });
        piechart.removeAttribute("_echarts_instance_");
      }
    },
    //#endregion
    //#region 获取仪表盘
    getGaugeChart() {
      const gaugechart = this.$refs.gaugechart;
      if (gaugechart) {
        const myGaugeChart = this.$echarts.init(gaugechart);
        const option = {
          tooltip: {
            formatter: "{a} <br/>{b} : {c}%",
          },
          series: [
            {
              name: "Pressure",
              type: "gauge",
              progress: {
                show: true,
              },
              detail: {
                valueAnimation: true,
                formatter: "{value}",
              },
              data: [
                {
                  value: 50,
                  name: "员工到达比例",
                },
              ],
            },
          ],
        };
        myGaugeChart.setOption(option);
        window.addEventListener("resize", () => {
          myGaugeChart.resize();
        });
        this.$on("hook:destroyed", () => {
          window.removeEventListener("resize", function () {
            myGaugeChart.resize();
          });
        });
      }
      gaugechart.setAttribute("_echarts_instance_", "");
    },
    //#endregion
  },
};
</script>

<style lang="less" scoped>
.page {
  display: flex;
  flex-direction: column;
  .dashboard {
    width: 100%;
    flex: 5;
    display: flex;
    flex-direction: column;
    .gaugechart {
      flex: 3;
    }
    .count {
      flex: 1;
      display: flex;
      flex-direction: row;
      justify-content: center;
      .innertitle {
        margin: 0rem 1rem;
        font-size: 1.5rem;
      }
    }
  }
  .charts {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    width: 100%;
    flex: 3;
    .piechart {
      flex: 1;
    }
    .todaytable {
      width: 60rem;
      margin-left: 2rem;
      box-sizing: border-box;
    }
  }
}
</style>