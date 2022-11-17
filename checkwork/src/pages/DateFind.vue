<template>
  <div class="page">
    <div class="header">
      <div class="datefindinput">
        <el-date-picker
          v-model="checktimes"
          type="datetimerange"
          align="right"
          unlink-panels
          range-separator="-"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          :picker-options="pickerOptions"
          :blur="timeChange"
        >
        </el-date-picker>
        <el-button type="primary" plain round style="margin-left: 3rem" @click="init(checktimes)">查询</el-button>
      </div>
    </div>
    <div class="body">
      <div class="upper">
        <el-card class="upcard">
          <div class="numtext">时长共计：{{timeCount}}</div>
          <div class="numtext">签到人数：{{signCount}}</div>
          <div class="numtext">总人数：{{allCount}}</div>
        </el-card>
        <el-card class="upcard">
          <div class="numtext">
           涉及到的任务： {{taskStr}}
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
    const end = new Date();
    const start = new Date();
    start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
    this.init([start, end]);
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
      checktimes: "",
      allCount: 0,
      signCount: 0,
      taskStr: "",
      timeCount: ""
    };
  },
  methods: {
    async init(checkTimes){
      const startTime = checkTimes[0].getTime()
      const endTime = checkTimes[1].getTime()
      const {data:res} = await this.$http.post("/dataanay/DataAnaly/getTimeAnaly", null, {params: {starttime: startTime, endtime: endTime}})
      this.allCount = res.data.allCount
      this.signCount = res.data.signCount
      this.setDataAndTask(res.data.pie2data, endTime - startTime)
      this.getPieChart([this.allCount, this.signCount])
      this.getBarChart(JSON.parse(res.data.piedata))
      this.getPieChart2(JSON.parse(res.data.pie2data))
    },
    setDataAndTask(pie2data, timeNum){
      pie2data = JSON.parse(pie2data)
      let taskStr = ""
      for(let i = 0; i < pie2data.length; i++){
        taskStr += pie2data[i].taskname + ", "
      }
      taskStr = taskStr.substring(0, taskStr.length - 2)
      
      this.taskStr = taskStr

      let hour = parseInt(timeNum / 1000 / 60 / 60)
      let minute = parseInt(timeNum / 1000 / 60 % 60)
      let second = parseInt(timeNum / 1000 % 60 % 60)
      this.timeCount = hour + "h " + minute + "m " + second + "s"
    },
    // 获取饼图
    getPieChart(resData) {
      let data = [
        { value: resData[1], name: "签到" },
        { value: resData[0] - resData[1], name: "未签" },
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
              data: data,
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
      let nameArr = []
      let numArr = []
      for(let i = 0; i < resData.length; i++){
        nameArr.push(resData[i].username)
        numArr.push(resData[i].sum)
      }
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
              data: nameArr,
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
              data: numArr,
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
      let data = []
      for(let i = 0; i < resData.length; i++){
        data.push({ value: resData[i].sum, name: resData[i].taskname })
      }

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
  },
};
</script>