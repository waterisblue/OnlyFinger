<!--展示所有的任务 -->
<template>
  <div class="page">
    <el-container>
      <el-header>
        <el-button type="primary" @click="toAddTask">增加活动</el-button>
      </el-header>
      <el-main>
        <el-table
          :data="tableData"
          style="width: 100%"
          :default-sort="{ prop: 'date', order: 'descending' }"
        >
          <el-table-column prop="date" label="日期" sortable width="180">
          </el-table-column>
          <el-table-column prop="name" label="姓名" sortable width="180">
          </el-table-column>
          <el-table-column prop="address" label="地址" :formatter="formatter">
          </el-table-column>
        </el-table>
        <el-dialog title="添加活动" :visible.sync="dialogFormVisible">
          <el-form :model="form">
            <el-form-item label="活动名称" :label-width="formLabelWidth">
              <el-input v-model="form.name" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="开始时间" :label-width="formLabelWidth">
              <el-col :span="11">
                <el-date-picker
                  type="date"
                  placeholder="选择日期"
                  v-model="form.startdate"
                  style="width: 100%"
                ></el-date-picker>
              </el-col>
              <el-col class="line" :span="1">-</el-col>
              <el-col :span="12">
                <el-time-picker
                  placeholder="选择时间"
                  v-model="form.starttime"
                  style="width: 100%"
                ></el-time-picker>
              </el-col>
            </el-form-item>
            <el-form-item label="结束时间" :label-width="formLabelWidth">
              <el-col :span="11">
                <el-date-picker
                  type="date"
                  placeholder="选择日期"
                  v-model="form.enddate"
                  style="width: 100%"
                ></el-date-picker>
              </el-col>
              <el-col class="line" :span="1">-</el-col>
              <el-col :span="12">
                <el-time-picker
                  placeholder="选择时间"
                  v-model="form.endtime"
                  style="width: 100%"
                ></el-time-picker>
              </el-col>
            </el-form-item>
            <el-form-item label="活动组别" :label-width="formLabelWidth">
              <el-select
                v-model="form.group"
                placeholder="参与活动组别"
              >
                <el-option label="第一组" value="one"></el-option>
                <el-option label="第二组" value="two"></el-option>
              </el-select>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">取 消</el-button>
            <el-button type="primary" @click="addTaskPost"
              >确 定</el-button
            >
          </div>
        </el-dialog>
      </el-main>
    </el-container>
  </div>
</template>

<script>
export default {
  name: "TaskView",
  data: () => {
    return {
      tableData: [
        {
          date: "2016-05-02",
          name: "王小虎",
          address: "上海市普陀区金沙江路 1518 弄",
        }
      ],
      dialogTableVisible: false,
      dialogFormVisible: false,
      form: {
        name: "",
        group: "",
        startdate: "",
        starttime: "",
        enddate: "",
        endtime: ""
      },
      formLabelWidth: "120px",
    };
  },
  methods: {
    toAddTask() {
      this.dialogFormVisible = true;
    },
    addTaskPost(){
        
    }
  },
};
</script>

<style lang="less">
</style>

