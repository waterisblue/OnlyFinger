<template>
  <div class="page">
    <div class="upperhalf">
      <div class="gaugechart" ref="gaugechart"></div>
      <div class="handlemenu">

        <div class="workselect numtext">
          当前任务：
          <el-select v-model="value" placeholder="请选择">
            <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            >
            </el-option>
          </el-select>
        </div>
        <div class="numtext">当前已参与人数：56</div>
        <div class="numtext">当前未参与人数：56</div>
        <div class="numtext">请假人数：56</div>
      </div>
      <div class="refreshbtn">
          <el-button icon="el-icon-refresh" circle class="indexrefresh"></el-button>
      </div>
    </div>
    <div class="lowerhalf">
      <div class="realtimetable">
        <el-table :data="tableData">
          <el-table-column prop="date" label="日期" width="180">
          </el-table-column>
          <el-table-column prop="name" label="姓名" width="180">
          </el-table-column>
          <el-table-column prop="address" label="地址"> </el-table-column>
        </el-table>
      </div>
      <div class="realtimechart">
        <div class="piechart" ref="piechart"></div>
      </div>
    </div>
  </div>
</template>

<style lang="less" scoped>
.page {
  display: flex;
  flex-direction: column;
  height: 100%;
  .upperhalf {
    flex: 4;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    height: 100%;
    align-content: center;
    .gaugechart {
      flex: 1;
    }
    .handlemenu {
      flex: 1;
      padding: 0;

      display: flex;
      flex-direction: column;
      justify-content: space-around;
      text-align: start;
      .numtext {
        font-size: 1.2rem;
      }
    }
    
    @keyframes refreshbtnrotate {
      from {
        transform: rotate(0);
      }
      to {
        transform: rotate(360deg);
      }
    }
    .indexrefresh:hover {
      animation: refreshbtnrotate 1s linear infinite;
    }
  }
  .lowerhalf {
    flex: 5;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    .realtimetable {
      width: 50rem;
      box-sizing: border-box;
    }
    .realtimechart {
      flex: 1;
      display: flex;
      flex-direction: column;
      .piechart {
        flex: 1;
      }
    }
  }
}
</style>

<script>
export default {
  name: "DashBroad",
  mounted() {
    this.getGaugeChart();
    this.getPieChart();
  },
  data() {
    return {
      dialogVisible: false,
      tableData: [
        {
          date: "2016-05-02",
          name: "王小虎",
          address: "上海市普陀区金沙江路 1518 弄",
        },
        {
          date: "2016-05-04",
          name: "王小虎",
          address: "上海市普陀区金沙江路 1517 弄",
        },
        {
          date: "2016-05-04",
          name: "王小虎",
          address: "上海市普陀区金沙江路 1517 弄",
        },
        {
          date: "2016-05-04",
          name: "王小虎",
          address: "上海市普陀区金沙江路 1517 弄",
        },
        {
          date: "2016-05-03",
          name: "王小虎",
          address: "上海市普陀区金沙江路 1516 弄",
        },
      ],
      options: [{
          value: '选项1',
          label: '黄金糕'
        }, {
          value: '选项2',
          label: '双皮奶'
        }, {
          value: '选项3',
          label: '蚵仔煎'
        }, {
          value: '选项4',
          label: '龙须面'
        }, {
          value: '选项5',
          label: '北京烤鸭'
        }],
    };
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
        myPieChart.setAttribute("_echarts_instance_", "");
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
                  value: 20,
                  name: "",
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
