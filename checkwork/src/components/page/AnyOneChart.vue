<template>
    <div class="page">
        <div class="anyoneheader">
            <p>XX员工详细考勤记录</p>
            <div class="exit">返回</div>
        </div>
        <div class="anyonecharts">
            主要内容
        </div>
    </div>
</template>

<script>

export default ({
    name: "AnyOneChart",

})
</script>

<style lang="less" scoped>
.page {
    display: flex;
    flex-direction: column;
    .anyoneheader {
        flex: 1;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        p {
            margin: 2rem 0 0 3rem;
            font-size: 2rem;
        }
        .exit {
            margin: 2rem 8.3rem 0 0;
        }
    }
    .anyonecharts {
        flex: 5;
        display: flex;
        flex-direction: column;

    }
}
</style>
