<template>
  <div class="page">
    <div class="upperhalf">
      <div class="gaugechart" ref="gaugechart"></div>
      <el-card class="handlemenu">
        <div class="workselect numtext">
          当前任务：
          <el-select
            v-model="checkTask"
            placeholder="请选择"
            @change="taskChange"
          >
            <el-option
              v-for="item in options"
              :key="item.id"
              :label="item.taskName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </div>
        <div class="numtext">当前已参与人数：{{ signCount }}</div>
        <div class="numtext">当前未参与人数：{{ allCount - signCount }}</div>
        <div class="numtext">全部人数：{{ allCount }}</div>
      </el-card>
      <div class="refreshbtn">
        <el-button
          icon="el-icon-refresh"
          circle
          class="indexrefresh"
          @click="taskChange(checkTask)"
        ></el-button>
      </div>
    </div>
    <div class="lowerhalf">
      <div class="realtimetable">
        <el-table :data="tableData">
          <el-table-column prop="signtime" :formatter="this.$transform.transformDate" label="时间" width="180">
          </el-table-column>
          <el-table-column prop="username" label="姓名" width="180">
          </el-table-column>
          <el-table-column prop="taskname" label="任务"> </el-table-column>
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
      line-height: 4rem;
      padding-left: 7rem;
      margin-bottom: 2rem;
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
    this.initDashBoard();
  },
  data() {
    return {
      dialogVisible: false,
      tableData: [],
      options: [],
      checkTask: "",
      allCount: "",
      signCount: "",
      checkTaskName: ""
    };
  },
  methods: {
    async initDashBoard() {
      const { data: res } = await this.$http.get(
        "/dataanay/DataAnaly/getCurrentTimeTask"
      );
      this.options = res.data;
      if (res.data.length != 0) this.checkTask = res.data[0].id;
      this.taskChange(res.data[0].id);
    },
    //#region 获取饼图
    getPieChart(resData) {
      let data = [
        { value: resData.signCount, name: "签到" },
        { value: resData.allCount - resData.signCount, name: "未签" },
      ];
      const piechart = this.$refs.piechart;
      if (piechart) {
        const myPieChart = this.$echarts.init(piechart);
        const option = {
          title: {
            text: "任务打卡比例",
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
              data: data,
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
    //#endregion
    //#region 获取仪表盘
    getGaugeChart(resData) {
      let signValue = 0;
      if (resData.allCount != 0) {
        signValue = parseInt((resData.signCount / resData.allCount) * 100);
      }
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
                  value: signValue,
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
      // gaugechart.setAttribute("_echarts_instance_", "");
    },
    //#endregion
    async taskChange(taskId) {
      const { data: resData } = await this.$http.post(
        "/dataanay/DataAnaly/getGaugeData",
        null,
        { params: { taskId: this.checkTask } }
      );
      this.allCount = resData.data.allCount;
      this.signCount = resData.data.signCount;
      this.getGaugeChart(resData.data);
      this.getPieChart(resData.data);
      const { data: listData } = await this.$http.post(
        "/dataanay/DataAnaly/getSignUser",
        null,
        { params: { taskId: this.checkTask } }
      );
      
      listData.data.map(x => x.taskname = this.options.filter(x => x.id == taskId)[0].taskName)
      this.tableData = listData.data
    },
  },
};
</script>
