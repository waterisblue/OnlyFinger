<template>
  <div class="page">
    <div class="header">
      <div class="datefindinput">
        <el-date-picker
          v-model="value2"
          type="daterange"
          align="right"
          unlink-panels
          range-separator="-"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          :picker-options="pickerOptions"
        >
        </el-date-picker>
      </div>
    </div>
    <div class="body">
      <div class="upper">
        <el-card class="upcard">
          <div class="numtext">时长共计：20h 13m 1s</div>
          <div class="numtext">签到人数：30</div>
          <div class="numtext">总人数：50</div>
        </el-card>
        <el-card class="upcard">
          <div class="numtext">
            涉及任务：测试任务，核酸任务，6.1核酸任务，考试签到，6.1计组考试
          </div>
        </el-card>
      </div>
      <div class="chart">
        <div class="chart piechart" ref="piechart"></div>
        <div class="chart barchart" ref="barchart"></div>
        <div class="chart piechart2" ref="piechart2"></div>
      </div>
    </div>
  </div>
</template>

<style lang="less" scoped>
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
  .header {
    flex: 1;
  }
  .body {
    width: 100%;
    flex: 10;
    display: flex;
    flex-direction: column;

    .upper {
      flex: 2;
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-around;
      line-height: 4rem;
      .upcard {
        flex: 1;
      }
      .numtext {
        flex: 1;
        font-size: 1.2rem;
      }
    }
    .chart {
      flex: 3;
      display: flex;
      flex-direction: row;
      width: 100%;
      .chart {
        flex: 1;
      }
    }
  }
}
</style>

<script>
export default {
  name: "DateFind",
  mounted() {
    this.getPieChart(1);
    this.getBarChart(1);
    this.getPieChart2(1);
  },
  data() {
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
      value1: "",
      value2: "",
    };
  },
  methods: {
    // 获取饼图
    getPieChart(resData) {
      let data = [
        { value: resData.signCount, name: "签到" },
        { value: resData.allCount, name: "未签" },
      ];
      const piechart = this.$refs.piechart;
      if (piechart) {
        const myPieChart = this.$echarts.init(piechart);
        const option = {
          tooltip: {
            trigger: "item",
          },
          series: [
            {
              name: "Access From",
              type: "pie",
              radius: "50%",
              data: [
                { value: 30, name: "签到人数" },
                { value: 20, name: "未签人数" },
              ],
              emphasis: {
                itemStyle: {
                  shadowBlur: 10,
                  shadowOffsetX: 0,
                  shadowColor: "rgba(0, 0, 0, 0.5)",
                },
              },
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
        // myPieChart.setAttribute("_echarts_instance_", "");
      }
    },
    // 获取柱状图
    getBarChart(resData) {
      const barchart = this.$refs.barchart;
      if (barchart) {
        const myBarChart = this.$echarts.init(barchart);
        const option = {
          tooltip: {
            trigger: "axis",
            axisPointer: {
              type: "shadow",
            },
          },
          grid: {
            left: "3%",
            right: "4%",
            bottom: "3%",
            containLabel: true,
          },
          xAxis: [
            {
              type: "category",
              data: ["高晨康", "郭晨", "张佳宁", "朱孟雨"],
              axisTick: {
                alignWithLabel: true,
              },
            },
          ],
          yAxis: [
            {
              type: "value",
            },
          ],
          series: [
            {
              name: "Direct",
              type: "bar",
              barWidth: "60%",
              data: [50, 30, 20, 14],
            },
          ],
        };
        myBarChart.setOption(option);
        window.addEventListener("resize", () => {
          myBarChart.resize();
        });
        this.$on("hook:destroyed", () => {
          window.removeEventListener("resize", function () {
            myBarChart.resize();
          });
        });
        // myBarChart.setAttribute("_echarts_instance_", "");
      }
    },
    // 获取饼图2
    getPieChart2(resData) {
      const piechart2 = this.$refs.piechart2;
      if (piechart2) {
        const myPieChart = this.$echarts.init(piechart2);
        const option = {
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
                { value: 1048, name: "Search Engine" },
                { value: 735, name: "Direct" },
                { value: 580, name: "Email" },
                { value: 484, name: "Union Ads" },
                { value: 300, name: "Video Ads" },
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
        // myPieChart.setAttribute("_echarts_instance_", "");
      }
    },
  },
};
</script>