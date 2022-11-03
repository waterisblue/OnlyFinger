// 表格中转换日期
export function transformDate(row, column, cellValue, index) {
    if (cellValue == null || cellValue == "") return "";
    let date = new Date(parseInt(cellValue));//时间戳为10位需*1000，时间戳为13位的话不需乘1000
    let Y = date.getFullYear() + '-';
    let M = date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) + '-' : date.getMonth() + 1 + '-';
    let D = date.getDate() < 10 ? '0' + date.getDate() + ' ' : date.getDate() + ' ';
    let h = date.getHours() < 10 ? '0' + date.getHours() + ':' : date.getHours() + ':';
    let m = date.getMinutes() < 10 ? '0' + date.getMinutes() + ':' : date.getMinutes() + ':';
    let s = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
    return Y + M + D + h + m + s;
}
// 表格中转换性别
export function transformGender(row, column, cellValue, index){
    let gender = parseInt(cellValue)
    if(gender == 1) 
        return "男" 
    else if(gender == 0)
        return "女"
    else
        return "保密"
}
// 格式化描述
export function splitDesc(row, column, cellValue, index){
    if (cellValue == null || cellValue == "") return "";
    
    if(cellValue.length < 10)
        return cellValue
    else
        return cellValue.substring(0, 10) + "..."
}